using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplyForce : MonoBehaviour
{

    [SerializeField] public float forceStrength;
    

    private void OnTriggerEnter(Collider collision)
    {
        Rigidbody rb = collision.GetComponent<Rigidbody>();

        if (rb != null)
        {
            Vector3 dir = collision.transform.position - transform.position;

            rb.AddForce(dir.normalized * forceStrength, ForceMode.Impulse);
        }
    }

}
