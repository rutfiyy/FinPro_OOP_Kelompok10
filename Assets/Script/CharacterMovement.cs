using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class CharacterFollowMouse : MonoBehaviour
{
    private Camera mainCam; // Reference to the main camera
    private Vector3 mousePos; // Mouse position in world space

    public GameObject weapon; // Reference to the weapon object
    public float maxSpeed = 10f; // Maximum speed of the character
    public float friction = 0.95f; // Friction factor to reduce speed over time

    private float currentSpeed = 0f; // Current speed of the character

    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        // Reference the main camera
        mainCam = Camera.main;
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // Get the mouse position in world space
        mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0; // Ensure Z-axis is ignored for 2D

        // Get the weapon's rotation in the Z-axis
        float rotZ = weapon.transform.rotation.eulerAngles.z;

        // Calculate movement direction and speed
        float direction = 0f; // Movement direction (-1 for left, 1 for right)
        float speedFactor = 0f; // Speed multiplier based on rotation

        if (Mathf.Abs(mousePos.x - transform.position.x) > 0.5f)
        {
            //Debug.Log(rotZ);
            if (rotZ >= 270f)
            {
                direction = 1f; // Move right
                speedFactor = 1 - ((rotZ - 270f) / 90f); // Normalize rotation to [0, 1]
                //Debug.Log($"Kanan {speedFactor}");
            }
            else if (rotZ > 0 && rotZ <= 90f)
            {
                direction = -1f; // Move left
                speedFactor = rotZ / 90f; // Normalize rotation to [0, 1]
                //Debug.Log($"Kiri {speedFactor}");
            }

            Mathf.Clamp(speedFactor, 0, 0.5f);
        }
        else
        {
            direction = 0;
            speedFactor = 0;
        }

        // Calculate current speed
        currentSpeed = Mathf.Lerp(currentSpeed, maxSpeed * speedFactor, Time.deltaTime);
        //currentSpeed = Mathf.Lerp(currentSpeed, maxSpeed, Time.deltaTime);

        // Apply friction to gradually reduce speed
        currentSpeed *= friction;

        // Calculate movement step
        rb.velocity = new Vector2(currentSpeed * direction, rb.velocity.y);
    }
}
