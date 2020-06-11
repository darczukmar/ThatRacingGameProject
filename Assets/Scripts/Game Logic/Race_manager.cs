using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Race_manager : MonoBehaviour
{
    public List<AI_Handler> ai_Handler;
    public GameObject[] cars;
    public GameObject[] race;
    public List<int> checkpointTracker;

    void Awake()
    {
        int count=0;
        
        cars = GameObject.FindGameObjectsWithTag("Player");
        foreach(GameObject car in cars)
        {
            checkpointTracker.Add(1);
            ai_Handler.Add(car.GetComponent<AI_Handler>());
            car.GetComponent<AI_Handler>().carIndex = count;
            car.GetComponent<AI_Handler>().nextCheckpoint = race[0];
            count++;
        }

    }

    public void CheckpointPassed(AI_Handler car,int thisCheckpoint)
    {
        thisCheckpoint = thisCheckpoint + 1;
        checkpointTracker[car.carIndex] = thisCheckpoint;
        if (checkpointTracker[car.carIndex] >= race.Length)
        {
            thisCheckpoint = 0;
        }

        car.nextCheckpoint = race[thisCheckpoint];



    }
}
