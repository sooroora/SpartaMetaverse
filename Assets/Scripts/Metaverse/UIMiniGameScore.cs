using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIMiniGameScore : MonoBehaviour
{
    public MiniGameType miniGameType;
    public TextMeshProUGUI highScoreText;

    private void Start()
    {
        UpdateHighScore();
    }

    public void UpdateHighScore()
    {
        highScoreText.text = MiniGameSaveData.GetMiniGameHighScore(miniGameType).ToString();
    }
}
