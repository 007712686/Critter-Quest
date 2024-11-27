using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WarpScript : MonoBehaviour
{
    public string sceneName;

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
        StartCoroutine(saveBeforeWarp());
    }

    IEnumerator saveBeforeWarp()
    {
        if(DaySystem.instance != null)
        {
            if (DaySystem.instance.save != null && DaySystem.instance != null)
            {
                if (SceneManager.GetActiveScene().name == "critter quest")
                {
                    GameManager.Instance.overPos = GameManager.Instance.getPlayer().transform.position;
                    //add exceptions here
                    if (sceneName == "store")
                    {
                        GameManager.Instance.overPos.y = GameManager.Instance.getPlayer().transform.position.y - 1;
                    }
                }
                DaySystem.instance.save.SaveGame();
            }
        }

        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadSceneAsync(sceneName);


    }

}
