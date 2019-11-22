using System;
using UnityEngine;

public class MoveController : MonoBehaviour
{
    [SerializeField] private float GridSize = 1f;
    [SerializeField] private float AnimationSpeed = 1f;

    private Vector3 targetWorldPosition;
    private Vector2 OldGridPosition;
    private Vector2 GridPosition;
    private Vector3 GridOffset = new Vector3(0.5f, 0f, 0.5f);

    void Start()
    {
        GridPosition = new Vector2(
            Mathf.Round(transform.position.x / GridSize),
            Mathf.Round(transform.position.z / GridSize)
        );
        targetWorldPosition = new Vector3(GridPosition.x, 0f, GridPosition.y) + GridOffset;
    }

    void Update()
    {
        ProcessInput();

        UpdatePosition();
    }

    private void UpdatePosition()
    {
        if ((transform.position - targetWorldPosition).magnitude > 0.01f)
        {
            transform.position = Vector3.Lerp(transform.position, targetWorldPosition, AnimationSpeed * Time.deltaTime);
        }
        else
        {
            transform.position = targetWorldPosition;
        }
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.CompareTag("obstacles"))
        {
            Debug.Log("Collision Detected!!! " + col.gameObject);
            GridPosition = OldGridPosition;
            targetWorldPosition = new Vector3(GridPosition.x, 0f, GridPosition.y) + GridOffset;
        }
    }

    private void ProcessInput()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            Move(Vector2.up);
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            Move(Vector2.down);
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            Move(Vector2.left);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            Move(Vector2.right);
        }
    }

    private void Move(Vector2 gridMovement)
    {
        OldGridPosition = GridPosition;
        GridPosition += gridMovement;
        targetWorldPosition = new Vector3(GridPosition.x, 0f, GridPosition.y) + GridOffset;
    }
}
