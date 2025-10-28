using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TappyPlaneMiniGame : MiniGame
{
    [SerializeField] BackgroundTappyPlane background;
    [SerializeField] PlayerTappyPlane     player;

    // 태피플레인 초기화 값
    private readonly Vector3 playerOriginPos     = new Vector3(-2, 0, 0);
    private readonly Vector3 backgroundOriginPos = new Vector3(-5.5f, -3, 0);

    float playTime = 0.0f;
    
    
    void Awake()
    {
    }

    public override void Init()
    {
        base.Init();

        SoundManager.GetInstance().PlayBgm("bgm_fun",true,0.5f);
        
        followingCam.SetIsFixed(true);
        followingCam.transform.position = this.transform.position + (Vector3.back * 10);
        followingCam.SetOrthographicSize(3.0f);

        // 위치 초기화
        player.Init(this);
        player.transform.localPosition     = playerOriginPos;
        player.transform.rotation          = Quaternion.identity;
        
        background.transform.localPosition = backgroundOriginPos;
        background.Init();

        Time.timeScale = 0;
        CommonUIManager.Instance.StartCountDown(3.0f);
        StartCoroutine(Utility.DelayActionRealTime(3.0f, GameStart));
    }

    public override void GameStart()
    {
        base.GameStart();
        
        playTime       = 0.0f;
        
        Time.timeScale = 1.0f;
        CommonUIManager.Instance.SetActiveScore(true);
        
        isPlaying = true;
    }

    protected override void Update()
    {
        base.Update();

        if (isPlaying)
        {
            playTime += Time.deltaTime;
            CommonUIManager.Instance.UpdateScore((int)playTime);
        }
    }

    public override void GameOver()
    {
        base.GameOver();
        
        isPlaying = false;
        CommonUIManager.Instance.ShowGameOver();
        MiniGameSaveData.SaveMiniGameHighScore(MiniGameType.TappyPlane, (int)playTime);
        
        StartCoroutine(Utility.DelayActionRealTime(3.0f, ()=>
        {
            this.gameObject.SetActive(false);
            CommonUIManager.Instance.SetActiveScore(false);
        }));
    }
}