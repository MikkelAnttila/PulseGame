using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLookAt : MonoBehaviour
{
    public ObjectTargeting targetter;


    void Update()
    {
        transform.LookAt(targetter.currentTarget);    
    }
}
