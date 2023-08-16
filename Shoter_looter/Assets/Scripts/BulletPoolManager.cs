using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BulletPoolManager : MonoBehaviour
{
    public static BulletPoolManager Instance;

    public GameObject bulletPrefab;
    public int poolSize = 20;

    private Queue<GameObject> bulletPool = new Queue<GameObject>();
    private int bulletsLeft; // Variable to track the number of bullets currently in the pool

    public TMP_Text bulletsLeftText; // Reference to the TMP Text element that will display the number of bullets left



    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
        {
            Debug.LogWarning("Multiple instances of BulletPoolManager found. Make sure there's only one in the scene.");
            Destroy(gameObject);
        }

        InitializeBulletPool();
    }

    private void InitializeBulletPool()
    {
        for (int i = 0; i < poolSize; i++)
        {
            GameObject bullet = Instantiate(bulletPrefab, transform);
            bullet.SetActive(false);
            bulletPool.Enqueue(bullet);
        }
        bulletsLeft = poolSize; // Set the initial number of bullets left to the pool size
        UpdateBulletsLeftText(); // Update the UI Text to display the initial number of bullets left
    }


    public GameObject GetBulletFromPool()
    {
        if (bulletPool.Count > 0)
        {
            bulletsLeft--; // Decrease the number of bullets left when getting a bullet from the pool
            UpdateBulletsLeftText(); // Update the UI Text to reflect the new number of bullets left
            return bulletPool.Dequeue();
        }

        else
        {
            Debug.LogWarning("Bullet pool exhausted. Consider increasing the pool size or handling this case differently.");
            return null;
        }
    }

    public void ReturnBulletToPool(GameObject bullet)
    {
        bulletPool.Enqueue(bullet);
        bulletsLeft++; // Increase the number of bullets left when returning a bullet to the pool
        UpdateBulletsLeftText(); // Update the UI Text to reflect the new number of bullets left
    }

    private void UpdateBulletsLeftText()
    {
        if (bulletsLeftText != null)
            bulletsLeftText.text = "Bullets Left: " + bulletsLeft;
    }
}