using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTiltController : MonoBehaviour
{
    public Transform spaceshipTransform; // Reference to the spaceship's transform
    public float tiltSpeed = 5f; // Speed of the camera tilt
    public float maxTiltAngle = 10f; // Maximum tilt angle of the camera
    public float centerThreshold = 0.1f; // Threshold distance from the center to reset tilt

    private float currentTiltAngleX = 0f; // Current tilt angle around X-axis
    private float currentTiltAngleY = 0f; // Current tilt angle around Y-axis

    private void Update()
    {
        // Calculate the target tilt angles based on the spaceship's input
        float targetTiltAngleX = Mathf.Clamp(spaceshipTransform.position.y, -1f, 1f) * maxTiltAngle;
        float targetTiltAngleY = Mathf.Clamp(spaceshipTransform.position.x, -1f, 1f) * maxTiltAngle;

        // Reset tilt angles if spaceship is near the center of the screen
        if (Mathf.Abs(spaceshipTransform.position.x) < centerThreshold && Mathf.Abs(spaceshipTransform.position.y) < centerThreshold)
        {
            // Smoothly interpolate the current tilt angles towards zero (neutral position)
            currentTiltAngleX = Mathf.Lerp(currentTiltAngleX, 0f, tiltSpeed * Time.deltaTime);
            currentTiltAngleY = Mathf.Lerp(currentTiltAngleY, 0f, tiltSpeed * Time.deltaTime);
        }
        else
        {
            // Smoothly interpolate the current tilt angles towards the target tilt angles
            currentTiltAngleX = Mathf.Lerp(currentTiltAngleX, -targetTiltAngleX, tiltSpeed * Time.deltaTime);
            currentTiltAngleY = Mathf.Lerp(currentTiltAngleY, -targetTiltAngleY, tiltSpeed * Time.deltaTime);
        }

        // Apply the tilt angles to the camera
        Vector3 targetEulerAngles = new Vector3(currentTiltAngleX, 0f, currentTiltAngleY);
        transform.localRotation = Quaternion.Euler(targetEulerAngles);
    }
}