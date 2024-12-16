using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MoveArrows : MonoBehaviour
{
    public float speed;
    public int score = 0;  // Poin pemain 2
    public TMP_Text scoreText;
    private GameObject capturedFish = null; // Ikan yang dibawa pemain 2

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
        if (Input.GetKey(KeyCode.UpArrow))
        {
            MovementY = 1;
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            MovementY = -1;
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            MovementX = -1;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            MovementX = 1;
        }

        // Atur kecepatan berdasarkan input
        Rb.velocity = new Vector2(MovementX * speed * Time.deltaTime, MovementY * speed * Time.deltaTime);

        if(capturedFish  != null) 
        {
            capturedFish.transform.position = transform.position + new Vector3(0, 0.5f, 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Menangkap ikan jika belum ada ikan yang dibawa
        if (other.CompareTag("Fish") && capturedFish == null)
        {
            capturedFish = other.gameObject;  // Menyimpan ikan yang ditangkap
            capturedFish.GetComponent<Collider2D>().enabled = false;    // Menyembunyikan ikan saat dibawa
            Debug.Log("Ikan tertangkap");
        }
        // Menyimpan ikan ke area penyimpanan jika sudah dibawa
        else if (other.CompareTag("StorageArea") && capturedFish != null)
        {
            FishController fish = capturedFish.GetComponent<FishController>();
            if (fish != null)
            {
                score += fish.pointValue; // Menambah poin berdasarkan jenis ikan
                UpdateScoreText();
            }

            capturedFish.SetActive(false);  // Menghapus ikan setelah disimpan
            capturedFish.GetComponent<Collider2D>().enabled = false;
            capturedFish = null;    // Reset ikan yang dibawa
        }
    }

    void UpdateScoreText()
    {
        scoreText.text = score.ToString();
    }
}