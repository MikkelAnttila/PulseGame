using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectTargeting : MonoBehaviour
{

    public Transform currentTarget;
    public Transform carryTarget;

    public LayerMask layerMask;

    public float _distanceToCurrentTarget = 20;

 
    void Update()
    {
        Collider[] overlapColliders = Physics.OverlapSphere(transform.position, transform.localScale.x/2, layerMask);
        _distanceToCurrentTarget = 100;

        for (int i = 0; i < overlapColliders.Length; i++)
        {
            float _distanceToCollider = Vector3.Distance(overlapColliders[i].transform.position, carryTarget.position);
            if (_distanceToCollider < _distanceToCurrentTarget)
            {
                _distanceToCurrentTarget = _distanceToCollider;
                currentTarget = overlapColliders[i].transform;
            }
        }

        if (overlapColliders.Length == 0)
        {
            currentTarget = null;
            _distanceToCurrentTarget = 20;
        }
    }
}
