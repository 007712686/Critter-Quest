using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetCreature : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnMouseDown()
    {
        Debug.Log("Pet Button Clicked!");
        pet();
    }
    public void pet()
    {
        this.gameObject.transform.parent.GetComponent<PetInfo>().happiness += 20;
        if(this.gameObject.transform.parent.GetComponent<PetInfo>().happiness > this.gameObject.transform.parent.GetComponent<PetInfo>().maxHapp)
        {
            this.gameObject.transform.parent.GetComponent<PetInfo>().happiness = this.gameObject.transform.parent.GetComponent<PetInfo>().maxHapp;
        }
    }
}
