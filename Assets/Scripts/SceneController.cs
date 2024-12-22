using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public void SetPlayerDataAndGoToScene(int humanCount, int aiCount, string sceneName)
    {
        // Set jumlah pemain manusia dan AI
        GameData.humanPlayers = humanCount;
        GameData.aiPlayers = aiCount;

        // Pindah ke scene tujuan
        SceneManager.LoadScene(sceneName);
    }

    // Fungsi tambahan tanpa parameter untuk UI
    public void GoToGameScene1Player()
    {
        SetPlayerDataAndGoToScene(1, 3, "Game"); // Contoh nilai
    }

    public void GoToGameScene2Player()
    {
        SetPlayerDataAndGoToScene(2, 2, "Game 1"); // Contoh nilai
    }
}
