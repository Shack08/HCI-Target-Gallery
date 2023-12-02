using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ChangeControls : MonoBehaviour
{
    // Reference to your three input components
    public MouseLook mouseLookScript;
    public AnalogJoystickController analogJoystickControllerScript;
    public GyroAim gyroAimScript;

    public TextMeshProUGUI controllerTypeText;

    // Array to hold the components
    private MonoBehaviour[] inputComponents;

    // Variable to track the current active input component
    // private int currentInputIndex = 0;

    void Start()
    {
        controllerTypeText.text = "";
        // Initialize the array
        inputComponents = new MonoBehaviour[] {  mouseLookScript, analogJoystickControllerScript, gyroAimScript};
         // Load the controller type
        int controllerIndex = PlayerPrefs.GetInt("ControllerIndex", 0);
        // Activate the initial input component
        SwitchInputComponent(controllerIndex);
    }

    void Update()
    {
        // // Check for button press (you can customize this based on your input system)
        // if (Input.GetKeyDown(KeyCode.Space))
        // {
        //     // Toggle to the next input component
        //     currentInputIndex = (currentInputIndex + 1) % 3;
        //     SwitchInputComponent(currentInputIndex);
        // }
    }

    // Helper method to activate the specified input component
    public void SwitchInputComponent(int index)
    {
        // Deactivate all components
        foreach (var component in inputComponents)
        {
            component.enabled = false;
        }

        // Activate the selected component
        inputComponents[index].enabled = true;

        controllerTypeText.text = "Controller Type: " + inputComponents[index].GetType().Name;
          // Log the name of the active component to the console
        Debug.Log(inputComponents[index].GetType().Name + " is now active.");
    }
}
