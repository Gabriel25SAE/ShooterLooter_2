using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyJuggernaut : MonoBehaviour, IEnemy
{
    public int maxHealth = 200; // Maximum health of the enemy
    private int currentHealth;   // Current health of the enemy

    public Transform player;
    private NavMeshAgent agent;

    public float detectionDistance = 10f;

    public float chargeDistance = 3f; // Distance to start charging attack
    public float chargeSpeed = 10f; // Speed of the charge attack
    public float maxChargeDistance = 5f; // Maximum charging distance
    public float chargeDuration = 2f; // Duration of the charge attack
    public float stunDuration = 3f; // Duration of stun after charge attack

    private Vector3 chargeStartPosition;
    private bool isCharging = false;


    //gizmos para visualizar estas distancias
    public Color gizmoDetection = Color.red;
    public float sphereDetection;
    public Color gizmoCharge = Color.green;
    public float sphereCharge;



    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        currentHealth = maxHealth;
        sphereDetection = detectionDistance;
        sphereCharge = chargeDistance;
    }

    private void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (!isCharging && distanceToPlayer <= detectionDistance)
        {
            agent.destination = player.position;

            if (distanceToPlayer <= chargeDistance)
            {
                StartCharge();
            }
        }
        else if (!isCharging)
        {
            agent.ResetPath();
        }
    }

    private void StartCharge()
    {
        isCharging = true;
        chargeStartPosition = transform.position;

        agent.isStopped = true;
        StartCoroutine(ChargeCoroutine());
    }

    private IEnumerator ChargeCoroutine()
    {
        float startTime = Time.time;

        // Add a brief delay before starting the forward movement
        yield return new WaitForSeconds(1f); // Adjust the duration as needed

        while (Time.time - startTime < chargeDuration)
        {
            // Calculate the direction to charge (straight ahead from where the enemy stopped)
            Vector3 chargeDirection = transform.forward;

            // Calculate the distance traveled during this frame
            float distanceTraveled = chargeSpeed * Time.deltaTime;

            // Move the enemy forward by the calculated distance
            transform.position += chargeDirection * distanceTraveled;

            // Check if the maximum charging distance has been reached
            if (Vector3.Distance(transform.position, chargeStartPosition) >= maxChargeDistance)
            {
                break; // Exit the loop if the maximum distance is reached
            }

            yield return null;
        }

        agent.isStopped = false;
        isCharging = false;

        // Stun the enemy after the charge attack
        yield return new WaitForSeconds(stunDuration);

        // Resume chasing the player
        agent.destination = player.position;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = gizmoDetection;
        Gizmos.DrawWireSphere(transform.position, sphereDetection);
        Gizmos.color = gizmoCharge;
        Gizmos.DrawWireSphere(transform.position, sphereCharge);

    }

    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;

        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
}
