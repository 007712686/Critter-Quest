using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;

public class WarpTiles : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the object the player collided with is the warp tile
        if (collision.CompareTag("Warp"))
        {
            Debug.Log("Player has entered the warp tile!");
            collision.gameObject.GetComponent<WarpScript>().warp();
        }
    }
}
