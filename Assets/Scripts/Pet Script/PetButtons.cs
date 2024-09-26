using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetButtons : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (this.gameObject.name == "View")
            this.gameObject.transform.parent.gameObject.GetComponent<PetInfo>().viewButton = this.gameObject;
        if (this.gameObject.name == "Pet")
            this.gameObject.transform.parent.gameObject.GetComponent<PetInfo>().petButton = this.gameObject;
        if (this.gameObject.name == "Play")
            this.gameObject.transform.parent.gameObject.GetComponent<PetInfo>().playButton = this.gameObject;

        this.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
