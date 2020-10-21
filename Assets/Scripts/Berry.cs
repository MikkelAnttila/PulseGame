using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Berry : MonoBehaviour
{
    public float baseGrowthRate = 0.2f;

    public GameObject plant;

    public int berryPulseCount;
    
    public int ripePulse = 10;

    public Rigidbody rb;

    int _rotPulse = 15;


    public void Grow(float amount)
    {
        berryPulseCount += 1;

        transform.localScale += new Vector3(amount,amount,amount);
    }

    public void Detach()
    {
        rb.isKinematic = false;
        rb.AddForce(new Vector3(Random.Range(-50, 50), Random.Range(-50, 50), Random.Range(-50, 50)));
        BerryControl.instance.berryList.Add(this);
    }

    public void Rot()
    {
        berryPulseCount += 1;

        //Change colour tint to brown
        if (berryPulseCount >= _rotPulse)
        {
            Seed();
        }
    }

    void Seed()
    {
        BerryControl.instance.berryList.Remove(this);
        Instantiate(plant, transform.position, Quaternion.identity, transform.parent);
        Destroy(gameObject);
    }
}
