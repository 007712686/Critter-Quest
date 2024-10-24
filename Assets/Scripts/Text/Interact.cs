using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Interact : MonoBehaviour
{
    [SerializeField]
    KeyCode interact;
    [SerializeField]
    public GameObject interactionTarget;
    RaycastHit2D target;
    Vector2 currentDirection;
    [SerializeField]
    LayerMask layerMask;
    public bool petInteract;
    private void OnDrawGizmos()
    {
        Gizmos.DrawRay(transform.position, currentDirection * 1);
    }
    // Update is called once per frame
    void Update()
    {
        //If the escape key is pressed and we are interacting with a pet, reset the world to a walkable state
        if (Input.GetKeyDown("escape") && petInteract == true)
        {
            GameManager.Instance.getPlayer().GetComponent<PlayerMovement>().setPauseWorld(false);
            petInteract = false;
            interactionTarget.gameObject.GetComponent<PetInfo>().viewButton.transform.localPosition = new Vector2(-1000, 1000);
            interactionTarget.gameObject.GetComponent<PetInfo>().playButton.transform.localPosition = new Vector2(-1000, 1000);
            interactionTarget.gameObject.GetComponent<PetInfo>().petButton.transform.localPosition = new Vector2(-1000, 1000);
            if (interactionTarget.gameObject.GetComponentInChildren<ViewCreature>() != null)
            {
                print("Removing Screen");
                interactionTarget.gameObject.GetComponent<PetInfo>().viewScreen.transform.localPosition = new Vector2(-1000, 1000);
            }

        }
        //If we press interact, use the direction in the animator to set our interact direction
        if (Input.GetKeyDown(interact))
        {
            if (this.gameObject.GetComponentInChildren<Animator>().GetInteger("Direction") == 1)
                currentDirection = Vector2.up;
            if (this.gameObject.GetComponentInChildren<Animator>().GetInteger("Direction") == 2)
                currentDirection = Vector2.right;
            if (this.gameObject.GetComponentInChildren<Animator>().GetInteger("Direction") == 0)
                currentDirection = Vector2.down;
            if (this.gameObject.GetComponentInChildren<Animator>().GetInteger("Direction") == 3)
                currentDirection = Vector2.left;
            //set the target if one exists that the raycast hits
            target = Physics2D.Raycast(transform.position, currentDirection, 1,layerMask);
            if(target.transform != null)
            {
                interactionTarget = target.transform.gameObject;
                //Handles interacting with objects with text boxes
                if (interactionTarget.GetComponent<InteractText>() != null && interactionTarget.GetComponent<TextHolder>() != null && interactionTarget.GetComponent<InteractText>().getEndOfBox() != true && interactionTarget.GetComponent<InteractText>().getCompleteResetter() != true)
                {
                    if(interactionTarget.GetComponent<InteractText>().getIsTyping() == false)
                    interactionTarget.GetComponent<TextHolder>().startConvo();
                }
                //Handles interacting with objects with quests
                if (interactionTarget.GetComponent<QuestGiver>() != null)
                {
                    if (interactionTarget.GetComponent<QuestGiver>().questGiven.questTurnedIn != true)
                    {
                        GameManager.Instance.questManager.GetComponent<QuestTracker>().questInQuestion = interactionTarget.GetComponent<QuestGiver>().questGiven;
                        if(interactionTarget.GetComponent<InteractText>() == null && interactionTarget.GetComponent<TextHolder>() == null)
                            GameManager.Instance.questManager.GetComponent<QuestAssigner>().questHolder.GetComponent<QuestBoard>().openBoard();
                    }
                }





                //Handles interacting with pets
                else if (interactionTarget.GetComponent<PetInfo>() != null)
                {
                    print("INTERACTING WITH: " + interactionTarget.gameObject.GetComponent<PetInfo>().thisPet.petName);
                    GameManager.Instance.getPlayer().GetComponent<PlayerMovement>().setPauseWorld(true);
                    print("Pausing to talk with them!");
                    petInteract = true;
                    interactionTarget.gameObject.GetComponent<PetInfo>().viewButton.transform.localPosition = new Vector2(-1.5f, 1);
                    interactionTarget.gameObject.GetComponent<PetInfo>().playButton.transform.localPosition = new Vector2(0, 1);
                    interactionTarget.gameObject.GetComponent<PetInfo>().petButton.transform.localPosition = new Vector2(1.5f, 1);
                }


                //Handles interacting with items
                else if (interactionTarget.GetComponent<ItemAssign>() != null)
                {
                    print("INTERACTING WITH AN ITEM!");
                    GameManager.Instance.inventory.GetComponentInChildren<InventoryManager>().AddItem(interactionTarget.GetComponent<ItemAssign>().itemItIs);
                    Destroy(interactionTarget);
                }
                /*fixxxxxxxxx
                else if (interactionTarget.name == "bed")
                {
                    interactionTarget.GetComponent<TextHolder>().startConvo();
                }
                */
            }
        }
        
        if (DaySystem.instance != null)
        {
            if (DaySystem.instance.getDayNumber() == 0)
            {
                if (DaySystem.instance.GetComponent<InteractText>().getIsTyping() == false)
                {
                    if (DaySystem.instance.newDay == true)
                    {
                        GameManager.Instance.getPlayer().GetComponent<PlayerMovement>().setPauseWorld(true);
                        DaySystem.instance.day1Dialogue();
                    }
                    else if (Input.GetKeyDown(interact))
                    {
                        DaySystem.instance.day1Dialogue();

                    }
                    if (DaySystem.instance.GetComponent<TextHolder>().endOfIndex == true && DaySystem.instance.newDay == false)
                    {

                        DaySystem.instance.endDay();
                    }
                }
            }
        }
    }
}
