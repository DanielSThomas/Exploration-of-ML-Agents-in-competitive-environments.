using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;
using UnityEngine.UI;

public class EliminationAgentWithCam : EliminationAgent
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(rb.transform);
        sensor.AddObservation(rb.rotation);
        sensor.AddObservation(turretPivot.rotation);

    }
}
