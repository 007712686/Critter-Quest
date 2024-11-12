using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayScript : MonoBehaviour
{
    public StartScreen StartScreen;
    private Camera cam;
    public bool isHit = false;
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        print(cam.name);
    }

    // Update is called once per frame
    public void Handle()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || StartScreen.isPaused == true)
        {
            StartScreen.isPaused = true;
        }

        else
        {
            detectingRay();
        }
    }

    public void detectingRay()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 0f;
        mousePos = cam.ScreenToWorldPoint(mousePos);
        Debug.DrawRay(transform.position, mousePos - transform.position, Color.blue);

        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePos2D = cam.ScreenToWorldPoint(Input.mousePosition);

            RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);

            if (hit.collider != null)
            {
                Debug.Log(hit.transform.name);
                isHit = true;
            }

        }
    }
}
