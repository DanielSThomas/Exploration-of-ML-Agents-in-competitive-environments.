using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplyForce : MonoBehaviour
{

    [SerializeField] public float forceStrength;
    [SerializeField] private string filter;

    private void OnTriggerEnter(Collider collision)
    {
        Rigidbody rb = collision.GetComponent<Rigidbody>();

        if (rb != null && collision.tag == filter)
        {
            Vector3 dir = collision.transform.position - transform.position;

            rb.AddForce(dir.normalized * forceStrength, ForceMode.Impulse);
        }
    }

}
