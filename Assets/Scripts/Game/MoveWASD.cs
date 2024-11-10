using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveWASD : MonoBehaviour
{
    public float speed;
    public int score = 0;  // Poin pemain 1
    private GameObject capturedFish = null; // Ikan yang dibawa pemain 1

    float MovementX;
    float MovementY;

    Rigidbody2D Rb;

    void Start()
    {
        Rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Reset pergerakan setiap frame agar berhenti saat tombol dilepas
        MovementX = 0;
        MovementY = 0;

        // Periksa tombol yang ditekan untuk mengatur arah movement
        if (Input.GetKey(KeyCode.W))
        {
            MovementY = 1;
        }
        if (Input.GetKey(KeyCode.S))
        {
            MovementY = -1;
        }
        if (Input.GetKey(KeyCode.A))
        {
            MovementX = -1;
        }
        if (Input.GetKey(KeyCode.D))
        {
            MovementX = 1;
        }

        // Atur kecepatan berdasarkan input
        Rb.velocity = new Vector2(MovementX * speed * Time.deltaTime, MovementY * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Menangkap ikan jika belum ada ikan yang dibawa
        if (other.CompareTag("Fish") && capturedFish == null)
        {
            capturedFish = other.gameObject;  // Menyimpan ikan yang ditangkap
            capturedFish.SetActive(false);    // Menyembunyikan ikan saat dibawa
            Debug.Log("Ikan tertangkap");
        }
        // Menyimpan ikan ke area penyimpanan jika sudah dibawa
        else if (other.CompareTag("StorageArea") && capturedFish != null)
        {
            Fish fish = capturedFish.GetComponent<Fish>();
            if (fish != null)
            {
                score += fish.pointValue; // Menambah poin berdasarkan jenis ikan
                Debug.Log("Player 2 Score: " + score);
            }

            Destroy(capturedFish);  // Menghapus ikan setelah disimpan
            capturedFish = null;    // Reset ikan yang dibawa
        }
    }
}
