using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Interact : MonoBehaviour
{
    [SerializeField]
    KeyCode interact;
    [SerializeField]
    GameObject interactionTarget;
    RaycastHit2D target;
    Vector2 currentDirection;
    [SerializeField]
    LayerMask layerMask;
    public bool petInteract;
    private void OnDrawGizmos()
    {
        Gizmos.DrawRay(transform.position, currentDirection * .5f);
    }
    // Update is called once per frame
    void Update()
    {
        //If the escape key is pressed and we are interacting with a pet, reset the world to a walkable state
        if (Input.GetKeyDown("escape") && petInteract == true)
        {
            GameManager.Instance.getPlayer().GetComponent<PlayerMovement>().setPauseWorld(false);
            petInteract = false;
            interactionTarget.gameObject.GetComponent<PetInfo>().viewButton.SetActive(false);
            interactionTarget.gameObject.GetComponent<PetInfo>().playButton.SetActive(false);
            interactionTarget.gameObject.GetComponent<PetInfo>().petButton.SetActive(false);
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
            target = Physics2D.Raycast(transform.position, currentDirection, .5f,layerMask);
            if(target.transform != null)
            {
                //Handles interacting with objects with text boxes
                interactionTarget = target.transform.gameObject;
                if (interactionTarget.GetComponent<InteractText>() != null && interactionTarget.GetComponent<TextHolder>() != null && interactionTarget.GetComponent<InteractText>().getEndOfBox() != true && interactionTarget.GetComponent<InteractText>().getCompleteResetter() != true)
                {
                    if(interactionTarget.GetComponent<InteractText>().getIsTyping() == false)
                    interactionTarget.GetComponent<TextHolder>().startConvo();
                }





                //Handles interacting with pets
                else if(interactionTarget.GetComponent<PetInfo>() != null)
                {
                    print("INTERACTING WITH: " + interactionTarget.gameObject.GetComponent<PetInfo>().species);
                    GameManager.Instance.getPlayer().GetComponent<PlayerMovement>().setPauseWorld(true);
                    print("Pausing to talk with them!");
                    petInteract = true;
                    interactionTarget.gameObject.GetComponent<PetInfo>().viewButton.SetActive(true);
                    interactionTarget.gameObject.GetComponent<PetInfo>().playButton.SetActive(true);
                    interactionTarget.gameObject.GetComponent<PetInfo>().petButton.SetActive(true);

                    //When you press the Pet Button, happiness goes up
                }
            }
        }
    }
}
