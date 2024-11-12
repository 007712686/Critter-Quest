using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserScript : MonoBehaviour
{
    public float xBoundsLeft;
    public float xBoundsRight;
    public float yBoundsTop;
    public float yBoundsBottom;
    public float setPadding = 0.6f;
    private float randomXPos;
    private float randomYPos;
    // Start is called before the first frame update
    void Start()
    {
        SetBounds();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void SetBounds()
    {
        Camera gameCamera = Camera.main; //assign main camera to variable
        xBoundsLeft = gameCamera.ViewportToWorldPoint(new Vector3(0f, 0f, 0f)).x + setPadding;
        xBoundsRight = gameCamera.ViewportToWorldPoint(new Vector3(1f, 0f, 0f)).x - setPadding;
        yBoundsTop = gameCamera.ViewportToWorldPoint(new Vector3(0f, 1f, 0f)).y - setPadding;
        yBoundsBottom = gameCamera.ViewportToWorldPoint(new Vector3(0f, 0f, 0f)).y + setPadding;
    }
    public void Move()
    {
        randomXPos = Random.Range(xBoundsLeft, xBoundsRight);
        randomYPos = Random.Range(yBoundsBottom, yBoundsTop);
        transform.position = new Vector3(randomXPos, randomYPos, transform.position.z);
    }
}
