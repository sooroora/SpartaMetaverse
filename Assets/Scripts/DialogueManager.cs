using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance;

    public UIDialogue dialogueBox;

    bool isPlaying = false;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    private void Update()
    {
        if (isPlaying)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                NextDialogue();
            }
        }
    }

    private DialogueData nowDialogue;
    private int          idx;

    public void StartDialogue(DialogueData dialogueData)
    {
        isPlaying   = true;
        idx         = 0;
        nowDialogue = dialogueData;
        dialogueBox.gameObject.SetActive(true);
        NextDialogue();
    }

    public void NextDialogue()
    {
        if (nowDialogue.dialogues.Length <= idx)
        {
            isPlaying = false;
            dialogueBox.gameObject.SetActive(false);
            return;
        }

        dialogueBox.SetText(nowDialogue.dialogues[idx]);
        idx += 1;
    }
}