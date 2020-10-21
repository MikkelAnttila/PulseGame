using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextureRenderer : MonoBehaviour
{

    Texture2D controlMap;

    public Material material;

    private void Start()
    {
        controlMap = new Texture2D(20, 20, TextureFormat.ARGB32, false);
    }

    public void RenderTexture()
    {
        for (int col = 0; col < GridMap.instance.gridDimensions; col++)
        {
            for (int row = 0; row < GridMap.instance.gridDimensions; row++)
            {
                //controlMap.SetPixel(row, col, new Color(Mathf.Clamp01((GridMap.instance.CalculateMoisture(new Vector2Int(row, col)) / 10)), (Mathf.Clamp01(GridMap.instance.CalculateNutrition(new Vector2Int(row, col)) / 10)), 0, 0));
                controlMap.SetPixel(col, row, new Color(GridMap.instance.moistureGrid[col, row], GridMap.instance.nutritionGrid[col, row], 0, 0)); 
            }
        }

        controlMap.Apply();

        //Debug.Log("Render");
        material.SetTexture("Texture2D_882DD628", controlMap);
        //shader property id to get it
    }

}
