using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BerryControl : MonoBehaviour
{
    public static BerryControl instance;

    public List<Berry> berryList = new List<Berry>();

    private void Awake()
    {
        instance = this;
    }

    public void RotPulse()
    {
        for (int i = 0; i < berryList.Count; i++)
        {
            berryList[i].Rot();
        }
    }
}
