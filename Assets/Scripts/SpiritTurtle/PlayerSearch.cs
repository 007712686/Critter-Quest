using UnityEngine;

public class PlayerSearch : MonoBehaviour
{
    public GameObject searchPromptUI; // The UI prompt for "Press F to search."
    public GameObject foundTextUI; // The UI for displaying "Found."
    public GameObject notFoundTextUI; // The UI for displaying "Not Found."
    public float resultDisplayTime = 2.0f; // Time in seconds to display the result UI

    private bool canSearch = false; // Tracks if the player is in range to search
    private GameObject currentTarget; // The current prefab the player is colliding with
    public CurrentScore score;
    public StartScreen StartScreen;

    private void Start()
    {
        // Ensure all UI elements are hidden initially
        searchPromptUI.SetActive(false);
        foundTextUI.SetActive(false);
        notFoundTextUI.SetActive(false);
    }

    public void Handler()
    {
        // Check if the player presses F and can search
        if (canSearch && Input.GetKeyDown(KeyCode.F))
        {
            HandleSearch();
        }
        if(Input.GetKeyDown(KeyCode.Escape) || StartScreen.isPaused == true)
        {
                StartScreen.isPaused = true;
        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the colliding object is a Spirit or EmptySpace prefab
        if (other.CompareTag("Spirit") || other.CompareTag("EmptySpace"))
        {
            canSearch = true;
            currentTarget = other.gameObject;
            searchPromptUI.SetActive(true); // Display the "Press F to search" prompt
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        // When leaving the collider, reset the search state
        if (other.gameObject == currentTarget)
        {
            canSearch = false;
            currentTarget = null;
            searchPromptUI.SetActive(false); // Hide the "Press F to search" prompt
            ResetResultUI(); // Reset the result UI when leaving
        }
    }

    private void HandleSearch()
    {
        // Ensure the result UI updates based on the prefab type
        if (currentTarget != null)
        {
            if (currentTarget.CompareTag("Spirit"))
            {
                foundTextUI.SetActive(true); // Show "Found" text
                notFoundTextUI.SetActive(false); // Ensure "Not Found" text is hidden
                score.addScore();

            }
            else if (currentTarget.CompareTag("EmptySpace"))
            {
                notFoundTextUI.SetActive(true); // Show "Not Found" text
                foundTextUI.SetActive(false); // Ensure "Found" text is hidden
            }

            searchPromptUI.SetActive(false); // Hide the search prompt

            // Deactivate the result UI after a delay
            Invoke(nameof(ResetResultUI), resultDisplayTime);

            // Deactivate the prefab after a short delay
            StartCoroutine(DeactivatePrefabAfterDelay(currentTarget, 0.5f));

            // Allow the player to search other prefabs by resetting the state
            canSearch = false;
            currentTarget = null;
        }
    }

    private void ResetResultUI()
    {
        foundTextUI.SetActive(false);
        notFoundTextUI.SetActive(false);
    }

    private System.Collections.IEnumerator DeactivatePrefabAfterDelay(GameObject target, float delay)
    {
        yield return new WaitForSeconds(delay);
        target.SetActive(false); // Deactivate the prefab after the delay
    }
}
