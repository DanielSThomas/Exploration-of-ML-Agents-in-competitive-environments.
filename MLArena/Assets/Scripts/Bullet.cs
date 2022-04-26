using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class Bullet : MonoBehaviour
{
    [SerializeField] bool agentlessbullet;
    [SerializeField] private int bulletTeam;
    [SerializeField] private int damage;
    [SerializeField] private int bulletLifeTime;
    [SerializeField] private float speed;
    [SerializeField] private EliminationAgent bulletOwner;
    [SerializeField] SpriteRenderer sr;
    [SerializeField] Light2D li;


    private void Awake()
    {
      
        if (bulletTeam == 0)
        {       
            sr.color = new Color(1.0f, 0.30f, 0.0f); //orange
            li.color = new Color(1.0f, 0.30f, 0.0f);
        }
        else if (bulletTeam == 1)
        {
            sr.color = Color.cyan;
            li.color = Color.cyan;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position += transform.up * speed;

        bulletLifeTime -= 1;
        if (bulletLifeTime <= 0)
        {
            Destroy(this.gameObject);
        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
   
        EliminationAgent hitAgent = other.GetComponent<EliminationAgent>();
        if (hitAgent != null && hitAgent.getTeam() != bulletTeam)
        {
            hitAgent.setHealth(hitAgent.getHealth() - damage);

            //Give reward for hitting an enemy
            if(agentlessbullet == false)
            {
                bulletOwner.AddReward(0.3f);
            }
            
            //Minus reward for getting hit
            hitAgent.AddReward(-0.3f);

            if (hitAgent.getHealth() == 0)
            {
                //Give reward for elim

                hitAgent.AddReward(-1f);

                if (agentlessbullet == false)
                {
                    bulletOwner.AddReward(1f);
                }

            }


        Destroy(this.gameObject);
        }

        //Miss penalty
        else
        {
            if (agentlessbullet == false)
            {
                bulletOwner.AddReward(-0.03f);
            }
    
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
