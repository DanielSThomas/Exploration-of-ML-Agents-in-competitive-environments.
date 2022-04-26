using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class Bullet : MonoBehaviour
{

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
            bulletOwner.AddReward(0.3f);  // Change this to update if max health is changed

            //Minus reward for getting hit
            hitAgent.AddReward(-0.3f);

            if (hitAgent.getHealth() == 0)
            {
                //Give reward for elim

                hitAgent.AddReward(-1f);

                bulletOwner.AddReward(1f); // only works for 1v1 need to put this in the end of game instead
                
            }


        Destroy(this.gameObject);
        }
        //Miss penalty

        else
        {
            bulletOwner.AddReward(-0.05f);
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
