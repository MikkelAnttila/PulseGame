using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmatureBounceAnimation : MonoBehaviour
{
    public AnimationCurve bounceCurve;

    bool _bouncing;
    float _bounceTime;

    public void Bounce()
    {
        _bounceTime = 0;
        _bouncing = true;
    }

    void Update()
    {
        if (_bouncing)
        {
            _bounceTime += Time.deltaTime;
            transform.localPosition = Vector3.Lerp(transform.localPosition, new Vector3(transform.localPosition.x, bounceCurve.Evaluate(_bounceTime), transform.localPosition.z), Time.deltaTime * 70);
            if (_bounceTime > 1)
            {
                _bouncing = false;
            }
        }        
    }
}
