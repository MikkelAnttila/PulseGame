using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmAnimation : MonoBehaviour
{
    [Header("Hand IK targets")]
    public Transform leftTarget;
    public Transform rightTarget;

    [Header("IK stations")]
    public Transform leftRunningTarget;
    public Transform rightRunningTarget;
    public Transform leftRestingTarget;
    public Transform rightRestingTarget;
    public Transform leftGrabTarget;
    public Transform rightGrabTarget;


    public float animationSpeed = 5;
    public bool moving;
    bool _grabbing;
    

    void Update()
    {
        //could make this into a switch for cleanliness
        if (_grabbing)
        {
            MoveArm(leftTarget, leftGrabTarget);
            MoveArm(rightTarget, rightGrabTarget);

            //leftTarget.position = leftGrabTarget.position;
            //rightTarget.position = rightGrabTarget.position;
        } else if (moving)
        {
            MoveArm(leftTarget, leftRunningTarget);
            MoveArm(rightTarget, rightRunningTarget);

            //leftTarget.position = leftRunningTarget.position;
            //rightTarget.position = rightRunningTarget.position;
        }
        else
        {
            MoveArm(leftTarget, leftRestingTarget);
            MoveArm(rightTarget, rightRestingTarget);

            //leftTarget.position = leftRestingTarget.position;
            //rightTarget.position = rightRestingTarget.position;
        }
    }

    void MoveArm(Transform IKtarget, Transform target)
    {
        IKtarget.position = Vector3.MoveTowards(IKtarget.position, target.position, Time.deltaTime * animationSpeed);
    }


    public void Grab(Transform target)
    {
        BaseInteractable interactable = target.GetComponent<BaseInteractable>();
        if (interactable)
        {
            leftGrabTarget = interactable.grabPointLeft;
            rightGrabTarget = interactable.grabPointRight;
            _grabbing = true;
        }
    }

    public void Release()
    {
        _grabbing = false;
    }
}
