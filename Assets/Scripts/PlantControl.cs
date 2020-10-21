using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantControl : MonoBehaviour
{
    public static PlantControl instance;

    public List<GrowerTest> growerList = new List<GrowerTest>();

    private void Awake()
    {
        instance = this;
    }

    public void Grow()
    {
        for (int i = 0; i < growerList.Count; i++)
        {
            growerList[i].PulsePlant();
        }
    }
}
