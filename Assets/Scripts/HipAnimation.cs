using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HipAnimation : MonoBehaviour
{

    public Transform leftFoot;
    public Transform rightFoot;

    void Update()
    {
        Vector3 lAngle = transform.localPosition - leftFoot.localPosition;
        Vector3 rAngle = transform.localPosition - rightFoot.localPosition;

        Vector3 dir = (lAngle - rAngle) * 2;

        Quaternion rotation = Quaternion.Euler(dir * -10);
        
        //Quaternion.Lerp(transform.localRotation, rotation, Time.deltaTime * 0.5f);

        Quaternion.RotateTowards(transform.localRotation, rotation, Time.deltaTime * 0.5f);

        transform.localRotation = rotation;
    }
}
