using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestAssigner : MonoBehaviour
{
    public GameObject questHolder;
    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.questManager = this.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
