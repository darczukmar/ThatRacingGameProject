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

    public GameObject rayHolderL;
    public GameObject rayHolderLF;
    public GameObject rayHolderR;
    public GameObject rayHolderRF;
    public GameObject nextCheckpoint;
    public GameObject Trailing;

    public int carIndex;

    public float raycastdistance;
    public float forwardRaycasDistance;
    [Range(0.0f, 0.1f)]
    public float reactionStrFull;
    [Range(0.0f, 0.1f)]
    public float reactionStrHalf;
    [Range(-1.0f, 1.0f)]
    public float horizontalInput;
    [Range(0.0f, 1.0f)]
    public float verticalInput;
    float adjustAngle;

    public void Awake()
    {
        car_Controller = this.gameObject.transform.GetComponent<Car_controller>();
        race_Manager = GameObject.FindGameObjectWithTag("LogicHandler").GetComponent<Race_manager>();
    }



    public float PathFinding()
    {


        // Raycasts for colider checking
        // RaycastHit for getting transform of the hit victim
        Ray rayLF = new Ray(rayHolderLF.transform.position, rayHolderLF.transform.forward * 5);
        Ray rayL = new Ray(rayHolderL.transform.position, rayHolderL.transform.forward * 5);
        Ray rayR = new Ray(rayHolderR.transform.position, rayHolderR.transform.forward * 5);
        Ray rayRF = new Ray(rayHolderRF.transform.position, rayHolderRF.transform.forward * 5);

        Ray rayForward = new Ray(this.transform.position, this.transform.forward * 5);

        RaycastHit hitLF, hitL, hitR, hitRF,hitTrailing;

        UnityEngine.Vector3 targetDir;



        if (Physics.Raycast(rayLF, out hitLF, forwardRaycasDistance) && !hitLF.transform.CompareTag("PlayerMesh"))
        {
            Debug.DrawRay(rayHolderLF.transform.position, rayHolderLF.transform.forward * forwardRaycasDistance, Color.red);
            horizontalInput = Mathf.Clamp(horizontalInput + reactionStrFull + 0.1f / hitLF.distance / 2, -1f, 1f);
        }

        if (Physics.Raycast(rayL, out hitL, raycastdistance) && !hitL.transform.CompareTag("PlayerMesh"))
        {
            Debug.DrawRay(rayHolderL.transform.position, rayHolderL.transform.forward * raycastdistance, Color.red);

            horizontalInput = Mathf.Clamp(horizontalInput + reactionStrHalf + 0.1f / hitL.distance / 2, -1f, 1f);
        }





        if (Physics.Raycast(rayRF, out hitRF, forwardRaycasDistance) && !hitRF.transform.CompareTag("PlayerMesh"))
        {
            Debug.DrawRay(rayHolderRF.transform.position, rayHolderRF.transform.forward * forwardRaycasDistance, Color.red);
            horizontalInput = Mathf.Clamp(horizontalInput + -reactionStrFull + 0.1f / -hitRF.distance / 2, -1f, 1f);
        }
        if (Physics.Raycast(rayR, out hitR, raycastdistance) && !hitR.transform.CompareTag("PlayerMesh"))
        {
            //Debug.Log(hitR.distance);
            Debug.DrawRay(rayHolderR.transform.position, rayHolderR.transform.forward * raycastdistance, Color.red);
            horizontalInput = Mathf.Clamp(horizontalInput + -reactionStrHalf + 0.1f / -hitR.distance / 2, -1f, 1f);
        }

        /*
        if(Physics.Raycast(rayForward,out hitTrailing, 10f,2))
        {
            if (hitTrailing.transform.CompareTag("PlayerMesh"))
            {
                Trailing = hitTrailing.transform.gameObject;
                targetDir = hitTrailing.transform.position - this.gameObject.transform.position;
                adjustAngle = UnityEngine.Vector3.SignedAngle(targetDir, this.gameObject.transform.forward, this.gameObject.transform.up);
                horizontalInput = Mathf.Clamp(-adjustAngle / 90, -1, 1);
            }

        }
        */

        if (hitL.transform == null && hitLF.transform == null && hitR.transform == null && hitRF.transform == null)
        {
            targetDir = nextCheckpoint.transform.position - this.gameObject.transform.position;
            adjustAngle = UnityEngine.Vector3.SignedAngle(targetDir, this.gameObject.transform.forward, this.gameObject.transform.up);
            //Debug.Log(adjustAngle);
            horizontalInput = Mathf.Clamp(-adjustAngle / 90, -1, 1);
        }
        return horizontalInput;
    }
    public float SpeedTester(float horizontalInput)
    {
        verticalInput = 1;
        verticalInput = verticalInput - Mathf.Abs(horizontalInput)/2;


        return verticalInput;
    }
    private void FixedUpdate()
    {
        PathFinding();
    }

}