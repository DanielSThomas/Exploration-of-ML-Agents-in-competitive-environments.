using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotPlayerController : MonoBehaviour
{

    [SerializeField] private float torqueSpeed;
    [SerializeField] private float maxSteeringAngle;
    [SerializeField] private WheelCollider[] frontWheels;
    [SerializeField] private WheelCollider[] backWheels;


    // Start is called before the first frame update
    void Start()
    {
        

    }

    // Update is called once per frame
    void Update()
    {
        
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




        //frontWheels[0].steerAngle = maxSteeringAngle * horizontalInput;
        //frontWheels[1].steerAngle = maxSteeringAngle * horizontalInput;

    }





}
