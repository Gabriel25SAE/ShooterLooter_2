using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float speed = 20f; // Adjust the bullet speed as needed
    public float maxDistance = 50f; // The maximum distance the bullet can travel before being returned to the pool

    private Vector3 initialPosition;

    private void OnEnable()
    {
        initialPosition = transform.position;
    }

    private void Update()
    {
        // Move the bullet forward based on its speed
        transform.Translate(Vector3.forward * speed * Time.deltaTime);

        // Check if the bullet has traveled its maximum distance
        if (Vector3.Distance(transform.position, initialPosition) >= maxDistance)
        {
            Destroy(gameObject);
        }
    }
}
