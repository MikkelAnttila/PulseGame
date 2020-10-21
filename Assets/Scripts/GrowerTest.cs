using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Assertions.Must;

public class GrowerTest : MonoBehaviour
{
    public AnimationCurve moistureFulfilment;
    public AnimationCurve nutritionFulfilment;
    [Range(0,1)]
    public float moistureUsage;
    [Range(0, 1)]
    public float nutritionUsage;

    [Range(0, 1)]
    public float baseGrowth = 0.2f;


    [Range(0,100)]
    public int fruitStageStart = 10;
    [Range(0, 100)]
    public int dyingStageStart = 20;
    [Range(0, 100)]
    public int dyingStageEnd = 30;

    public GameObject fruit;
    
    Berry _berryScript;
    int _pulseCount;
    Vector2Int _plantPosition;


    //
    float _currentNutritionLevel;
    float _currentMoistureLevel;


    private void Start()
    {
        PlantControl.instance.growerList.Add(this);
        _plantPosition = new Vector2Int(Mathf.Clamp(Mathf.FloorToInt(transform.position.x), 0, 19), Mathf.Clamp(Mathf.FloorToInt(transform.position.z), 0, 19));
    }

    public void PulsePlant()
    {
        _pulseCount += 1;

        //Maybe use lambda expression to make cleaner
        if (_pulseCount >= dyingStageStart)
        {
            if(_pulseCount >= dyingStageEnd)
            {
                Die();
            }
            else
            {
                Dying();
            }
        }
        else if (_pulseCount >= fruitStageStart)
        {
            Fruiting();
        }
        else
        {
            Grow();
        }
    }

    void Grow()
    {
        //Grow plant
        transform.localScale = transform.localScale + new Vector3(0,adjustedGrowth(baseGrowth));

        //use resources
        GridMap.instance.nutritionGrid[_plantPosition.x, _plantPosition.y] -= adjustedGrowth(nutritionUsage);
        GridMap.instance.moistureGrid[_plantPosition.x, _plantPosition.y] -= adjustedGrowth(moistureUsage);
    }
    
    void Fruiting()
    {
        if (!_berryScript) 
        {
            //Work on placement later
            GameObject fruitInstance = Instantiate(fruit, transform.position + new Vector3(0,1,0), Quaternion.identity, transform.parent);
            _berryScript = fruitInstance.GetComponent<Berry>();
        }

        //Grow fruit
        _berryScript.Grow(adjustedGrowth(baseGrowth));

        //use resources
        GridMap.instance.nutritionGrid[_plantPosition.x, _plantPosition.y] -= adjustedGrowth(nutritionUsage);
        GridMap.instance.moistureGrid[_plantPosition.x, _plantPosition.y] -= adjustedGrowth(moistureUsage);

        if (_berryScript.berryPulseCount >= _berryScript.ripePulse)
        {
            _berryScript.Detach();
            _berryScript = null;
        }
    }

    void Dying()
    {
        if (_berryScript)
        {
            _berryScript.Detach();
            _berryScript = null;
        }

        //"Degrow" turn brown
    }

    void Die()
    {
        // number 10 should be number related to the plants size. Bc why not?
        GridMap.instance.nutritionGrid[_plantPosition.x, _plantPosition.y] += 0.1f;
        Destroy(gameObject);
    }

    float adjustedGrowth(float unadjustedGrowth)
    {
        //Takes input(unadjustedGrowth) and returns after modifying by current nutrition and moisture levels.

        float result;

        _currentNutritionLevel = GridMap.instance.nutritionGrid[_plantPosition.x, _plantPosition.y];
        _currentMoistureLevel = GridMap.instance.moistureGrid[_plantPosition.x, _plantPosition.y];
        float growthModifier = (nutritionFulfilment.Evaluate(_currentNutritionLevel) + moistureFulfilment.Evaluate(_currentMoistureLevel)) / 2;
        result = unadjustedGrowth * growthModifier;

        return result;
    }

    private void OnDisable()
    {
        PlantControl.instance.growerList.Remove(this);
    }
}
