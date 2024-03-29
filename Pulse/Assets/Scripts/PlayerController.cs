﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    CharacterController characterController;

    public float speed = 6.0f;
    public float jumpSpeed = 8.0f;
    public float gravity = 20.0f;
    public static bool checkpoint;

    private Vector3 moveDirection = Vector3.zero;
    private float rotationFrame;
    private bool finalGround;

    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        rotationFrame = 0;
        finalGround = false;
        checkpoint = false;
    }

    // Update is called once per frame
    void Update()
    {

        if (characterController.isGrounded)
        {
            // We are grounded, so recalculate
            // move direction directly from axes

            moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical"));
            moveDirection *= speed;

            if (Input.GetButton("Jump") && (transform.position.z < 45 || transform.position.z > 125))
            {
                moveDirection.y = jumpSpeed;
            }
        }

        // Apply gravity. Gravity is multiplied by deltaTime twice (once here, and once below
        // when the moveDirection is multiplied by deltaTime). This is because gravity should be applied
        // as an acceleration (ms^-2)
        moveDirection.y -= gravity * Time.deltaTime;

        if (transform.position.y < -5 && transform.position.z < 55)
        {
            SceneManager.LoadScene("Level1");
        }

        if (transform.position.z > 55 && rotationFrame < 45)
        {
            // Rotate the cube by converting the angles into a quaternion.
            Quaternion target = Quaternion.Euler(rotationFrame, 0, 0);

            // Dampen towards the target rotation
            transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime * 5);
            moveDirection.y -= gravity * Time.deltaTime;

            rotationFrame++;
        }

        if (rotationFrame >= 45 && !finalGround)
        {
            moveDirection = new Vector3(Input.GetAxis("Horizontal"), -1, 1);
            moveDirection *= speed;
            characterController.Move(moveDirection * Time.deltaTime);
        } else
        {
            // Move the controller
            characterController.Move(moveDirection * Time.deltaTime);
        }

        if (checkpoint)
        {
            transform.position = new Vector3(3, -20, 60.7f);
            checkpoint = false;
        }

        if (transform.position.y < -100 && transform.position.z > 55)
        {
            transform.position = new Vector3(3, -91, 130);
            Quaternion target = Quaternion.Euler(0, 0, 0);
            transform.rotation = target;
            finalGround = true;
            PlatformController.finalStart = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "FinalGround")
        {
            finalGround = true;
            Quaternion target = Quaternion.Euler(0, 0, 0);
            transform.rotation = target;
            PlatformController.finalStart = true;
        }
    }
}
