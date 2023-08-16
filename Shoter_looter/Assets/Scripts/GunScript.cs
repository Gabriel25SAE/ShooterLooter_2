using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class GunScript : MonoBehaviour
{
    public Transform firePoint; // The position where the bullets will be spawned
    public GameObject bulletPrefab;

    // You can adjust fire rate and other properties as needed
    public float fireRate = 0.2f;

    // Variable to check if the gun is currently firing
    private bool isFiring = false;

    private void Update()
    {
        // Start the firing coroutine while the Fire1 button is held down
        if (Input.GetButton("Fire1") && !isFiring)
        {
            StartCoroutine(FireCoroutine());
        }
    }

    private IEnumerator FireCoroutine()
    {
        // Set the flag to indicate the gun is currently firing
        isFiring = true;

        // Fire the first bullet immediately upon pressing Fire1
        FireBullet();

        // Wait for the specified fire rate time before firing the next bullet
        yield return new WaitForSeconds(fireRate);

        // Reset the flag to indicate the gun has finished firing
        isFiring = false;
    }

    private void FireBullet()
    {
        // Use object pooling to get a bullet from the pool
        GameObject bullet = BulletPoolManager.Instance.GetBulletFromPool();
        if (bullet != null)
        {
            bullet.transform.position = firePoint.position;
            bullet.transform.rotation = firePoint.rotation;
            bullet.SetActive(true);
        }
    }
}