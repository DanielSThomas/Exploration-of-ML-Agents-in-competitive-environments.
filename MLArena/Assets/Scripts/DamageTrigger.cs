using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageTrigger : MonoBehaviour
{
    [SerializeField] private int damage;

    private void OnTriggerEnter(Collider other)
    {
        Health hp = other.GetComponent<Health>();
        if(hp != null)
        {
            hp.setHealth(damage);
        }
    }




}
