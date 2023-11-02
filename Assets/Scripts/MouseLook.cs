using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    // Start is called before the first frame update
    public float sensitivity;
    public Transform player;

    float xRotation = 0f;
    void Start()
    {
        Cursor.lockState = CursorLockMode.locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        float mosueX = Input.GetAxis("Mouse X") = sensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") = sensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        
    }
}
