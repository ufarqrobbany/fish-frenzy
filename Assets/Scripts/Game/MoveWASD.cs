using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MoveWASD : MonoBehaviour
{
    public float speed;
    public int score = 0;  // Poin pemain 1
    public TMP_Text scoreText;
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

        // Posisi ikan mengikuti pemain jika ada yang tertangkap
        if (capturedFish != null)
        {
            capturedFish.transform.position = transform.position + new Vector3(0, -0.5f, 0); // Tempelkan ikan di bawah pemain
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Menangkap ikan jika belum ada ikan yang dibawa
        if (other.CompareTag("Fish") && capturedFish == null)
        {
            capturedFish = other.gameObject;  // Menyimpan ikan yang ditangkap
            capturedFish.GetComponent<Collider2D>().enabled = false; // Matikan collider ikan
            Debug.Log("Ikan tertangkap");
        }
        // Menyimpan ikan ke area penyimpanan jika sudah dibawa
        else if (other.CompareTag("StorageArea") && capturedFish != null)
        {
            Fish fish = capturedFish.GetComponent<Fish>();
            if (fish != null)
            {
                score += fish.pointValue; // Menambah poin berdasarkan jenis ikan
                UpdateScoreText();
            }

            capturedFish.SetActive(false); // Nonaktifkan ikan setelah disimpan
            capturedFish.GetComponent<Collider2D>().enabled = true; // Aktifkan kembali collider jika digunakan lagi nanti
            capturedFish = null; // Reset ikan yang dibawa
        }
    }

    void UpdateScoreText()
    {
        scoreText.text = score.ToString();
    }
}
