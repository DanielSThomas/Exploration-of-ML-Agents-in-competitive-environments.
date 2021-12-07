using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageTrigger : MonoBehaviour
{
    [SerializeField] private int damage;

    private void OnTriggerEnter(Collider other)
    {
        RollerBotAgent rba = other.GetComponent<RollerBotAgent>();
        if(rba != null)
        {
            rba.setHealth(damage);
        }
    }




}
