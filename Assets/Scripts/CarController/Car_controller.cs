using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class Car_controller : MonoBehaviour
{


    // Wheel Coliders used for suspension and driving.
    public WheelCollider fl_wheel, fr_wheel;
    public WheelCollider bl_wheel, br_wheel;
    // Wheel transform points used to change rotation of wheel models
    public Transform fl_wheel_t, fr_wheel_t;
    public Transform bl_wheel_t, br_wheel_t;

    // shit go brrrrr
    public float maxSteerAngle = 30;
    public float motorForce = 50;
    public int brakePower = 200;
    public bool isBraking = false;
    public double currentSpeed;
    public bool AIControlled = false;
    public AI_Handler AI_handler;




    private float trueSpeed;
    private float horizontalInput;
    private float verticalInput;
    private float steering_angle;
    





    public void GetInput()
    {
        // Controls
        if(AIControlled == false)
        {
            horizontalInput = Input.GetAxis("Horizontal");
            verticalInput = Input.GetAxis("Vertical");
            isBraking = Input.GetButton("Handbrake");
        }
        else
        {
            
            isBraking = Input.GetButton("Handbrake");
            horizontalInput = AI_handler.PathFinding();
            verticalInput = AI_handler.SpeedTester(horizontalInput);
        }


    }
    private void Handbrake()
    {
        if (isBraking == true)
        {
            bl_wheel.brakeTorque = brakePower;
            br_wheel.brakeTorque = brakePower;
        }
        else
        {
            bl_wheel.brakeTorque = 0;
            br_wheel.brakeTorque = 0;
        }
    }
    public void Steer(float horizontalInput)
    {
        steering_angle = maxSteerAngle * horizontalInput;
        fl_wheel.steerAngle = steering_angle;
        fr_wheel.steerAngle = steering_angle;
    }

    private void Accelerate(float verticalInput)
    {
            bl_wheel.motorTorque = verticalInput * motorForce;
            br_wheel.motorTorque = verticalInput * motorForce;
    }

    private void UpdateWheelPoses()
    {
        UpdateWheelPose(fl_wheel, fl_wheel_t);
        UpdateWheelPose(fr_wheel, fr_wheel_t);
        UpdateWheelPose(bl_wheel, bl_wheel_t);
        UpdateWheelPose(br_wheel, br_wheel_t);
    }

    private void UpdateWheelPose(WheelCollider _collider, Transform Wheel_Transform)
    {
        UnityEngine.Vector3 _pos = Wheel_Transform.position;
        UnityEngine.Quaternion _quat = Wheel_Transform.rotation;
        _collider.GetWorldPose(out _pos, out _quat);
        _quat = _quat * UnityEngine.Quaternion.Euler(new UnityEngine.Vector3(0, -90, 0));
        Wheel_Transform.position = _pos;
        Wheel_Transform.rotation = _quat;

    }
    private void Update()
    {
    
    }
    private void FixedUpdate()
    {
        trueSpeed = (GetComponent<Rigidbody>().velocity.magnitude * 10);
        currentSpeed = Math.Round(trueSpeed, 1);
        this.gameObject.GetComponent<Rigidbody>().AddForce(-UnityEngine.Vector3.up*trueSpeed);
        GetInput();
        Handbrake();
        Accelerate(verticalInput);
        Steer(horizontalInput);
       
        UpdateWheelPoses();


    }

}