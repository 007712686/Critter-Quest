using UnityEngine;
using UnityEngine.SceneManagement;

public class FSS : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        // Check for the TAB key press
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            SwitchScene(this.gameObject.GetComponent<RotateObject>().selection);
        }
    }

    // Switch statement to load scenes based on currentPosition
    private void SwitchScene(int position)
    {
        switch (position)
        {
            case 0:
                SceneManager.LoadScene("inside house"); // Replace "Scene0" with your actual scene name
                break;
            case 1:
                SceneManager.LoadScene("critter quest"); // Replace "Scene1" with your actual scene name
                break;
            case 2:
                SceneManager.LoadScene("Forest"); // Replace "Scene2" with your actual scene name
                break;
            default:
                Debug.LogWarning("Invalid position value: " + position);
                break;
        }
    }
}
