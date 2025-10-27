using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CommonUIManager : MonoBehaviour
{
    public static CommonUIManager Instance;
    
    [SerializeField]         TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI countDownText;
    [SerializeField] TextMeshProUGUI gameOverText;

    private UIMiniGameScore[] gameScores;

    private void Awake()
    {
        if(Instance == null)
            Instance = this;
        
        scoreText.gameObject.SetActive(false);
        countDownText.gameObject.SetActive(false);
        
        gameScores = GameObject.FindObjectsOfType<UIMiniGameScore>(true);
        
    }

    public void SetActiveScore(bool active)
    {
        scoreText.gameObject.SetActive(active);
        scoreText.text = "Score : 0";
    }

    public void UpdateScore(int score)
    {
        scoreText.text = "Score : " + score;
    }

    public void ShowGameOver()
    {
        gameOverText.gameObject.SetActive(true);
        StartCoroutine(Utility.DelayAction(3.0f, () =>
        {
            gameOverText.gameObject.SetActive(false);
        }));
    }

    public void StartCountDown(float _time =3.0f)
    {
        StartCoroutine(StartCountDownRoutine(_time));
    }

    IEnumerator StartCountDownRoutine(float _time)
    {
        float countDownTime = _time;
        countDownText.gameObject.SetActive(true);
        countDownText.text = ((int)countDownTime).ToString();
        while (countDownTime > 0)
        {
            yield return new WaitForSecondsRealtime(1);
            countDownTime -= 1;
            countDownText.text = ((int)countDownTime).ToString();
        }
        
        countDownText.gameObject.SetActive(false);
    }

    public void UpdateHighScore()
    {
        foreach (UIMiniGameScore score in gameScores)
        {
            score.UpdateHighScore();
        }
    }
}
