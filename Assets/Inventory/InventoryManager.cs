using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public int maxStackedItems = 16;
    public InventorySlot[] inventorySlots;
    public GameObject inventoryItemPrefab;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ClearInventory()
    {
        foreach (var slot in inventorySlots)
        {
            // Get all InventoryItem components in this slot and destroy their GameObjects
            foreach (Transform child in slot.transform)
            {
                DestroyImmediate(child.gameObject);
            }
        }

        Debug.Log("Inventory cleared.");
    }

    public bool AddItem(Item item)
    {
        //Find slot with stackable/same item
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            InventorySlot slot = inventorySlots[i];
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
            if (itemInSlot != null && itemInSlot.item == item && itemInSlot.count < maxStackedItems && itemInSlot.item.stackable == true)
            {
                itemInSlot.count++;
                itemInSlot.RefreshCount();
                if (GameManager.Instance.questManager != null)
                {
                    if (GameManager.Instance.questManager.GetComponent<QuestTracker>().currentQuests.Count != 0)
                    {
                        GameManager.Instance.questManager.GetComponent<QuestTracker>().checkUpdatedInvenAdd(item);
                    }
                }
                return true;
            }
        }


        //Find any empty slot
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            InventorySlot slot = inventorySlots[i];
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
            if(itemInSlot == null)
            {
                SpawnNewItem(item, slot);
                if (GameManager.Instance.questManager != null)
                {
                    if (GameManager.Instance.questManager.GetComponent<QuestTracker>().currentQuests.Count != 0)
                    {
                        GameManager.Instance.questManager.GetComponent<QuestTracker>().checkUpdatedInvenAdd(item);
                    }
                }
                return true;
            }
        }
        return false;
    }

    void SpawnNewItem(Item item, InventorySlot slot)
    {
        GameObject newItemGo = Instantiate(inventoryItemPrefab, slot.transform);
        InventoryItem inventoryItem = newItemGo.GetComponent<InventoryItem>();
        inventoryItem.InitializeItem(item);
    }


    public void RemoveItem(InventoryItem itemToRemove)
    {
                if (itemToRemove.count > 1)
                {
                print("Goin");
                    itemToRemove.count--;
                    itemToRemove.RefreshCount();
                }
                else
                {
                    Destroy(itemToRemove.gameObject);
                }
        Debug.Log("Item removed: " + itemToRemove.name);
    }

}
