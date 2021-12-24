using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    [SerializeField] private int bulletTeam;
    [SerializeField] private int damage;
    [SerializeField] private int bulletLifeTime;
    [SerializeField] private float speed;
    [SerializeField] private EliminationAgent bulletOwner;
    [SerializeField] MeshRenderer mr;


    private void Start()
    {
        mr = GetComponent<MeshRenderer>();

        if (bulletTeam == 0)
        {
            mr.material.color = Color.red;
        }
        else if (bulletTeam == 1)
        {
            mr.material.color = Color.blue;
        }
    }

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
            hp.setHealth(hp.getHealth() - damage);
            Destroy(this.gameObject);
        }

        Destroy(this.gameObject);
    }

    public EliminationAgent getbulletOwner()
    {
        return bulletOwner;
    }

    public void setbulletOwner(EliminationAgent value)
    {
        bulletOwner = value;
    }

    public int getbulletTeam()
    {
        return bulletTeam;
    }

    public void setbulletTeam(int value)
    {
        bulletTeam = value;
    }


}
