using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceshipController : MonoBehaviour
{
    public float speed = 10f;
    public float rotationAmount = 45f; // Amount of rotation in degrees

    private Camera mainCamera;
    private float xMin, xMax, yMin, yMax;
    private Transform visualRotationTransform; // Transform for visual rotation

    private void Start()
    {
        mainCamera = Camera.main;
        SetCameraBounds();
        rotationAmount = rotationAmount * -1;

        // Create an empty GameObject as a child of the spaceship to handle visual rotation
        GameObject visualRotationGO = new GameObject("VisualRotation");
        visualRotationTransform = visualRotationGO.transform;
        visualRotationTransform.SetParent(transform); // Set it as a child of the spaceship
    }

    private void SetCameraBounds()
    {
        float halfWidth = mainCamera.aspect * mainCamera.orthographicSize;
        float halfHeight = mainCamera.orthographicSize;

        xMin = mainCamera.transform.position.x - halfWidth;
        xMax = mainCamera.transform.position.x + halfWidth;
        yMin = mainCamera.transform.position.y - halfHeight;
        yMax = mainCamera.transform.position.y + halfHeight;
    }

    private void Update()
    {
        // Get input from horizontal and vertical axes
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Rotate the spaceship based on horizontal and vertical input (visual rotation)
        RotateShipVisual(horizontalInput, verticalInput);

        // Move the spaceship based on horizontal and vertical input
        Vector3 movement = new Vector3(horizontalInput, verticalInput, 0f);
        transform.Translate(movement * speed * Time.deltaTime);

        // Clamp the spaceship's position within the camera boundaries
        float clampedX = Mathf.Clamp(transform.position.x, xMin, xMax);
        float clampedY = Mathf.Clamp(transform.position.y, yMin, yMax);
        transform.position = new Vector3(clampedX, clampedY, transform.position.z);
    }

    private void RotateShipVisual(float horizontalInput, float verticalInput)
    {
        // Calculate the target rotation angles based on the horizontal and vertical input
        float targetRotationX = verticalInput * rotationAmount;
        float targetRotationY = horizontalInput * rotationAmount;

        // Apply the rotation angles
        visualRotationTransform.localRotation = Quaternion.Euler(targetRotationX, targetRotationY, 0f);
    }
}