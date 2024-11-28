using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInteraction : MonoBehaviour
{
    public Text promptText; // UI Text element to prompt player actions
    public float interactionRange = 2f; // Maximum range for detecting spirits
    private int spiritsFound = 0; // Spirits found by player
    private int totalSpirits = 3; // Total spirits to find
    private int maxSearchAttempts = 5; // Maximum allowed searches
    private int currentSearchAttempts = 0; // Track searches made
    private Collider2D currentSpiritCollider = null; // Tracks the collider of the spirit in range
    //public CurrentScore score;

    public void Handler()
    {
        // If a spirit is nearby and player hasn't reached max attempts, prompt them
        if (currentSpiritCollider != null && currentSearchAttempts < maxSearchAttempts)
        {
            promptText.text = "Spirit nearby! Press 'y' to search or 'n' to skip.";
            
            // Handle player confirmation input
            if (Input.GetKeyDown(KeyCode.Y)) // Player chooses to search
            {
                AttemptSearch();
            }
            else if (Input.GetKeyDown(KeyCode.N)) // Player skips search
            {
                promptText.text = ""; // Clear prompt
                currentSpiritCollider = null; // Reset current collider
            }
        }
    }

    private void AttemptSearch()
    {
        currentSearchAttempts++;
        promptText.text = ""; // Clear prompt

        if (currentSpiritCollider != null && currentSpiritCollider.CompareTag("Spirit"))
        {
            Destroy(currentSpiritCollider.gameObject); // Remove spirit if found
            spiritsFound++; //KODA Scoring here!!!!

            currentSpiritCollider = null; // Reset current collider after finding spirit
            CheckWinCondition();
        }
        else
        {
            Debug.Log("Nothing here! Keep searching.");
        }

        // Check if maximum searches reached
        if (currentSearchAttempts >= maxSearchAttempts && spiritsFound < totalSpirits)
        {
            Debug.Log("No more search attempts left.");
            promptText.text = "Out of search attempts! Game over.";
        }
    }

    private void CheckWinCondition()
    {
        if (spiritsFound >= totalSpirits)
        {
            Debug.Log("You found all the spirits!");
            promptText.text = "You found all the spirits!";
        }
    }

    // Detect when player enters a spirit's search area
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Spirit"))
        {
            currentSpiritCollider = other; // Set current spirit collider when player is in range
        }
    }

    // Detect when player exits a spirit's search area
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other == currentSpiritCollider)
        {
            currentSpiritCollider = null; // Clear current spirit collider when player leaves range
            promptText.text = ""; // Clear prompt when leaving range
        }
    }
}