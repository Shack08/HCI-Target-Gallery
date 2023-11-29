using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnalogJoystickController : MonoBehaviour
{
    public float sensitivity = 200f;
    public Transform player;

    float xRotation = 0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
{
    float joystickX = Input.GetAxis("RightStickHorizontal");
    float joystickY = -Input.GetAxis("RightStickVertical");

    // Dead zone
    if (Mathf.Abs(joystickX) < 0.1f) joystickX = 0;
    if (Mathf.Abs(joystickY) < 0.1f) joystickY = 0;

    joystickX *= sensitivity * Time.deltaTime;
    joystickY *= sensitivity * Time.deltaTime;

    xRotation -= joystickY;
    xRotation = Mathf.Clamp(xRotation, -90f, 90f);

    transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
    player.Rotate(Vector3.up * joystickX);
}
}
