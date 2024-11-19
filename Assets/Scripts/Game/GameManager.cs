using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public float gameDuration = 60f; // Waktu permainan dalam detik
    public TMP_Text timerText; // Referensi ke UI TextMeshPro untuk timer
    public GameObject endGamePanel; // Panel pop-up akhir game
    public TMP_Text winnerText; // Referensi ke UI TextMeshPro untuk menampilkan pemenang
    public GameObject[] players; // Semua pemain (Player + Bot)
    public FishSpawner fishSpawner; // Referensi ke script spawner ikan

    private bool gameEnded = false; // Menandakan apakah game sudah berakhir
    private float remainingTime; // Waktu tersisa dalam game

    void Start()
    {
        remainingTime = gameDuration;

        // Pastikan elemen UI diinisialisasi
        if (endGamePanel != null)
            endGamePanel.SetActive(false); // Sembunyikan panel hasil game di awal

        if (timerText != null)
            timerText.text = $"Time: {gameDuration:00}:00";
    }

    void Update()
    {
        if (gameEnded) return;

        // Kurangi waktu berdasarkan deltaTime
        remainingTime -= Time.deltaTime;

        // Update UI Timer
        if (timerText != null)
        {
            int minutes = Mathf.FloorToInt(remainingTime / 60f);
            int seconds = Mathf.FloorToInt(remainingTime % 60f);
            timerText.text = $"Time: {minutes:00}:{seconds:00}";
        }

        // Jika waktu habis, akhiri permainan
        if (remainingTime <= 0f)
        {
            EndGame();
        }
    }

    void EndGame()
    {
        gameEnded = true;

        // Hentikan semua pemain dan bot
        foreach (GameObject player in players)
        {
            if (player != null)
            {
                if (player.GetComponent<MoveWASD>() != null)
                    player.GetComponent<MoveWASD>().enabled = false;

                if (player.GetComponent<MoveArrows>() != null)
                    player.GetComponent<MoveArrows>().enabled = false;

                if (player.GetComponent<BotController>() != null)
                    player.GetComponent<BotController>().enabled = false;
            }
        }

        // Hentikan spawner ikan
        if (fishSpawner != null)
            fishSpawner.enabled = false;

        // Hancurkan semua ikan yang tersisa
        foreach (GameObject fish in GameObject.FindGameObjectsWithTag("Fish"))
        {
            Destroy(fish);
        }

        // Tentukan pemenang berdasarkan skor
        string winner = DetermineWinner();

        // Tampilkan hasil
        if (endGamePanel != null)
            endGamePanel.SetActive(true);

        if (winnerText != null)
            winnerText.text = $"Winner: {winner}";
    }

    string DetermineWinner()
    {
        string winnerName = "No Winner";
        int highestScore = -1;

        foreach (GameObject player in players)
        {
            if (player != null)
            {
                int playerScore = 0;

                // Ambil skor dari komponen yang relevan
                if (player.GetComponent<MoveWASD>() != null)
                    playerScore = player.GetComponent<MoveWASD>().score;

                else if (player.GetComponent<MoveArrows>() != null)
                    playerScore = player.GetComponent<MoveArrows>().score;

                else if (player.GetComponent<BotController>() != null)
                    playerScore = player.GetComponent<BotController>().score;

                // Tentukan pemain dengan skor tertinggi
                if (playerScore > highestScore)
                {
                    highestScore = playerScore;
                    winnerName = player.name;
                }
            }
        }

        return winnerName;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
