using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    private int bulletTeam;
    [SerializeField] int damage;
    [SerializeField] int bulletLifeTime;
    private int bulletTimer = 0;

    public Bullet(int _bulletTeam)
    {
        bulletTeam = _bulletTeam;
    }
   

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.forward;

        bulletTimer += 1;
        if (bulletTimer >= bulletLifeTime)
        {
            Destroy(this);
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        Health hp = other.GetComponent<Health>();
        EliminationAgent hitAgent = other.GetComponent<EliminationAgent>();
        if (hp != null && hitAgent.getTeam() != bulletTeam)
        {
            hp.setHealth(damage);
        }
        Destroy(this);
    }


}
