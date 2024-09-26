using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

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
        contextMenuInstance.transform.position = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y-50);

        // Get the buttons and add listeners
        Button placeButton = contextMenuInstance.transform.Find("PlaceButton").GetComponent<Button>();
        Button destroyButton = contextMenuInstance.transform.Find("DestroyButton").GetComponent<Button>();
        Button viewButton = contextMenuInstance.transform.Find("ViewButton").GetComponent<Button>();

        placeButton.onClick.AddListener(() => OnPlace());
        destroyButton.onClick.AddListener(() => OnDestroyItem());
        viewButton.onClick.AddListener(() => OnView());
    }

    private void OnPlace()
    {
        Debug.Log("Placing item..." + item.name);
        CloseContextMenu();
    }

    private void OnDestroyItem()
    {
        Debug.Log("Destroying item..." + item.name);
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
