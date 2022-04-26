using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    Collider2D col;
    SpriteRenderer sr;

    [SerializeField] private float timesCollected = 0;
    [SerializeField] private int coinTeam;

    // Start is called before the first frame update
    void Start()
    {
        col = this.GetComponent<Collider2D>();
        sr = this.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if(collision.GetComponent<EliminationAgent>() != null && coinTeam == collision.GetComponent<EliminationAgent>().getTeam())
        {
            

            if(timesCollected <= 3)
            {
                collision.GetComponent<EliminationAgent>().AddReward(0.3f - timesCollected / 10);

                timesCollected += 1f;

                this.gameObject.tag = "Coin" + timesCollected;
            }
            
            col.enabled = false;

            sr.color = Color.gray;

            Invoke("Reactivate", 20);

        }
        

    }

    private void Reactivate()
    {
        col.enabled = true;

        sr.color = Color.yellow;
    }

}
