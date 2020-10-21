using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationCounterWeight : MonoBehaviour
{
    public Transform target;

    // Update is called once per frame
    void Update()
    {
        transform.position = target.position;    
    }
}
