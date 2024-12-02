using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class PetCreature : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per fram
    void OnMouseDown()
    {
        Debug.Log("Pet Button Clicked!");
        pet();
    }
    public void pet()
    {
        this.gameObject.transform.parent.GetComponent<PetInfo>().thisPet.happiness += 20;
        if(this.gameObject.transform.parent.GetComponent<PetInfo>().thisPet.happiness > this.gameObject.transform.parent.GetComponent<PetInfo>().thisPet.maxHapp)
        {
            print("FOLLOW");
            this.gameObject.transform.parent.GetComponent<PetInfo>().thisPet.happiness = this.gameObject.transform.parent.GetComponent<PetInfo>().thisPet.maxHapp;
            this.gameObject.transform.parent.GetComponent<PetInfo>().following = true;
            GameManager.Instance.getPlayer().gameObject.GetComponent<PlayerMovement>().trailObject = this.gameObject.transform.parent.gameObject.GetComponent<ObjectTrail>();
        }
        this.gameObject.transform.parent.GetComponent<PetInfo>().viewScreen.GetComponent<PetScreenAssigner>().happinessText.GetComponent<Text>().text = this.gameObject.transform.parent.GetComponent<PetInfo>().thisPet.happiness + "/" + this.gameObject.transform.parent.GetComponent<PetInfo>().thisPet.maxHapp;

    }
}
