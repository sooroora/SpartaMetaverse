using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIDialogue : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;

    public void SetText(string _text)
    {
        this.gameObject.SetActive(true);
        this.text.text = _text;
    }

    private void Update()
    {
        
    }
}
