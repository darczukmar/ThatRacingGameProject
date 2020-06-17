using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public  class AI_Additives : MonoBehaviour
{

    // This Method picks a random point within NextCheckpoint box collider to add randomness to cars path
    public static UnityEngine.Vector3 RandomPointInBounds(GameObject checkpoint)
    {
        return new UnityEngine.Vector3(
            UnityEngine.Random.Range(checkpoint.GetComponent<BoxCollider>().bounds.min.x+1f, checkpoint.GetComponent<BoxCollider>().bounds.max.x-1f),
            checkpoint.transform.position.y,
            UnityEngine.Random.Range(checkpoint.GetComponent<BoxCollider>().bounds.min.z, checkpoint.GetComponent<BoxCollider>().bounds.max.z));

    }
    public static float checkCollisions(Ray ray, RaycastHit hit, float rayDistance,float reaction , bool isRight,float horizontalInput)
    {
        float modifier;
        if (isRight == false)
            modifier = -1;
        else
            modifier = 1;

        if (Physics.Raycast(ray, out hit, rayDistance))
        {
            if (hit.transform.CompareTag("PlayerMesh"))
            {
                
                horizontalInput = Mathf.Clamp(horizontalInput + modifier*reaction / modifier*hit.distance / 2, -1f, 1f);

            }
            if (!hit.transform.CompareTag("PlayerMesh"))
            {
                horizontalInput = Mathf.Clamp(horizontalInput + modifier*reaction  / modifier*hit.distance / 2, -1f, 1f);
            }
        }

        return horizontalInput;
    }
}
