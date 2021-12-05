using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;

public class CoinCarBotAgent : Agent
{

    [SerializeField] private Rigidbody rb;
    [SerializeField] private Transform target;
    [SerializeField] private float torqueSpeed;
    [SerializeField] private float brakeSpeed;
    [SerializeField] private float maxSteeringAngle;
    [SerializeField] private ApplyForce applyForce;
    [SerializeField] private WheelCollider[] frontWheels;
    [SerializeField] private WheelCollider[] backWheels;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public override void OnEpisodeBegin()
    {
        // If the Agent fell, zero its momentum
        if (this.transform.localPosition.y < 0)
        {
            this.rb.angularVelocity = Vector3.zero;
            this.rb.velocity = Vector3.zero;
            this.transform.localPosition = new Vector3(0, 0.5f, 0);
            backWheels[0].motorTorque = 0;
            backWheels[1].motorTorque = 0;
        }

        // Move the target to a new spot
        target.localPosition = new Vector3(Random.value * 8 - 4, 0.5f, Random.value * 8 - 4);
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        // Target and Agent positions
        //sensor.AddObservation(target.localPosition);
       // sensor.AddObservation(this.transform.localPosition);

        // Agent velocity
        //sensor.AddObservation(frontWheels[0].motorTorque);
        //sensor.AddObservation(frontWheels[1].motorTorque);
        //sensor.AddObservation(backWheels[0].motorTorque);
        //sensor.AddObservation(backWheels[1].motorTorque);
    }

   
    public override void OnActionReceived(ActionBuffers actionBuffers)
    {
        // Actions, size = 2
        Vector3 controlSignal = Vector3.zero;
        controlSignal.x = actionBuffers.ContinuousActions[0];
        controlSignal.z = actionBuffers.ContinuousActions[1];

        Movement(controlSignal.x, controlSignal.z);

        // Rewards
        float distanceToTarget = Vector3.Distance(this.transform.localPosition, target.localPosition);

        //AddReward(-1f / MaxStep);

        // Reached target
        if (distanceToTarget < 1.42f)
        {
            SetReward(1.0f);
            EndEpisode();
        }

        // Fell off platform
        else if (this.transform.localPosition.y < 0)
        {
            //AddReward(-1f);
            EndEpisode();
        }
    }

    public override void Heuristic(in ActionBuffers actionsOut) // Player Control
    {
        var continuousActionsOut = actionsOut.ContinuousActions;
        continuousActionsOut[0] = Input.GetAxis("Horizontal");
        continuousActionsOut[1] = Input.GetAxis("Vertical");
    }

    private void Movement(float horizontalInput, float verticalInput)
    {
        
        //Steering
        for (int i = 0; i < frontWheels.Length; i++)
        {
            frontWheels[i].steerAngle = maxSteeringAngle * horizontalInput;
        }

        //Acceleration
        for (int i = 0; i < backWheels.Length; i++)
        {
            backWheels[i].motorTorque = verticalInput * torqueSpeed;
        }

        //Braking
        //if (Input.GetKey(KeyCode.Space))
        //{

        //    backWheels[0].brakeTorque = brakeSpeed;
        //    backWheels[1].brakeTorque = brakeSpeed;
        //    frontWheels[0].brakeTorque = brakeSpeed;
        //    frontWheels[1].brakeTorque = brakeSpeed;


        //}
        //else
        //{
        //    backWheels[0].brakeTorque = 0;
        //    backWheels[1].brakeTorque = 0;
        //    frontWheels[0].brakeTorque = 0;
        //    frontWheels[1].brakeTorque = 0;
        //}




    }
}
