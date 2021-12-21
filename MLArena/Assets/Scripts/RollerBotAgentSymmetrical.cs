using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;

public class RollerBotAgentSymmetrical : Agent
{

    [SerializeField] private float speed = 10;
    [SerializeField] private int health = 100;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private RollerBotAgentSymmetrical target;
    [SerializeField] private Transform spawn;
    [SerializeField] private bool isAttacker;
    [SerializeField] private float meanReward;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //if (target.getHealth() < 1)
        //{
        //  AddReward(1f);
          //EndEpisode();
        //}
    }

    public override void OnEpisodeBegin()
    {
       
           rb.angularVelocity = Vector3.zero;
           rb.velocity = Vector3.zero;
           this.transform.localPosition = spawn.position;
           health = 100;
       
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        //sensor.AddObservation(rb.transform.localPosition);
   
        //sensor.AddObservation(rb.velocity.x);
        //sensor.AddObservation(rb.velocity.z);
    }

    public override void OnActionReceived(ActionBuffers actionBuffers)
    {
        // Actions



        Vector3 movedir = new Vector3(0, 0, 0);

        int horizontalDir = actionBuffers.DiscreteActions[0];
        int verticalDir = actionBuffers.DiscreteActions[1];

        switch(horizontalDir)
        {
            case 0: movedir.x = 0; break;
            case 1: movedir.x = 1; break;
            case 2: movedir.x = -1; break;
        }

        switch (verticalDir)
        {
            case 0: movedir.z = 0; break;
            case 1: movedir.z = 1; break;
            case 2: movedir.z = -1; break;
        }


        rb.AddForce(movedir * speed);

        meanReward = GetCumulativeReward();
        //Rewards

        if (isAttacker == true)
        {
            
            AddReward(-1f/MaxStep);
                
        }
        else if(isAttacker == false)
        {
            AddReward(+1f/MaxStep);
        }

        if (health < 1)
        {
            
            AddReward(-1f);
            if (isAttacker == false)
            {                
                target.AddReward(+1);
            }
            Debug.Log(this.gameObject.name + " Score: " + GetCumulativeReward());
            Debug.Log(target.gameObject.name + " Score: " + target.GetCumulativeReward());
            target.EndEpisode();
            EndEpisode();
            
        }

    }

    public override void Heuristic(in ActionBuffers actionsOut) // Player Control
    {
        var discreteActionsOut = actionsOut.DiscreteActions;

        switch (Input.GetAxisRaw("Horizontal"))
        {
            case -1: discreteActionsOut[0] = 2; break;
            case 0: discreteActionsOut[0] = 0; break;
            case 1: discreteActionsOut[0] = 1; break;
        }

        switch (Input.GetAxisRaw("Vertical"))
        {
            case -1: discreteActionsOut[1] = 2; break;
            case 0: discreteActionsOut[1] = 0; break;
            case 1: discreteActionsOut[1] = 1; break;
        }


    }

    private void OnTriggerEnter(Collider other)
    {
        if (isAttacker == true && other.tag == "BlueTeam")
        {
            AddReward(0.1f);
            Debug.Log("we hit em");
        }
        else if (isAttacker == false && other.tag == "RedTeam")
        {
                AddReward(-0.1f);
                Debug.Log("we got hit");
        }
    }

    public int getHealth()
    {
        return health;
    }

    public void setHealth(int value)
    {
        health = value;
    }

}
