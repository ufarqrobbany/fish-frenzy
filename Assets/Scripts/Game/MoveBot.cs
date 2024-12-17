using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MoveBot : PlayerController
{
    public Transform storageArea; // Area penyimpanan ikan

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

  
}
