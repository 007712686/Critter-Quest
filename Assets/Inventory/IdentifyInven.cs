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
        this.gameObject.transform.localPosition = new Vector3(1000, -1000, 0);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
