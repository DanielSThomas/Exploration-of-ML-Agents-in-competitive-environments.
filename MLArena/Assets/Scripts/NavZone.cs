using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavZone : MonoBehaviour
{

    [SerializeField] private float bluevisits = 0;
    [SerializeField] private float redvisits = 0;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        EliminationAgent visitingAgent = collision.GetComponent<EliminationAgent>();

        if(visitingAgent != null)
        {


            if(visitingAgent.getTeam() == 0)
            {
                visitingAgent.AddReward(1f - redvisits);

                redvisits += 0.2f;
            }

            if (visitingAgent.getTeam() == 1)
            {
                visitingAgent.AddReward(1f - bluevisits);

                bluevisits += 0.2f;
            }


        }

    }


    public float getRedVisits()
    {
        return redvisits;
    }

    public float getBlueVisits()
    {
        return bluevisits;
    }



}
