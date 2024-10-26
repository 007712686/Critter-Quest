using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FadeScript : MonoBehaviour
{

    public Image image; // Reference to the SpriteRenderer component
    public float fadeDuration = 2f; // Duration of the fade

    private Color originalColor; // Store the original color of the sprite
    private float fadeTimer;

    public bool fadeOutComplete = false;

    private void Start()
    {
        if (image == null)
            image = GetComponent<Image>(); // Get SpriteRenderer if not assigned

        originalColor = image.color; // Store the initial color

    }

    public void StartFadeOutBlack()
    {
        this.gameObject.SetActive(true);
        fadeTimer = 0f; // Reset timer
        image.color = Color.black;
        StartCoroutine(otherSceneFades());
    }

    public void StartFadeInBlack()
    {
        fadeTimer = 0f; // Reset timer
        image.color = image.color = new Color(0f, 0f, 0f, 0f);
        StartCoroutine(FadeInBlack());
    }

    public void StartSpecialLogoFade()
    {
        fadeTimer = 0f;
        image.color = image.color = Color.black;
        StartCoroutine(SpecialLogoFade());
    }

    private IEnumerator FadeOutBlack()
    {
        fadeOutComplete = false;
        yield return new WaitForSeconds(0.1f); // Optional: small delay
        while (fadeTimer < fadeDuration)
        {
            fadeTimer += Time.deltaTime;
            float t = fadeTimer / fadeDuration;

            // Interpolate the alpha value from 1 (fully visible) to 0 (fully transparent)
            Color newColor = originalColor;
            newColor.a = Mathf.Lerp(1f, 0f, t);
            image.color = newColor;

            yield return null; // Wait for the next frame
        }

        // Ensure the final color is fully transparent
        Color finalColor = originalColor;
        finalColor.a = 0f;
        image.color = finalColor;
        fadeOutComplete = true;
    }

    private IEnumerator FadeInBlack()
    {
        //image.color = image.color = new Color(0f, 0f, 0f, 0f);
        yield return new WaitForSeconds(0.1f); // Optional: small delay
        
        while (fadeTimer < fadeDuration)
        {
            fadeTimer += Time.deltaTime;
            float t = fadeTimer / fadeDuration;

            // Interpolate the alpha value from 0 (fully transparent) to 1 (fully visible)
            Color newColor = originalColor;
            newColor.a = Mathf.Lerp(0f, 1f, t);
            image.color = newColor;

            yield return null; // Wait for the next frame
        }

        // Ensure the final color is fully visible
        Color finalColor = originalColor;
        finalColor.a = 1f;
        image.color = finalColor;
    }

    private IEnumerator SpecialLogoFade()
    {
        fadeTimer = 0f;
        yield return StartCoroutine(FadeOutBlack());
        fadeTimer = 0f;
        yield return StartCoroutine(FadeInBlack());
        SceneManager.LoadScene("MainMenu");
        
    }

    private IEnumerator otherSceneFades()
    {
        fadeTimer = 0f;
        yield return StartCoroutine(FadeOutBlack());
        this.gameObject.SetActive(false);

    }
}

