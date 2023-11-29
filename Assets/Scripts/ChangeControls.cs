using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeControls : MonoBehaviour
{
    // Reference to your three input components
    public MouseLook mouseLookScript;
    public AnalogJoystickController analogJoystickControllerScript;
    public GyroAim gyroAimScript;

    // Array to hold the components
    private MonoBehaviour[] inputComponents;

    // Variable to track the current active input component
    private int currentInputIndex = 0;

    void Start()
    {
        // Initialize the array
        inputComponents = new MonoBehaviour[] {  mouseLookScript, analogJoystickControllerScript, gyroAimScript};

        // Activate the initial input component
        SwitchInputComponent(currentInputIndex);
    }

    void Update()
    {
        // Check for button press (you can customize this based on your input system)
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // Toggle to the next input component
            currentInputIndex = (currentInputIndex + 1) % 3;
            SwitchInputComponent(currentInputIndex);
        }
    }

    // Helper method to activate the specified input component
    void SwitchInputComponent(int index)
    {
        // Deactivate all components
        foreach (var component in inputComponents)
        {
            component.enabled = false;
        }

        // Activate the selected component
        inputComponents[index].enabled = true;

          // Log the name of the active component to the console
        Debug.Log(inputComponents[index].GetType().Name + " is now active.");
    }
}
