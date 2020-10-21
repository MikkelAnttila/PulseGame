using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridMap : MonoBehaviour
{
    public static GridMap instance;
    //public GridPoint[,] gridCoordinate;
    public float[,] nutritionGrid;
    public float[,] moistureGrid;

    public int gridDimensions = 10;

    private void Awake()
    {
        instance = this;
        nutritionGrid = new float[gridDimensions, gridDimensions];
        moistureGrid = new float[gridDimensions, gridDimensions];


        for (int col = 0; col < gridDimensions; col++)
        {
            for (int row = 0; row < gridDimensions; row++)
            {
                nutritionGrid[col, row] = Random.value;
                moistureGrid[col, row] = Random.value;
            }
        }



        StartCoroutine(Tester());

    }

    IEnumerator Tester()
    {
        yield return new WaitForSeconds(5);
        nutritionGrid[10, 10] = 1;
        moistureGrid[10, 10] = 1;

        Debug.Log(moistureGrid[15, 10]);
    }
}
