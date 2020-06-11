using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class gui_controller : MonoBehaviour
{
    public static gui_controller gui_Controller;
    public void Next_Scene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void Change_ButtonText(GameObject Button,string newText)
    {
        Debug.Log("change_buttonText");
        TextMeshProUGUI textObject;
        textObject = Button.transform.GetChild(Button.transform.childCount-1).gameObject.GetComponent<TextMeshProUGUI>();
        Debug.Log(Button.transform.GetChild(0));
        Debug.Log(textObject);

        textObject.text = newText;
    }
    public int CheckScene()
    {
        int currentScene;
        currentScene = SceneManager.GetActiveScene().buildIndex;
        return currentScene;
    }

}