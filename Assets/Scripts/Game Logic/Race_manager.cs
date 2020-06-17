using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Race_manager : MonoBehaviour
{
    public List<AI_Handler> ai_Handler;         // List of all AI Handlers pulled from cars 
    public List<int> checkpointTracker;         // Each car has a index & stored int of current checkpoint to check progression.
    public GameObject[] cars, race;             // Cars for GO with tag Player in scene , race for all GO with tag Checkpoint in scene.


    void Awake()
    {
        int count=0;
        race = GameObject.FindGameObjectsWithTag("Checkpoint");
        cars = GameObject.FindGameObjectsWithTag("Player");
        foreach(GameObject car in cars)
        {
            checkpointTracker.Add(0);
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
       car.nextCheckpointTrue=  AI_Additives.RandomPointInBounds(race[thisCheckpoint]);
       car.nextCheckpoint = race[thisCheckpoint];



    }
}
