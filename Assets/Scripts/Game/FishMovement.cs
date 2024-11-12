using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishMovement : MonoBehaviour
{
    public float speed = 1.5f;
    public Vector2 areaLimit = new Vector2(8f, 4.5f);
    private Vector2 targetPosition;

    void Start()
    {
        SetRandomTargetPosition();
    }

    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        if (Vector2.Distance(transform.position, targetPosition) < 0.1f)
        {
            SetRandomTargetPosition();
        }
    }

    void SetRandomTargetPosition()
    {
        float randomX = Random.Range(-areaLimit.x, areaLimit.x);
        float randomY = Random.Range(-areaLimit.y, areaLimit.y);
        targetPosition = new Vector2(randomX, randomY);
    }
}

