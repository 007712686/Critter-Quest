using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectWalls : MonoBehaviour
{
    [SerializeField]
    bool UpWallDetected;
    [SerializeField]
    bool DownWallDetected;
    [SerializeField]
    bool LeftWallDetected;
    [SerializeField]
    bool RightWallDetected;
    [SerializeField]
    LayerMask layerMask;
    Ray upRay;
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, Vector2.up * .5f);
        Gizmos.DrawRay(transform.position, Vector2.down * .5f);
        Gizmos.DrawRay(transform.position, Vector2.left * .5f);
        Gizmos.DrawRay(transform.position, Vector2.right * .5f);
    }
    private void Update()
    {
        RaycastHit2D hitUp = Physics2D.Raycast(transform.position, Vector2.up, .5f, layerMask);
        if (hitUp.collider != null)
        {
            UpWallDetected = true;
        }
        else UpWallDetected = false;
        RaycastHit2D hitDown = Physics2D.Raycast(transform.position, Vector2.down, .5f, layerMask);

        if (hitDown.collider != null)
        {
            DownWallDetected = true;
        }
        else DownWallDetected = false;
        RaycastHit2D hitLeft = Physics2D.Raycast(transform.position, Vector2.left, .5f, layerMask);

        if (hitLeft.collider != null)
        {
            LeftWallDetected = true;
        }
        else LeftWallDetected = false;

        RaycastHit2D hitRight = Physics2D.Raycast(transform.position, Vector2.right, .5f, layerMask);
        if (hitRight.collider != null)
        {
            RightWallDetected = true;
        }
        else RightWallDetected = false;

    }
    public bool returnUpWall()
    {
        return UpWallDetected;
    }
    public bool returnDownWall()
    {
        return DownWallDetected;
    }
    public bool returnLeftWall()
    {
        return LeftWallDetected;
    }
    public bool returnRightWall()
    {
        return RightWallDetected;
    }
}