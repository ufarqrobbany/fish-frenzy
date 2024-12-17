using UnityEngine;
using TMPro;

public abstract class PlayerController : MonoBehaviour
{
    public float speed;
    public int score = 0;  // Poin pemain
    public TMP_Text scoreText;
    protected GameObject capturedFish = null; // Ikan yang dibawa pemain

    protected float MovementX;
    protected float MovementY;

    protected Rigidbody2D Rb;

    void Start()
    {
        Rb = GetComponent<Rigidbody2D>();
        Initialize();
    }

    void Update()
    {
         // Reset pergerakan setiap frame agar berhenti saat tombol dilepas
        MovementX = 0;
        MovementY = 0;

        HandleInput();
        Move();
        FollowCapturedFish();
    }

    protected abstract void HandleInput(); // Abstraksi untuk input spesifik

    protected void Move()
    {
        // Atur kecepatan berdasarkan input
        Rb.velocity = new Vector2(MovementX * speed * Time.deltaTime, MovementY * speed * Time.deltaTime);
    }

    // Fungsi untuk menonaktifkan kontrol
    public void DisableControl()
    {
        MovementX = 0;
        MovementY = 0;
        Rb.velocity = Vector2.zero;
    }

    protected void FollowCapturedFish()
    {
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
            capturedFish = other.gameObject; // Menyimpan ikan yang ditangkap
            capturedFish.GetComponent<Collider2D>().enabled = false; // Matikan collider ikan
            Debug.Log("Ikan tertangkap");
        }
        // Menyimpan ikan ke area penyimpanan jika sudah dibawa
        else if (other.CompareTag("StorageArea") && capturedFish != null)
        {
            FishController fish = capturedFish.GetComponent<FishController>();
            if (fish != null)
            {
                score += fish.pointValue;
                UpdateScoreText();  // Menambah poin berdasarkan jenis ikan
            }
            capturedFish.SetActive(false); // Nonaktifkan ikan setelah disimpan
            capturedFish.GetComponent<Collider2D>().enabled = true; // Aktifkan kembali collider jika digunakan lagi nanti
            capturedFish = null; // Reset ikan yang dibawa
        }
    }

    protected virtual void Initialize() { }

    protected void UpdateScoreText()
    {
        scoreText.text = score.ToString();
    }
}
