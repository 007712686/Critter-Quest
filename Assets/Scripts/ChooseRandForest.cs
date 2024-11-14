using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChooseRandForest : MonoBehaviour
{
    public GameObject[] forestLayouts;
    // Start is called before the first frame update
    void Start()
    {
        RandomForest();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RandomForest()
    {
        int x = Random.Range(0, forestLayouts.Length);
        forestLayouts[x].SetActive(true);
        switch (x)
        {
            case 0:
                print("NORMAL VERSION");
                break;
            case 1:
                print("ALTERED VERSION");
                break;
        }
    }
}
