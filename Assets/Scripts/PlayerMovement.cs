using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Start is called before the first frame update
    bool isMoving;
    Vector2 origPos, targetPos;
    float timeToMove = .2f;
    Animator playerAnim;
    [SerializeField]
    KeyCode Up, Down, Left, Right;
    [SerializeField]
    public int direction;
    [SerializeField]
    bool pauseWorld = false;

    private void Start()
    {
        GameManager.Instance.setPlayer(this.gameObject);
        playerAnim = this.gameObject.GetComponentInChildren<Animator>();
    }
    public void setPauseWorld(bool x)
    {
        pauseWorld = x;
    }
    public bool getPauseWorld()
    {
        return pauseWorld;
    }

    void Update()
    {
        if (!pauseWorld)
        {
            if (!isMoving)
            {
                playerAnim.SetBool("IsMoving", false);
            }
            if (Input.GetKey(Up) && !isMoving)
            {
                direction = 1;
                if (playerAnim.GetInteger("Direction") != 1)
                    playerAnim.SetInteger("Direction", 1);
                if (this.gameObject.GetComponent<DetectWalls>().returnUpWall() != true)
                    StartCoroutine(MovePlayer(Vector2.up));
            }
            else
            if (Input.GetKey(Down) && !isMoving)
            {
                direction = 0;

                if (playerAnim.GetInteger("Direction") != 0)
                    playerAnim.SetInteger("Direction", 0);
                if (this.gameObject.GetComponent<DetectWalls>().returnDownWall() != true)
                    StartCoroutine(MovePlayer(Vector2.down));
            }
            else
            if (Input.GetKey(Right) && !isMoving)
            {
                direction = 2;

                if (playerAnim.GetInteger("Direction") != 2)
                    playerAnim.SetInteger("Direction", 2);
                if (this.gameObject.GetComponent<DetectWalls>().returnRightWall() != true)
                    StartCoroutine(MovePlayer(Vector2.right));
            }
            else
            if (Input.GetKey(Left) && !isMoving)
            {
                direction = 3;

                if (playerAnim.GetInteger("Direction") != 3)
                    playerAnim.SetInteger("Direction", 3);
                if (this.gameObject.GetComponent<DetectWalls>().returnLeftWall() != true)
                    StartCoroutine(MovePlayer(Vector2.left));

            }
        }
    }


    private IEnumerator MovePlayer(Vector2 direction)
    {
        isMoving = true;
        playerAnim.SetBool("IsMoving", true);
        float elapsedTime = 0;
        origPos = new Vector2(Mathf.Round(transform.position.x), Mathf.Round(transform.position.y));
        targetPos = origPos + direction;
        while (elapsedTime < timeToMove)
        {
            transform.position = Vector2.Lerp(origPos, targetPos, (elapsedTime / timeToMove));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = targetPos;
        isMoving = false;
    }
}
