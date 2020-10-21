using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using UnityEngine;

public class ObjectGrabber : MonoBehaviour
{
    public Transform hands;
    public ObjectTargeting targetter;
    public ArmAnimation armAnimation;

    [Header("Throwable")]
    public Rigidbody grabbedRigidbody;
    public AnimationCurve throwForce;
    float chargeTime;
    
    public Transform carryPosition;
    public BaseInteractable grabbedInteractable;

    public float grabableDistance;

    public float rtAxis;

    public bool grabBlock;
    bool _coolDownActive;
    bool _charging;
    bool _grabbingAnimation;

    //Måske skulle der laves en class der hed interactable, istedet for berry og tool, som så kan derive fra det. Så kan de have base functions der hedder use og throw eller whatevs, og override dem! GO DO IT

    void Update()
    {
        if (!grabBlock && !_coolDownActive && !grabbedRigidbody && rtAxis > 0 && targetter.currentTarget)
        {

            if (!_grabbingAnimation)
            {
                armAnimation.Grab(targetter.currentTarget);
                _grabbingAnimation = true;
            }

            if (Vector3.Distance(hands.position, targetter.currentTarget.position) < grabableDistance)
            {
                grabBlock = true;
                GrabObject(targetter.currentTarget.gameObject);
            }
        }

        if (rtAxis == 0)
        {
            if (_grabbingAnimation)
            {
                armAnimation.Release();
                _grabbingAnimation = false;
            }

            grabBlock = false;
            if (grabbedRigidbody)
            {
                ReleaseObject();
            }
        }

        MoveObjectPosition();

        if (_charging)
        {
            ChargeThrowUpdate();
        }

    }

    void MoveObjectPosition()
    {
        if (grabbedRigidbody)
        {
            grabbedRigidbody.MovePosition(carryPosition.position);
            grabbedRigidbody.MoveRotation(carryPosition.rotation);
            //did no difference
        }
    }

    void ChargeThrowUpdate()
    {
        chargeTime += Time.deltaTime;
    }

    void GrabObject(GameObject target)
    {
        grabbedRigidbody = target.GetComponent<Rigidbody>();
        grabbedInteractable = target.GetComponent<BaseInteractable>();

        if (grabbedInteractable)
        {
            grabbedRigidbody = grabbedInteractable.rb;
            grabbedRigidbody.isKinematic = true;
        }

        
    } 

    void ReleaseObject()
    {
        grabbedInteractable = null;
        grabbedRigidbody.isKinematic = false;
        grabbedRigidbody = null;
    }

    public void UseObject()
    {

    }

    public void ChargeThrow()
    {
        _charging = true;
    }

    public void ThrowObject()
    {
        if (grabbedRigidbody)
        {
            _coolDownActive = true;
            Debug.Log("throw");
            Rigidbody rb = grabbedRigidbody;
            ReleaseObject();
            rb.AddForce(transform.forward * throwForce.Evaluate(chargeTime) + Vector3.up * throwForce.Evaluate(chargeTime / 2), ForceMode.Impulse);
            _charging = false;
            chargeTime = 0;
            StartCoroutine(GrabCoolDown());
        }
    }

    IEnumerator GrabCoolDown()
    {
        yield return new WaitForSeconds(0.2f);
        _coolDownActive = false;

    }

}
