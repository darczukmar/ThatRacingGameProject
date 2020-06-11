using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build.Reporting;
using UnityEngine;

public class spawn_vehicle : MonoBehaviour
{

    


    public Transform SpawnPosition;  // coords for spawning object
    public Vehicle_selection player_selection;  // checking what object what selected by player to spawn
    public GameObject Spawned; // what to spawn

    private Camera player_camera;
    private Car_controller player_controller;

    void Start()
    {
        player_selection = GetComponent<Vehicle_selection>();
        player_selection = GameObject.FindGameObjectWithTag("LogicHandler").GetComponent<Vehicle_selection>();
        Spawned = player_selection.selectedCar;

        MoveObject(Spawned,SpawnPosition.transform.position);
        EnableComponents();
    }


    void MoveObject(GameObject objectTarget, UnityEngine.Vector3 locationTarget)
    {
        //Doesn't actually spawn anything , just moves the object to spawn point.
        objectTarget.transform.position = locationTarget;

    }
    void EnableComponents()

    {
        player_camera = Spawned.transform.GetComponentInChildren<Camera>();
        player_controller = Spawned.GetComponent<Car_controller>();
        player_controller.enabled = true;
        player_camera.enabled = true;
    }

}
