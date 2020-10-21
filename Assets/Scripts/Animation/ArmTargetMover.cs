using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmTargetMover : MonoBehaviour
{
    public Transform idleTarget;
    
    void Update()
    {
        transform.position = idleTarget.position;    
    }
}
