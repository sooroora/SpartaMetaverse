using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MeteversePlayerControl : PlayerControl
{
    [SerializeField] private GameObject interactionButtonImg;

    List<ITriggerInteractable> stayTriggerInteractables = new List<ITriggerInteractable>();

    public override void ManualUpdate()
    {
        base.ManualUpdate();

        Vector3 dirFloat = character.moveControlData.lastDirection;
        RaycastHit2D[] hits = Physics2D.LinecastAll(character.transform.position,
            character.transform.position + (dirFloat * character.moveControlData.collisionCheckDis));

        InteractionInput(hits);
        TriggerCheck(hits); //안에 들어있는 trigger 보다는 바라보는 trigger 인데 뭐라하지
    }

    IInteractable InteractionCheck(RaycastHit2D[] hits)
    {
        foreach (RaycastHit2D hit in hits)
        {
            IInteractable interactable = hit.collider.GetComponent<IInteractable>();
            if (interactable != null)
            {
                interactionButtonImg.SetActive(true);
                return interactable;
            }
        }

        interactionButtonImg.SetActive(false);
        return null;
    }

    void TriggerCheck(RaycastHit2D[] hits)
    {
        List<ITriggerInteractable> nowTriggerInteractables = new List<ITriggerInteractable>();
        
        foreach (RaycastHit2D hit in hits)
        {
            ITriggerInteractable interactable = hit.collider.GetComponent<ITriggerInteractable>();
            if (interactable != null)
            {
                nowTriggerInteractables.Add(interactable);
                if (stayTriggerInteractables.Contains(interactable) == false)
                {
                    stayTriggerInteractables.Add(interactable);
                    interactable.TriggerEnterInteraction();
                }
                // if (stayTriggerInteractables.Contains(interactable))
                //     continue;
                // else
                // {
                //     stayTriggerInteractables.Add(interactable);
                //     interactable.TriggerEnterInteraction();
                // }
            }
        }

        // 이제 안 보고 있는 애들은 exit
        stayTriggerInteractables.RemoveAll(stay =>
        {
            if (nowTriggerInteractables.Contains(stay)== false)
            {
                stay.TriggerExitInteraction();
                return true;
            }
            else
            {
                return false;
            }
        });

        // foreach (ITriggerInteractable nowInteractable in stayTriggerInteractables)
        // {
        //     bool isExit = true;
        //
        //     foreach (RaycastHit2D hit in hits)
        //     {
        //         ITriggerInteractable hitInteractable = hit.collider.GetComponent<ITriggerInteractable>();
        //         if (hitInteractable == nowInteractable)
        //         {
        //             isExit = false;
        //             break;
        //         }
        //     }
        //
        //     if (isExit)
        //     {
        //         nowInteractable.TriggerExitInteraction();
        //         stayTriggerInteractables.Remove(nowInteractable);
        //     }
        // }
    }

    public void InteractionInput(RaycastHit2D[] hits)
    {
        if (InteractionCheck(hits) is IInteractable interactable)
        {
            if (Input.GetKeyDown(KeyCode.Space))
                interactable.Interaction();
        }
    }
}