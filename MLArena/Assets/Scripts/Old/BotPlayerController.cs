using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotPlayerController : MonoBehaviour
{

    [SerializeField] private float torqueSpeed;
    [SerializeField] private float brakeSpeed;
    [SerializeField] private float maxSteeringAngle;
    [SerializeField] private ApplyForce applyForce;
    [SerializeField] private WheelCollider[] frontWheels;
    [SerializeField] private WheelCollider[] backWheels;
    [SerializeField] private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        

    }

    // Update is called once per frame
    void Update()
    {
        ApplyForce();
    }

    private void FixedUpdate()
    {
        Movement();
    }


    private void Movement()
    {
        //Input
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

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
        if(Input.GetKey(KeyCode.Space))
        {
            
            backWheels[0].brakeTorque = brakeSpeed;
            backWheels[1].brakeTorque = brakeSpeed;
            frontWheels[0].brakeTorque = brakeSpeed;
            frontWheels[1].brakeTorque = brakeSpeed;

            
        }
        else
        {
            backWheels[0].brakeTorque = 0;
            backWheels[1].brakeTorque = 0;
            frontWheels[0].brakeTorque = 0;
            frontWheels[1].brakeTorque = 0;
        }

        





    }

    private void ApplyForce()
    {

        float _force = Mathf.Clamp((backWheels[0].rpm * 30), -6000, 6000);

        _force = Mathf.Abs(_force);

        applyForce.forceStrength = _force;
    }



}
