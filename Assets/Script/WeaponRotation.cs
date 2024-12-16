using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    private Camera mainCam;
    private Vector3 mousePos;
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        // Get the mouse position in world space
        mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);

        // Calculate the direction vector from the object to the mouse position
        Vector3 rotation = mousePos - player.transform.position;

        // Calculate the angle in degrees
        float rotZ = (Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg) - 90f;

        // Adjust rotation based on mouse position
        if (transform.position.x > mousePos.x) // Mouse is on the left
        {
            if (rotZ < 0) // Bottom-left quadrant
            {
                rotZ = 90f;
            }
            else // Top-left quadrant
            {
                rotZ = Mathf.Clamp(rotZ, 0f, 90f);
            }
        }
        else // Mouse is on the right
        {
            rotZ = Mathf.Clamp(rotZ, -90f, 0);
        }

        //Debug.Log(rotZ); // Log the rotation value for debugging

        // Apply the rotation
        transform.rotation = Quaternion.Euler(0, 0, rotZ);
    }
}
