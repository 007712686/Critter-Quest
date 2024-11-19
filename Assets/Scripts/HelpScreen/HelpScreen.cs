using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelpScreen : MonoBehaviour
{
    bool flag = false;
    public GameObject hideMe;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {           
            flag = !flag;
            hideMe.gameObject.SetActive(flag);
        }
    }
}
