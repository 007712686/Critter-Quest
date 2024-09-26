using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdentifyInven : MonoBehaviour
{
    // Start is called before the first frame update
    //Sets the inventory object in game manager on creation
    void Start()
    {
        GameManager.Instance.inventory = this.gameObject;
        this.gameObject.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
