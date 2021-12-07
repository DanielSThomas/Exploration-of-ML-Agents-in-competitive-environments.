using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;

public class RollerBotAgent : Agent
{

    [SerializeField] private float speed = 10;   
    [SerializeField] private Rigidbody rb;
    [SerializeField] private Transform target;
    [SerializeField] private Transform spawn;
    [SerializeField] private bool isAttacker;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void OnEpisodeBegin()
    {
        if (rb.transform.position.y < -1 || target.transform.position.y < -1)
        {
            rb.angularVelocity = Vector3.zero;
            rb.velocity = Vector3.zero;
            rb.transform.position = spawn.position;              
        }
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
        Vector3 controlSignal = Vector3.zero;
        controlSignal.x = actionBuffers.ContinuousActions[0];
        controlSignal.z = actionBuffers.ContinuousActions[1];
        
        rb.AddForce(controlSignal * speed);

        
        //Rewards

        if(isAttacker == true)
        {
            if (target.transform.position.y < -1)
            {
                AddReward(1f);
                EndEpisode();
            }
            

            AddReward(-0.0005f);

            // Fell off platform
           
        }
        else if(isAttacker == false)
        {
            AddReward(+0.0005f);
        }

        if (rb.transform.position.y < -1)
        {
            AddReward(-1f);
            EndEpisode();
        }

    }

    public override void Heuristic(in ActionBuffers actionsOut) // Player Control
    {
        var continuousActionsOut = actionsOut.ContinuousActions;
        continuousActionsOut[0] = Input.GetAxis("Horizontal");
        continuousActionsOut[1] = Input.GetAxis("Vertical");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isAttacker == true && other.tag == "Target")
        {
            AddReward(0.5f);
            Debug.Log("we hit em");
        }
        else if (isAttacker == false && other.tag == "Attacker")
        {
                AddReward(-0.5f);
                Debug.Log("we got hit");
        }
    }

}
