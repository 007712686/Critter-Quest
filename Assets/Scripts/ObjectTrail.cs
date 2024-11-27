using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectTrail : MonoBehaviour
{
    private Vector2 targetPos;
    private bool isMoving;
    private float timeToMove = 0.2f;

    private Queue<Vector2> playerPositions = new Queue<Vector2>();
    private Transform playerTransform;

    private void Start()
    {
        // Assuming the player object has the tag "Player"
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        targetPos = transform.position;
    }

    private void Update()
    {
        if (!isMoving && playerPositions.Count > 0)
        {
            StartCoroutine(MoveToPosition(playerPositions.Dequeue()));
        }
    }

    public void UpdateTrailPosition(Vector2 newPosition)
    {
        playerPositions.Enqueue(newPosition);
    }

    private IEnumerator MoveToPosition(Vector2 newPosition)
    {
        isMoving = true;
        float elapsedTime = 0;
        Vector2 startPos = transform.position;

        while (elapsedTime < timeToMove)
        {
            transform.position = Vector2.Lerp(startPos, newPosition, (elapsedTime / timeToMove));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = newPosition;
        isMoving = false;
    }
}