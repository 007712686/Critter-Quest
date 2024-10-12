using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ViewCreature : MonoBehaviour
{
    public GameObject petScreen;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnMouseDown()
    {
        Debug.Log("View Button Clicked!");
        view();
    }
    public void view()
    {
        petScreen.GetComponent<PetScreenAssigner>().happinessText.GetComponent<Text>().text = this.gameObject.transform.parent.GetComponent<PetInfo>().thisPet.happiness + "/" + this.gameObject.transform.parent.GetComponent<PetInfo>().thisPet.maxHapp;
        petScreen.GetComponent<PetScreenAssigner>().fullnessText.GetComponent<Text>().text = this.gameObject.transform.parent.GetComponent<PetInfo>().thisPet.fullness + "/" + this.gameObject.transform.parent.GetComponent<PetInfo>().thisPet.maxFull;
        petScreen.GetComponent<PetScreenAssigner>().levelText.GetComponent<Text>().text = this.gameObject.transform.parent.GetComponent<PetInfo>().thisPet.level.ToString();
        petScreen.transform.localPosition = Vector3.zero; 
    }
}
