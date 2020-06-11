using UnityEngine;
using System.Collections;

public class SmoothCamFollow : MonoBehaviour
{
    public Transform target;
    private new Transform camera;

    public float posSpeed = 1.0F;
    public float rotSpeed = 1.0F;

    // Use this for initialization
    void Start()
    {
        camera = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        // position movement
        camera.position = Vector3.Lerp(camera.position, target.position, (posSpeed * Time.deltaTime));

        // rotation movement
        camera.rotation = Quaternion.Lerp(camera.rotation, target.rotation, (rotSpeed * Time.deltaTime));
    }
}