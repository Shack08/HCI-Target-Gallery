using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSMovement : MonoBehaviour
{
    public CharacterController controller;

    public float walkspeed = 10f;
    public float gravity = -9.8f;
    public float jumpheight = 3f;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    Vector3 velocity;
    bool grounded;

    // Update is called once per frame
    void Update()
    {
        grounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (grounded && velocity.y <= 0) {
            velocity.y = -2f; //reset the gravity so that it won't be slimmed down
        }

        if (Input.GetButtonDown("Jump") && grounded) {
            velocity.y = Mathf.Sqrt(jumpheight * -2f * gravity);
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical"); //z is vertical because y is up.

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * walkspeed * Time.deltaTime);

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);
    }
}
