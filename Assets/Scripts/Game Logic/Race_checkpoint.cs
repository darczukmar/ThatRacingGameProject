using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Race_checkpoint : MonoBehaviour
{

    public Race_manager race_Manager;
    private void Awake()
    {
        race_Manager = this.gameObject.GetComponentInParent<Race_manager>();
    }


    public void OnTriggerEnter(Collider car)
    {
        
        if (car.gameObject.transform.parent.CompareTag("Player"))
        {
            Debug.Log('x');
            race_Manager.CheckpointPassed(car.GetComponentInParent<AI_Handler>(), race_Manager.checkpointTracker[car.GetComponentInParent<AI_Handler>().carIndex]);
            
        }
    }

}
