using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Additives : MonoBehaviour
{

    // This Method picks a random point within NextCheckpoint box collider to add randomness to cars path
    public static UnityEngine.Vector3 RandomPointInBounds(GameObject checkpoint)
    {
        return new UnityEngine.Vector3(
            UnityEngine.Random.Range(checkpoint.GetComponent<BoxCollider>().bounds.min.x+1f, checkpoint.GetComponent<BoxCollider>().bounds.max.x-1f),
            checkpoint.transform.position.y,
            UnityEngine.Random.Range(checkpoint.GetComponent<BoxCollider>().bounds.min.z, checkpoint.GetComponent<BoxCollider>().bounds.max.z));

    }


    // Smooths out values given 
    public IEnumerator SmoothValues(float v_start, float v_end, float duration)
    {
        float elapsed = 0.0f;
        while (elapsed < duration)
        {
            var speed = Mathf.Lerp(v_start, v_end, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }
        speed = v_end;
    }
}
