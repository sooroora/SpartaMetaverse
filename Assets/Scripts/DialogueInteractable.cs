using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueInteractable : MonoBehaviour
{
    [SerializeField] DialogueData dialogueData;

    public void PlayDialogue()
    {
        if(dialogueData != null)
            DialogueManager.Instance.StartDialogue(dialogueData);
    }
}
