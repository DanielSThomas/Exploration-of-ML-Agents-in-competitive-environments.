using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour //Unessessary Script, merge into agent
{

    [SerializeField] int health;

    public int getHealth()
    {
        return health;
    }

    public void setHealth(int value)
    {
        health = value;
    }


}
