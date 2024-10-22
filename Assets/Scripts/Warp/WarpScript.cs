using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WarpScript : MonoBehaviour
{
    public string sceneName;
    public FadeScript fadeBackground;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void warp()
    {
        //fadeBackground.StartFadeToBlack();
        SceneManager.LoadSceneAsync(sceneName);
    }

}
