using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControlSelectButtonHandler : MonoBehaviour
{
    public int controllerIndex; // Index of the controller to activate
    public string sceneName; // Name of the scene to load

    // Method to call when the button is clicked
    public void OnButtonClick()
    {
        // Save the controller type
        PlayerPrefs.SetInt("ControllerIndex", controllerIndex);

        // Load the new scene
        SceneManager.LoadScene(sceneName);
    }
}
