using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    [SerializeField] private int bulletTeam;
    [SerializeField] private int damage;
    [SerializeField] private int bulletLifeTime;
    [SerializeField] private float speed;
    
    // Update is called once per frame
    void Update()
    {
        transform.position += transform.forward * speed;

        bulletLifeTime -= 1;
        if (bulletLifeTime <= 0)
        {
            Destroy(this.gameObject);
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        Health hp = other.GetComponent<Health>();
        EliminationAgent hitAgent = other.GetComponent<EliminationAgent>();
        if (hp != null && hitAgent.getTeam() != bulletTeam)
        {
            hp.setHealth(damage);
            Destroy(this.gameObject);
        }

        Destroy(this.gameObject);
    }


}
