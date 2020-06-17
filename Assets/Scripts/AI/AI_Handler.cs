using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.EventSystems;

public class AI_Handler : MonoBehaviour
{
    public Car_controller car_Controller;
    public Race_manager race_Manager;

    public GameObject rayHolderL,             // rayHolders used for origin point of raycasts , can be easily adjustem in aditor.
                    rayHolderLF,
                    rayHolderR,
                    rayHolderRF,
                    nextCheckpoint;           //next checkpoint is assinged by Race Manager from race Array
    public float raycastdistance,
             forwardRaycasDistance,
             adjustAngle,
             responseTime;


    public int carIndex;

    [Range(0.0f, 0.5f)]
    public float reactionStrFull;
    [Range(0.0f, 0.1f)]
    public float reactionStrHalf;
    [Range(-1.0f, 1.0f)]
    public float horizontalInput;
    [Range(0.0f, 1.0f)]
    public float verticalInput;

    public UnityEngine.Vector3 nextCheckpointTrue;                      //Random position chosen from box collider - Car is actually going here.
    private UnityEngine.Vector3 targetDir = UnityEngine.Vector3.zero;    //Direction in which cars next objective is.

    public void Awake()
    {
        car_Controller = this.gameObject.transform.GetComponent<Car_controller>();
        race_Manager = GameObject.FindGameObjectWithTag("LogicHandler").GetComponent<Race_manager>();
        targetDir = nextCheckpoint.transform.position - this.gameObject.transform.position;
    }



    public float PathFinding()
    {



        Ray rayLF = new Ray(rayHolderLF.transform.position, rayHolderLF.transform.forward * 5),              // Raycasts for colider checking
            rayL = new Ray(rayHolderL.transform.position, rayHolderL.transform.forward * 5),
            rayR = new Ray(rayHolderR.transform.position, rayHolderR.transform.forward * 5),
            rayRF = new Ray(rayHolderRF.transform.position, rayHolderRF.transform.forward * 5);

        if (true)
        {
            Debug.DrawRay(rayHolderL.transform.position, rayHolderL.transform.forward * raycastdistance, Color.red);
            Debug.DrawRay(rayHolderLF.transform.position, rayHolderLF.transform.forward * forwardRaycasDistance, Color.red);
            Debug.DrawRay(rayHolderRF.transform.position, rayHolderRF.transform.forward * forwardRaycasDistance, Color.red);
            Debug.DrawRay(rayHolderR.transform.position, rayHolderR.transform.forward * raycastdistance, Color.red);
            Debug.DrawRay(this.gameObject.transform.position, targetDir, Color.green);
        }
        RaycastHit hitLF, hitL, hitR, hitRF;                                                                 // RaycastHit for getting transform of the hit victim

        AI_Additives.checkCollisions(rayLF, hitLF, forwardRaycasDistance, reactionStrFull, false, horizontalInput);
        AI_Additives.checkCollisions(rayL, hitL, raycastdistance, reactionStrFull, false, horizontalInput);
        AI_Additives.checkCollisions(rayRF, hitRF, forwardRaycasDistance, reactionStrFull, true, horizontalInput);
        AI_Additives.checkCollisions(rayR, hitR, raycastdistance, reactionStrFull, true, horizontalInput);

        if (hitL.transform == null && hitLF.transform == null && hitR.transform == null && hitRF.transform == null)
        {
            if (nextCheckpointTrue == UnityEngine.Vector3.zero)
            {
                targetDir = nextCheckpoint.transform.position - this.gameObject.transform.position;
            }
            else
            {
                targetDir = nextCheckpointTrue - this.gameObject.transform.position;
            }

            adjustAngle = UnityEngine.Vector3.SignedAngle(targetDir, this.gameObject.transform.forward, this.gameObject.transform.up);
            horizontalInput = Mathf.Clamp(-adjustAngle / 90, -1, 1);
        }
 
        return  horizontalInput;
    }
    public bool BrakeFinding()

    {
        Ray rayForward = new Ray(this.transform.position, this.transform.forward * 5);
        RaycastHit hitForward;
        bool shouldBrake;
        if (car_Controller.currentSpeed > 170 || Physics.Raycast(rayForward, out hitForward, 5f))
            shouldBrake = true;
        else
            shouldBrake = false;
        return shouldBrake;
    }
    public float SpeedTester(float horizontalInput)
    {
        verticalInput = 1;
        verticalInput = verticalInput - Mathf.Abs(horizontalInput) / 1.5f;


        return verticalInput;
    }

    private void FixedUpdate()
    {
        PathFinding();
    }

}