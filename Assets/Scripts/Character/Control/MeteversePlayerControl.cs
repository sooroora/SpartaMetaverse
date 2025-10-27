using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteversePlayerControl : PlayerControl
{
    [SerializeField] private GameObject interactionBubble;
    
    public override void ManualUpdate()
    {
        base.ManualUpdate();
        
        InteractionInput();
    }

    IInteractable InteractionCheck()
    {
        Vector3 dirFloat = character.moveControlData.lastDirection;
        RaycastHit2D[] hits = Physics2D.LinecastAll(character.transform.position,
            character.transform.position + (dirFloat * character.moveControlData.collisionCheckDis));
        
        foreach (RaycastHit2D hit in hits)
        {
            IInteractable interactable = hit.collider.GetComponent<IInteractable>();
            if (interactable!= null)
            {
                interactionBubble.SetActive(true);
                return interactable;
            }
        }
        
        interactionBubble.SetActive(false);
        return null;
    }

    public void InteractionInput()
    {
        if (InteractionCheck() is IInteractable interactable)
        {
            if(Input.GetKeyDown(KeyCode.Space))
                interactable.Interaction();
        }
       
    }
}
