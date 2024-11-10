using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopInventory : MonoBehaviour
{
    // Start is called before the first frame update
    public Item[] shopItems;
    public InventorySlot slotPrefab;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void showItems()
    {
        for (int i = 0; i < GameManager.Instance.inventory.GetComponentInChildren<InventoryManager>().inventorySlots.Length; i++)
        {
            this.gameObject.GetComponent<ShopManager>().inventorySlots[i] = Instantiate(slotPrefab, GameManager.Instance.inventory.GetComponentInChildren<InventoryManager>().inventorySlots[i].gameObject.transform.parent);
            GameManager.Instance.inventory.GetComponentInChildren<InventoryManager>().inventorySlots[i].gameObject.SetActive(false);
        }
        for (int i = 0; i < shopItems.Length; i++)
        {
            this.gameObject.GetComponent<ShopManager>().AddItem(shopItems[i]);
        }

    }
}
