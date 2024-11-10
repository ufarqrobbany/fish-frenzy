using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish : MonoBehaviour
{
    public int pointValue;  // Nilai poin ikan (besar, sedang, kecil)

    void Start()
    {
        // Menentukan nilai poin berdasarkan jenis ikan
        if (gameObject.CompareTag("BigFish"))
        {
            pointValue = 3; // Ikan besar mendapat 3 poin
        }
        else if (gameObject.CompareTag("MediumFish"))
        {
            pointValue = 2; // Ikan sedang mendapat 2 poin
        }
        else if (gameObject.CompareTag("SmallFish"))
        {
            pointValue = 1; // Ikan kecil mendapat 1 poin
        }
    }
}
