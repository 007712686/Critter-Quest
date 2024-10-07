using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectWalls : MonoBehaviour
{
    public float dist;
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
        Gizmos.DrawRay(transform.position, Vector2.up * dist);
        Gizmos.DrawRay(transform.position, Vector2.down * dist);
        Gizmos.DrawRay(transform.position, Vector2.left * dist);
        Gizmos.DrawRay(transform.position, Vector2.right * dist);
    }
    private void Update()
    {
        RaycastHit2D hitUp = Physics2D.Raycast(transform.position, Vector2.up, dist, layerMask);
        if (hitUp.collider != null)
        {
            //print("DETECTED: " + hitUp.collider.gameObject.name);
            UpWallDetected = true;
        }
        else UpWallDetected = false;
        RaycastHit2D hitDown = Physics2D.Raycast(transform.position, Vector2.down, dist, layerMask);

        if (hitDown.collider != null)
        {
            //print("DETECTED: " + hitDown.collider.gameObject.name);
            DownWallDetected = true;
        }
        else DownWallDetected = false;
        RaycastHit2D hitLeft = Physics2D.Raycast(transform.position, Vector2.left, dist, layerMask);

        if (hitLeft.collider != null)
        {
            //print("DETECTED: " + hitLeft.collider.gameObject.name);
            LeftWallDetected = true;
        }
        else LeftWallDetected = false;

        RaycastHit2D hitRight = Physics2D.Raycast(transform.position, Vector2.right, dist, layerMask);
        if (hitRight.collider != null)
        {
            //print("DETECTED: " + hitRight.collider.gameObject.name);
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