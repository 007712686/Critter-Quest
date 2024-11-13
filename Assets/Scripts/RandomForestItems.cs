using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomForestItems : MonoBehaviour
{
    public List<Item> items = new List<Item>();
    bool alreadyTaken = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Tab))
        {
            if(alreadyTaken)
            {
                GameManager.Instance.getPlayer().GetComponent<PlayerMovement>().setPauseWorld(false);
                GameManager.Instance.getTextBox().GetComponent<TextBox>().closeBox();
                Destroy(gameObject);
                
            }
        }
    }

    public void gainItem()
    {
        if(!alreadyTaken)
        {
            print("GOIN");
            int x = Random.Range(0, items.Count);
            string[] itemText = new string[1];
            itemText[0] = "Gained a " + items[x].name;
            this.gameObject.GetComponent<TextHolder>().setDialogue(itemText);
            GameManager.Instance.inventory.GetComponentInChildren<InventoryManager>().AddItem(items[x]);
            alreadyTaken = true;
        }
    }
}
