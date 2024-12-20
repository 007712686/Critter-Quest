using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    [SerializeField]
    string playerName;
    public GameObject player;
    GameObject textBox;
    [SerializeField]
    GameObject cursor;
    [SerializeField]
    GameObject TextManager;
    [SerializeField]
    public GameObject inventory;
    public List<PetObject> petObjects = new List<PetObject>();
    public List<QuestSO> questsInGame = new List<QuestSO>();
    public GameObject questManager;
    public Vector2 overPos;
    public bool needReset = false;
    public bool shopping = false;
    public int coins = 0;
    public string saveFilePath;
    public string sceneCurrent;
    public string sceneTarget = "";
    protected GameManager()
    {

    }
    public void positionPlayerProper()
    {
        if(sceneTarget != "")
        {
            if (sceneCurrent == "inside house" && sceneTarget == "critter quest")
            {
                print("GO");
                player.transform.position = new Vector2(3, 12);
            }
            if (sceneCurrent == "critter quest" && sceneTarget == "inside house")
            {
                print("GO");
                player.transform.position = new Vector2(-1, -4);
            }
            if (SceneManager.GetActiveScene().name == sceneTarget)
            {
                sceneTarget = "";
                sceneCurrent = SceneManager.GetActiveScene().name;
            }

        }
    }
    private void Start()
    {
        sceneCurrent = SceneManager.GetActiveScene().name;
    }
    public void resetPlayerPos()
    {
        if (player != null)
        {
            player.transform.position = overPos;
            needReset = false;
        }

    }
    //Getters and Setters for each object that must accessable be in every scene
    public GameObject getTextManager()
    {
        return TextManager;
    }
    public void setTextManager(GameObject x)
    {
        TextManager = x;
    }
    public GameObject getCursor()
    {
        return cursor;
    }
    public void setCursor(GameObject x)
    {
        cursor = x;
    }
    public GameObject getPlayer()
    {
        return player;
    }
    public void setPlayer(GameObject x)
    {
        player = x;
    }
    public void setTextBox(GameObject x)
    {
        textBox = x;
    }
    public GameObject getTextBox()
    {
        return textBox;
    }
    //Openers for inventory 
    public void openInventory()
    {
        inventory.transform.localPosition = new Vector3(0, -250, 0);
    }
    public void closeInventory()
    {
        inventory.transform.localPosition = new Vector3(10000, -10000, 0);
    }

    void Update()
    {
        positionPlayerProper();
        if(needReset == true)
        {
            resetPlayerPos();
        }
        //Escape while the player isnt paused
        if (Input.GetKeyDown(KeyCode.Backspace) && GameManager.Instance.getPlayer().GetComponent<PlayerMovement>().getPauseWorld() == false)
        {
            GameManager.Instance.openInventory();
            GameManager.Instance.getPlayer().GetComponent<PlayerMovement>().setPauseWorld(true);
        }
        else if (Input.GetKeyDown(KeyCode.Backspace) && player.GetComponent<Interact>().interactionTarget != null && GameManager.Instance.getPlayer().GetComponent<PlayerMovement>().getPauseWorld() == true)
        {
            if (player.GetComponent<Interact>().interactionTarget.GetComponent<ShopInventory>() != null)
            {
                GameManager.Instance.closeInventory();
                for (int i = 0; i < player.GetComponent<Interact>().interactionTarget.GetComponent<ShopManager>().inventorySlots.Length; i++)
                {
                    Destroy(player.GetComponent<Interact>().interactionTarget.GetComponent<ShopManager>().inventorySlots[i].gameObject);
                    inventory.GetComponentInChildren<InventoryManager>().inventorySlots[i].gameObject.SetActive(true);

                }
                for (int i = 0; i < player.GetComponent<Interact>().interactionTarget.GetComponent<ShopManager>().inventorySlots.Length; i++)
                {
                    if (player.GetComponent<Interact>().interactionTarget.GetComponent<ShopManager>().inventorySlots[i].GetComponentInChildren<InventoryItem>() != null)
                        player.GetComponent<Interact>().interactionTarget.GetComponent<ShopManager>().inventorySlots[i].GetComponentInChildren<InventoryItem>().CloseContextMenu();
                }
                player.GetComponent<Interact>().interactionTarget = null;
                GameManager.Instance.getPlayer().GetComponent<PlayerMovement>().setPauseWorld(false);
                shopping = false;

            }
            else
            {
                GameManager.Instance.closeInventory();
                GameManager.Instance.getPlayer().GetComponent<PlayerMovement>().setPauseWorld(false);

            }
        }
        else if (Input.GetKeyDown(KeyCode.Backspace) && GameManager.Instance.getPlayer().GetComponent<PlayerMovement>().getPauseWorld() == true)
        {
            GameManager.Instance.closeInventory();
            for (int i = 0; i < inventory.GetComponentInChildren<InventoryManager>().inventorySlots.Length; i++)
            {
                if (inventory.GetComponentInChildren<InventoryManager>().inventorySlots[i].GetComponentInChildren<InventoryItem>() != null)
                    inventory.GetComponentInChildren<InventoryManager>().inventorySlots[i].GetComponentInChildren<InventoryItem>().CloseContextMenu();
            }
            GameManager.Instance.getPlayer().GetComponent<PlayerMovement>().setPauseWorld(false);

        }

    }
}
