using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetInfo : MonoBehaviour
{
    public GameObject viewButton, petButton, playButton, viewScreen;
    public PetObject thisPet;
    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.petObjects.Add(thisPet);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
