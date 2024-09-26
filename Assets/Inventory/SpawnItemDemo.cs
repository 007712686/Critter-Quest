using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnItemDemo : MonoBehaviour
{
    public InventoryManager inventoryManager;
    public Item[] itemsToPickup;

    public void PickupItem(int id)
    {
       bool result = inventoryManager.AddItem(itemsToPickup[id]);
        if(result == true)
        {
            print("ADDED " + itemsToPickup[id].name);
        }
        else
        {
            print("DID NOT ADD " + itemsToPickup[id].name);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            PickupItem(0);
            print("1");
        }
        if (Input.GetKeyDown(KeyCode.F2))
        {
            PickupItem(1); 
            print("2");
        }
        if (Input.GetKeyDown(KeyCode.F3))
        {
            PickupItem(2); 
            print("3");
        }
    }
}
