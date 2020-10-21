using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LegAnimation : MonoBehaviour
{
    bool legLocked;
    public LegAnimation otherLeg;
    public Transform footRotator;
    public AnimationCurve footLift;

    public LayerMask layerMask;
    public Transform targetTransform;
    public Transform anchor;
    public ArmatureBounceAnimation bodyBouncer;

    public float maxDistance;

    Vector3 _currentTargetDestination;
    Vector3 _currentTargetRotation;

    //Tester
    public PlayerTestScript playerControl;
    public float stepTriggerThreshold;
    float stepTriggerTime;
    
    void Update()
    {

        //Tester BAD THAT THERE IS TWO TIMERS INSTEAD OF A SINGLE ONE SHOULD ONLY HAVE ONE SCRIPT FOR LEG CONTROL
        if(stepTriggerTime > stepTriggerThreshold)
        {
            if (!legLocked)
            {
                MoveLeg(0.2f);
                legLocked = true;
            }
        }
        
        if (stepTriggerTime > 1)
        {
            legLocked = false;
            stepTriggerTime = 0;
        }


        if (playerControl.movement.magnitude == 0)
        {
            stepTriggerTime = 0;
            MoveLeg(0);
        }
        else
        {
            stepTriggerTime += 2.5f * Time.deltaTime * playerControl.movement.magnitude;
        }

        //Tester


        targetTransform.position = Vector3.MoveTowards(targetTransform.position, _currentTargetDestination, Time.deltaTime * 3) + (new Vector3(0,footLift.Evaluate(stepTriggerTime), 0));
        targetTransform.rotation = transform.rotation;

        //if (Vector3.Distance(anchor.position, targetTransform.position) < maxDistance / 2)
        //{
        //    otherLeg.legLocked = false;
        //}

        //if (!legLocked)
        //{
        //    if (Vector3.Distance(anchor.position, targetTransform.position) > maxDistance)
        //    {
        //        MoveLeg();
        //    }
        //}

        //targetTransform.forward = transform.forward;
        //targetTransform.up = Vector3.up;
    }

    void FixedUpdate()
    {
        //get angle from ground, rotate foot
        footRotator.rotation =  Quaternion.Euler(_currentTargetRotation);
        footRotator.forward = transform.forward;
    }


    public void MoveLeg(float reach)
    {
        legLocked = true;
        bodyBouncer.Bounce();
        RaycastHit hit;
        Physics.Raycast(transform.position + (anchor.forward * reach), transform.up * -1, out hit, 10, layerMask, QueryTriggerInteraction.UseGlobal);

        _currentTargetDestination = hit.point + (Vector3.up * 0.06f);
        _currentTargetRotation = hit.normal;

        //StartCoroutine(UnlockOtherLeg());
    }

    IEnumerator UnlockOtherLeg()
    {
        yield return new WaitForSeconds(0.1f);
        otherLeg.legLocked = false;
    }

}
