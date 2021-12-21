using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageTrigger : MonoBehaviour
{
    [SerializeField] private int damage;

    private void OnTriggerEnter(Collider other)
    {
        RollerBotAgentAsymmetric rba = other.GetComponent<RollerBotAgentAsymmetric>();
        if(rba != null)
        {
            rba.setHealth(damage);
        }
    }




}
