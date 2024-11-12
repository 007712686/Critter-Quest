using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paw : MonoBehaviour
{
    public SpriteRenderer cursorSpriteRenderer;  // Drag the sprite renderer here
    public Vector2 positionSpriteOffset;
    [Range(0, 1)] public float transparency = 1f;  // Transparency slider (0 to 1)
    public float transparentValue = 0.46f;
    //bool mouseDown = false;
    

    private void Start()
    {
        SetTransparency(transparentValue);
        Cursor.visible = false;  // Hide system cursor
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //mouseDown = true;
            transparentValue = 1f;
            SetTransparency(transparentValue);
        }
        if (Input.GetMouseButtonUp(0))
        {
            transparentValue = 0.46f;
        }

        SetTransparency(Mathf.Lerp(cursorSpriteRenderer.color.a, transparentValue, Time.deltaTime / 0.25f));

        // Convert mouse position from screen space to world space
        Vector3 cursorPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        cursorPosition.z = 0;  // Keep the cursor on the same z-plane (2D)

        // Move the sprite to the new mouse position
        transform.position = cursorPosition + (Vector3)positionSpriteOffset;
    }

    public void SetTransparency(float alphaValue)
    {
        Color color = cursorSpriteRenderer.color;  // Get the current color of the sprite
        color.a = alphaValue;  // Set the alpha (transparency)
        cursorSpriteRenderer.color = color;  // Apply the modified color
    }
}
