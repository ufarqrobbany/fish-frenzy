using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishController : MonoBehaviour
{
    // Properti untuk nilai poin ikan
    [SerializeField] private int pointValue;  // Akan terlihat di Inspector, tapi tetap private

    public int PointValue
    {
        get { return pointValue; }
        set { pointValue = Mathf.Max(0, value); } // Menjamin nilai tidak negatif
    }

    public float speed = 1.5f;
    public Vector2 areaLimit = new Vector2(8f, 4.5f);
    private Vector2 targetPosition;

    void Start()
    {
        SetRandomTargetPosition();
    }

    void Update()
    {
        MoveFish();
    }

    

    // Metode untuk menggerakkan ikan ke posisi target
    void MoveFish()
    {
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        if (Vector2.Distance(transform.position, targetPosition) < 0.1f)
        {
            SetRandomTargetPosition();
        }
    }

    // Metode untuk menentukan posisi target secara acak
    void SetRandomTargetPosition()
    {
        float randomX = Random.Range(-areaLimit.x, areaLimit.x);
        float randomY = Random.Range(-areaLimit.y, areaLimit.y);
        targetPosition = new Vector2(randomX, randomY);
    }
    
}
