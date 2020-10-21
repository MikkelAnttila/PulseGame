using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseInteractable : MonoBehaviour
{
    public Rigidbody rb;

    [Header("Animation")]
    public Transform grabPointLeft;
    public Transform grabPointRight;


    public void Use()
    {

    }

    public void Throw()
    {

    }

    public void Grab()
    {
        rb.isKinematic = true;
    }

    public void Release()
    {
        rb.isKinematic = false;
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
