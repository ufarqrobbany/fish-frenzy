using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishSpawner : MonoBehaviour
{
    public GameObject goldFishPrefab;    // Prefab ikan emas (10%)
    public GameObject bigFishPrefab; // Prefab ikan besar (30%)
    public GameObject smallFishPrefab;  // Prefab ikan kecil (45%)
    public GameObject poisonFishPrefab;  // Prefab ikan racun (15%)

    public Vector2 spawnArea = new Vector2(7f, 4f);
    public int maxFish = 7;
    public float spawnInterval = 4f;

    private List<GameObject> spawnedFish = new List<GameObject>();

    void Start()
    {
        StartCoroutine(SpawnFish());
        StartCoroutine(CleanUpInactiveFish());
    }

    IEnumerator CleanUpInactiveFish()
    {
        while (true)
        {
            spawnedFish.RemoveAll(fish => fish == null); // Pastikan daftar tetap bersih

            foreach (GameObject fish in spawnedFish)
            {
                if (fish != null && !fish.activeInHierarchy)
                {
                    Destroy(fish); // Hapus fish clone yang tidak aktif
                }
            }
            yield return new WaitForSeconds(10f); // Jalankan setiap 10 detik
        }
    }

    IEnumerator SpawnFish()
    {
        while (true)
        {
            // Hapus objek yang hilang atau sudah diambil dari daftar
            spawnedFish.RemoveAll(fish => fish == null || !fish.activeInHierarchy);

            // Cek apakah masih bisa menambah ikan baru
            if (spawnedFish.Count < maxFish)
            {
                int fishToSpawn = Mathf.Min(4, maxFish - spawnedFish.Count);
                for (int i = 0; i < fishToSpawn; i++)
                {

                    Vector2 spawnPos = new Vector2(Random.Range(-spawnArea.x, spawnArea.x), Random.Range(-spawnArea.y, spawnArea.y));
                    GameObject fishPrefab = GetRandomFishPrefab();

                    // Pastikan prefab tidak null sebelum di-spawn
                    if (fishPrefab != null)
                    {
                        GameObject fish = Instantiate(fishPrefab, spawnPos, Quaternion.identity);
                        fish.SetActive(true);
                        spawnedFish.Add(fish);
                    }
                    else
                    {
                        Debug.LogWarning("Fish prefab is null! Please check the prefab assignments.");
                    }
                }
            }
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    GameObject GetRandomFishPrefab()
    {
        float randomValue = Random.Range(0f, 100f); // Nilai acak antara 0 dan 100

        if (randomValue < 10f)
        {
            // 10% kemungkinan ikan emas
            return goldFishPrefab;
        }
        else if (randomValue < 40f) // 10% + 30% = 40%
        {
            // 30% kemungkinan ikan besar
            return bigFishPrefab;
        }
        else if (randomValue < 85f) // 40% + 45% = 85%
        {
            // 45% kemungkinan ikan kecil
            return smallFishPrefab;
        }
        else
        {
            // 15% kemungkinan ikan racun
            return poisonFishPrefab;
        }
    }


}
