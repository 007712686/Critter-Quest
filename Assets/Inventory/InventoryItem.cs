using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class InventoryItem : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler, IPointerClickHandler
{
    [Header("UI")]
    public Image image;
    public Text countText;
    [HideInInspector] public Item item;
    [HideInInspector] public int count = 1;
    [HideInInspector] public Transform parentAfterDrag;

    public GameObject contextMenuPrefab;  // Reference to your context menu prefab
    private GameObject contextMenuInstance; // Instance of the context menu
    private static GameObject currentContextMenu; // Static reference to the currently open context menu
    public InventorySlot currentSlot;

    public void RefreshCount()
    {
        countText.text = count.ToString();
        bool textActive = count > 1;
        countText.gameObject.SetActive(textActive);
    }

    public void InitializeItem(Item newItem)
    {
        item = newItem;
        image.sprite = newItem.image;
        RefreshCount();
    }

    private void Start()
    {
        InitializeItem(item);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        print("BEGIN DRAG");
        image.raycastTarget = false;
        parentAfterDrag = transform.parent;

        // Move the item to the root of the canvas to ensure it's on top
        transform.SetParent(transform.root);

        // Make sure it's the last child of the canvas so it renders on top
        transform.SetAsLastSibling();
    }



    public void OnDrag(PointerEventData eventData)
    {
        print("DRAGGING");
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        print("END DRAG");
        image.raycastTarget = true;

        // Return the item to its original parent
        transform.SetParent(parentAfterDrag);
    }

    // Implement IPointerClickHandler to detect right-clicks
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            currentSlot = transform.gameObject.GetComponent<InventorySlot>();
            ShowContextMenu();
        }
    }

    private void ShowContextMenu()
    {
        // Close the current context menu if one is already open
        if (currentContextMenu != null)
        {
            Destroy(currentContextMenu);
        }

        // Instantiate the context menu prefab
        contextMenuInstance = Instantiate(contextMenuPrefab, transform.root);
        currentContextMenu = contextMenuInstance; // Set the current context menu reference

        // Set the position of the context menu near the clicked item
        contextMenuInstance.transform.position = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y - 50);

        // Get the buttons and add listeners
        Button placeButton = contextMenuInstance.transform.Find("PlaceButton").GetComponent<Button>();
        Button destroyButton = contextMenuInstance.transform.Find("DestroyButton").GetComponent<Button>();
        Button viewButton = contextMenuInstance.transform.Find("ViewButton").GetComponent<Button>();

        if(item.isFood != true)
        {
            placeButton.onClick.AddListener(() => OnPlace());
        }
        else
        {
            placeButton.GetComponentInChildren<Text>().text = "Feed";
            placeButton.onClick.AddListener(() => OnFeed());
        }
        //change place button with feed button

        destroyButton.onClick.AddListener(() => OnDestroyItem());
        viewButton.onClick.AddListener(() => OnView());
    }
    private void OnFeed()
    {
        if(GameManager.Instance.getPlayer().GetComponent<Interact>().interactionTarget != null)
        {
            if(GameManager.Instance.getPlayer().GetComponent<Interact>().interactionTarget.GetComponent<PetInfo>() != null)
            {
                print("Feeding " + GameManager.Instance.getPlayer().GetComponent<Interact>().interactionTarget.GetComponent<PetInfo>().thisPet.petName);
                GameManager.Instance.getPlayer().GetComponent<Interact>().interactionTarget.GetComponent<PetInfo>().thisPet.fullness += 10;
                if(GameManager.Instance.getPlayer().GetComponent<Interact>().interactionTarget.GetComponent<PetInfo>().thisPet.fullness > GameManager.Instance.getPlayer().GetComponent<Interact>().interactionTarget.GetComponent<PetInfo>().thisPet.maxFull)
                {
                    GameManager.Instance.getPlayer().GetComponent<Interact>().interactionTarget.GetComponent<PetInfo>().thisPet.fullness = GameManager.Instance.getPlayer().GetComponent<Interact>().interactionTarget.GetComponent<PetInfo>().thisPet.maxFull;
                }
            }
        }
        else
        {
            print("Nowhere to feed");
        }
        CloseContextMenu();
    }
    private void OnPlace()
    {
        Vector3 spawnPosition = GameManager.Instance.getPlayer().GetComponent<PlayerMovement>().transform.position;
        switch (GameManager.Instance.getPlayer().GetComponent<PlayerMovement>().direction)
        {
            case 0: // Down
                spawnPosition -= transform.up;
                break;
            case 1: // Up
                spawnPosition += transform.up;
                break;
            case 2: // Right
                spawnPosition += transform.right;
                break;
            case 3: // Left
                spawnPosition -= transform.right;
                break;
            default:
                Debug.LogWarning("Invalid direction specified.");
                return;
        }
        Debug.Log("Placing item..." + item.name);
        item.pref.GetComponent<SpriteRenderer>().sprite = item.image;
        item.pref.GetComponent<ItemAssign>().itemItIs = this.item;
        Instantiate(item.pref, spawnPosition, Quaternion.identity);
        GameManager.Instance.inventory.GetComponentInChildren<InventoryManager>().RemoveItem(this);

        CloseContextMenu();
    }

    private void OnDestroyItem()
    {
        Debug.Log("Destroying item..." + item.name);
        GameManager.Instance.inventory.GetComponentInChildren<InventoryManager>().RemoveItem(this);
        CloseContextMenu();
    }

    private void OnView()
    {
        Debug.Log("Viewing item..." + item.name);
        CloseContextMenu();
    }

    private void CloseContextMenu()
    {
        if (contextMenuInstance != null)
        {
            Destroy(contextMenuInstance);
            currentContextMenu = null; // Clear the static reference
        }
    }

    private void OnDestroy()
    {
        // Ensure that the context menu is cleared if this object is destroyed
        if (contextMenuInstance == currentContextMenu)
        {
            currentContextMenu = null;
        }
    }
}
