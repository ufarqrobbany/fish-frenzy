using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishSpawner : MonoBehaviour
{
    public GameObject bigFishPrefab;    // Prefab ikan besar (30%)
    public GameObject mediumFishPrefab; // Prefab ikan sedang (35%)
    public GameObject smallFishPrefab;  // Prefab ikan kecil (35%)

    public Vector2 spawnArea = new Vector2(7f, 4f);
    public int maxFish = 7;
    public float spawnInterval = 4f;

    private List<GameObject> spawnedFish = new List<GameObject>();

    void Start()
    {
        StartCoroutine(SpawnFish());
    }

    IEnumerator SpawnFish()
    {
        while (true)
        {
            // Hapus objek yang hilang atau sudah diambil dari daftar
            spawnedFish.RemoveAll(fish => fish == null);

            // Cek apakah masih bisa menambah ikan baru
            if (spawnedFish.Count < maxFish)
            {
                Vector2 spawnPos = new Vector2(Random.Range(-spawnArea.x, spawnArea.x), Random.Range(-spawnArea.y, spawnArea.y));
                GameObject fish = Instantiate(GetRandomFishPrefab(), spawnPos, Quaternion.identity);
                spawnedFish.Add(fish);
            }
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    GameObject GetRandomFishPrefab()
    {
        float randomValue = Random.Range(0f, 100f); // Nilai acak antara 0 dan 100

        if (randomValue < 30f)
        {
            // 30% kemungkinan ikan besar
            return bigFishPrefab;
        }
        else if (randomValue < 35f)
        {
            // 35% kemungkinan ikan sedang
            return mediumFishPrefab;
        }
        else
        {
            // 35% kemungkinan ikan kecil
            return smallFishPrefab;
        }
    }
}
