using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    [SerializeField] private float GridSize = 1f;
    void Start()
    {
        Vector2 GridPosition = new Vector2(
            Mathf.Round(transform.position.x / GridSize),
            Mathf.Round(transform.position.z / GridSize)
        );

        Debug.Log($"Hi I am at: {GridPosition}");
    }

}
