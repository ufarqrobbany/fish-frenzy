using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BotController : PlayerController
{
    public Transform storageArea; // Area penyimpanan ikan

    void Start()
    {
        Rb = GetComponent<Rigidbody2D>();
        Initialize();
    }

    void Update()
    {
        // Set the movement to 0 by default and handle the bot's behavior.
        MovementX = 0;
        MovementY = 0;

        HandleInput(); // Call bot-specific movement behavior
        Move(); // Move the bot using velocity
        FollowCapturedFish(); // Keep fish following bot if captured
    }

    protected override void HandleInput()
    {
        if (capturedFish == null)
        {
            // Jika belum membawa ikan, cari ikan terdekat
            GameObject nearestFish = FindNearestFish();
            if (nearestFish != null)
            {
                // Gerak ke arah ikan terdekat
                Vector2 directionToFish = ((Vector2)nearestFish.transform.position - Rb.position).normalized;
                MovementX = directionToFish.x;
                MovementY = directionToFish.y;
            } else {
                // Tidak ada ikan yang tersedia
                Rb.velocity = Vector2.zero;
            }
        }
        else
        {
            // Jika membawa ikan, arahkan ke storageArea
            Vector2 directionToStorage = ((Vector2)storageArea.position - Rb.position).normalized;
            MovementX = directionToStorage.x;
            MovementY = directionToStorage.y;
        }
    }

    // Cari ikan terdekat
    GameObject FindNearestFish()
    {
        GameObject[] fishObjects = GameObject.FindGameObjectsWithTag("Fish");
        GameObject nearestFish = null;
        float shortestDistance = Mathf.Infinity;

        foreach (GameObject fish in fishObjects)
        {
            if (fish.activeInHierarchy) // Hanya ikan yang aktif yang diperhitungkan
            {
                float distance = Vector2.Distance(transform.position, fish.transform.position);
                if (distance < shortestDistance)
                {
                    shortestDistance = distance;
                    nearestFish = fish;
                }
            }
        }

        return nearestFish;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Fish") && capturedFish == null)
        {
            // Bot menangkap ikan dan mengarahkan ke storageArea
            capturedFish = other.gameObject;
            capturedFish.GetComponent<Collider2D>().enabled = false;    // Menyembunyikan ikan saat dibawa
            Debug.Log("Ikan tertangkap oleh bot");
        }
        else if (other.CompareTag("StorageArea") && capturedFish != null)
        {
            // Add score when storing the fish
            FishController fish = capturedFish.GetComponent<FishController>();
            if (fish != null)
            {
                score += fish.pointValue;
                UpdateScoreText();
            }

            capturedFish.SetActive(false); // Nonaktifkan ikan setelah disimpan
            capturedFish.GetComponent<Collider2D>().enabled = true; // Aktifkan kembali collider jika digunakan lagi nanti
            capturedFish = null; // Reset ikan yang dibawa        }
        }
    }
}
