using UnityEngine;
using System.Collections;
using System.Runtime.CompilerServices;
using System.Reflection;
using UnityEngine.SceneManagement;
using JetBrains.Annotations;

public class Vehicle_selection : MonoBehaviour
{
    public static Vehicle_selection vehicleSelection;
    public Camera sceneCamera;
    public GameObject selectedCar;
    public GameObject nextButton;
    public gui_controller gui_Controller;

    



    void Awake()
    {
        DontDestroyOnLoad(this.gameObject.transform.root);
        //DontDestroyOnLoad(selectedCar);
    }


    private void Update()
    {
        if (sceneCamera != null)
        {
            Selection();
            ChangeCamera();
        }

    }
    public void SelectionCurrent()
    {
        gui_Controller.Change_ButtonText(nextButton, "GO!");
    }
    public void SelectionConfirmed()
    {
        DontDestroyOnLoad(selectedCar);
    }
    public void Selection()
    {
        // Used to get player selection from mouse click  using raycast hit
        RaycastHit hit;
        Ray ray = sceneCamera.ScreenPointToRay(Input.mousePosition);

        if (Input.GetMouseButtonDown(0))
        {

            if (Physics.Raycast(ray, out hit))
            {
                Transform objectHit = hit.transform;
                if (hit.transform.CompareTag("Player"))
                {
                    selectedCar = hit.transform.gameObject;
                    if (selectedCar == null);
                        SelectionCurrent();
                    
                }

            }
        }
    }
    public void ChangeCamera()
    {
        if(selectedCar != null)
        {
            GameObject CameraObj = sceneCamera.transform.gameObject;   //Take Gameobject holding camrea component
            Vector3 CameraPosition;                               //Position Destination
            CameraPosition = new Vector3(CameraObj.transform.position.x, CameraObj.transform.position.y, selectedCar.transform.position.z);
            CameraObj.transform.position = Vector3.MoveTowards(CameraObj.transform.position, CameraPosition, 0.05f);
        }
        //Moves camera to the object.

    }
}