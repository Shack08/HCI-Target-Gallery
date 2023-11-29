using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JoyShockLibrary;

public class GyroAim : MonoBehaviour
{
    // Sensitivity of the gyroscope
    public float sensitivity = 200f;

    // Player and camera objects
    public Transform player;
    public Camera playerCamera;

    // Device ID and gyro data queues
    private int deviceId;
    private Queue<float> gyroXQueue = new Queue<float>();
    private Queue<float> gyroYQueue = new Queue<float>();
    // Threshold for ignoring small movements
private const float gyroThreshold = 0.01f;

    // Size of the gyro data queues
    private const int queueSize = 10;

    void Start()
    {
        // Connect to the JoyShock devices
        JSL.JslConnectDevices();

        // Lock and hide the cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        // Get the connected device handles
        int[] deviceHandles = new int[4];
        int deviceCount = JSL.JslGetConnectedDeviceHandles(deviceHandles, 4);
        Debug.Log(deviceId);

        // Get the gyro data and invert it
        float gyroY = -JSL.JslGetGyroX(deviceId);
        float gyroX = -JSL.JslGetGyroY(deviceId);

        // Map the gyroscope data to the range [-1, 1] to control the sensitivity
        gyroX = Mathf.Clamp(gyroX / sensitivity, -1, 1);
        gyroY = Mathf.Clamp(gyroY / sensitivity, -1, 1);

         // Ignore small movements
    if (Mathf.Abs(gyroX) < gyroThreshold) gyroX = 0;
    if (Mathf.Abs(gyroY) < gyroThreshold) gyroY = 0;

        // Add the gyro data to the queues
        gyroXQueue.Enqueue(gyroX);
        gyroYQueue.Enqueue(gyroY);

        // Remove old data from the queues
        if (gyroXQueue.Count > queueSize) gyroXQueue.Dequeue();
        if (gyroYQueue.Count > queueSize) gyroYQueue.Dequeue();

        // Calculate the average gyro data
        gyroX = Average(gyroXQueue);
        gyroY = Average(gyroYQueue);

        // Log the gyro data
        Debug.Log("GyroX: " + gyroX + ", GyroY: " + gyroY);

 // Rotate the camera based on the gyro data
    if (gyroX != 0 || gyroY != 0)
    {
        // Rotate the camera based on the gyro data
        playerCamera.transform.Rotate(gyroY * sensitivity * Time.deltaTime, 0, 0);
        playerCamera.transform.Rotate(0, gyroX * sensitivity * Time.deltaTime, 0);
    }
    }

    // Function to calculate the average of a queue of floats
    float Average(Queue<float> queue)
    {
        float sum = 0f;
        foreach (float f in queue)
        {
            sum += f;
        }
        return sum / queue.Count;
    }
}