using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetScreenAssigner : MonoBehaviour
{
    public GameObject happinessText;
    public GameObject fullnessText;
    public GameObject levelText;
    // Start is called before the first frame update
    void Start()
    {
        this.gameObject.transform.parent.transform.parent.GetComponent<PetInfo>().viewScreen = this.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
