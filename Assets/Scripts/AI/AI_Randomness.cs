﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Randomness : MonoBehaviour
{

    // This Method picks a random point within NextCheckpoint box collider to add randomness to cars path
    public static UnityEngine.Vector3 RandomPointInBounds(GameObject checkpoint)
    {
        return new UnityEngine.Vector3(
            UnityEngine.Random.Range(checkpoint.GetComponent<BoxCollider>().bounds.min.x+1f, checkpoint.GetComponent<BoxCollider>().bounds.max.x-1f),
            checkpoint.transform.position.y,
            UnityEngine.Random.Range(checkpoint.GetComponent<BoxCollider>().bounds.min.z, checkpoint.GetComponent<BoxCollider>().bounds.max.z));
        /*
            UnityEngine.Random.Range(checkpoint.GetComponent<BoxCollider>().bounds.min.y, checkpoint.GetComponent<BoxCollider>().bounds.max.y),
            UnityEngine.Random.Range(checkpoint.GetComponent<BoxCollider>().bounds.min.z, checkpoint.GetComponent<BoxCollider>().bounds.max.z)
        );;*/
    }
}