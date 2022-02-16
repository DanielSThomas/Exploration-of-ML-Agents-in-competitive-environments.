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


    private void Start()
    {
        li = GetComponent<Light2D>();
        sr = GetComponent<SpriteRenderer>();

        if (bulletTeam == 0)
        {
            this.tag = "RedBullet";
            this.gameObject.layer = 12;
            sr.color = new Color(1.0f, 0.30f, 0.0f); //orange
            li.color = new Color(1.0f, 0.30f, 0.0f);
        }
        else if (bulletTeam == 1)
        {
            this.tag = "BlueBullet";
            this.gameObject.layer = 11;
            sr.color = Color.cyan;
            li.color = Color.cyan;
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.up * speed * Time.deltaTime;

        bulletLifeTime -= 1;
        if (bulletLifeTime <= 0)
        {
            Destroy(this.gameObject);
        }

    }

    private void OnTriggerEnter2D(Collider2D other)
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
