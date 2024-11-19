using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BotController : MonoBehaviour
{
    public float speed;
    public int score = 0;
    public TMP_Text scoreText;
    private GameObject capturedFish = null; // Ikan yang ditangkap
    public Transform storageArea; // Area penyimpanan ikan

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (capturedFish == null)
        {
            // Jika belum membawa ikan, cari ikan terdekat
            GameObject nearestFish = FindNearestFish();
            if (nearestFish != null)
            {
                // Gerak ke arah ikan terdekat
                Vector2 directionToFish = ((Vector2)nearestFish.transform.position - rb.position).normalized;
                rb.velocity = directionToFish * speed * Time.deltaTime;
            }
            else
            {
                // Tidak ada ikan yang tersedia
                rb.velocity = Vector2.zero;
            }
        }
        else
        {
            // Jika membawa ikan, arahkan ke storageArea
            Vector2 directionToStorage = ((Vector2)storageArea.position - rb.position).normalized;
            rb.velocity = directionToStorage * speed * Time.deltaTime;
            capturedFish.transform.position = transform.position + new Vector3(0, 0.5f, 0);
        }
    }

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
            Fish fish = capturedFish.GetComponent<Fish>();
            if (fish != null)
            {
                score += fish.pointValue;
                UpdateScoreText();
            }

            capturedFish.SetActive(false); // Nonaktifkan ikan setelah disimpan
            capturedFish.GetComponent<Collider2D>().enabled = true; // Aktifkan kembali collider jika digunakan lagi nanti
            capturedFish = null; // Reset ikan yang dibawa        }
        }
        void UpdateScoreText()
        {
            scoreText.text = score.ToString();
        }
    }
}
