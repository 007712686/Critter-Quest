using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class ObjectTrail : MonoBehaviour
{
    public Vector2 targetPos;
    public bool isMoving;
    public float timeToMove = 0.2f;

    private Vector2 input;
    private Animator animator;

    public Queue<Vector2> playerPositions = new Queue<Vector2>();
    public Transform playerTransform;

    private void Start()
    {
        // Assuming the player object has the tag "Player"
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        targetPos = transform.position;
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (this.gameObject.GetComponent<PetInfo>() != null)
        {
            if (this.gameObject.GetComponent<PetInfo>().following)
            {
                print("GOIN2");

                if (!isMoving && playerPositions.Count > 0)
                {
                    print("GOIN 3");

                    StartCoroutine(MoveToPosition(playerPositions.Dequeue()));
                }
                if (this.gameObject.name != "CreatureTest")
                {
                    if (isMoving)
                    {
                        input.x = UnityEngine.Input.GetAxisRaw("Horizontal");
                        input.y = UnityEngine.Input.GetAxisRaw("Vertical");
                        if (input.x != 0) input.y = 0;

                        if (input != Vector2.zero)
                        {
                            animator.SetFloat("moveX", input.x);
                            animator.SetFloat("moveY", input.y);

                            var aniPos = transform.position;
                            aniPos.x += input.x;
                            aniPos.y += input.y;
                        }

                    }

                    animator.SetBool("isMoving", isMoving);
                }
            }
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
