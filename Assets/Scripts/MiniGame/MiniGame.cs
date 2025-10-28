using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MiniGameType
{
    TappyPlane   = 0,
    StackGame    = 1,
    ShootingGame = 2,
}

public class MiniGame : MonoBehaviour
{
    protected FollowingCamera followingCam;
    public bool               isDead { get; protected set; }

    public bool isPlaying;
    
    public virtual void Init()
    {
        isDead            = false;
        isPlaying        = false;
        followingCam      = Camera.main.GetComponent<FollowingCamera>();
        ControlManager.Instance.SetControlPlayer(null);
    }


    void OnDisable()
    {
        Release();
    }

    protected virtual void Update()
    {
    }

    public virtual void Release()
    {
        if(this == null) return;
        
        followingCam?.SetMetaverseCamera();
        MetaverseGameManager.Instance.EnterMetaverse();
        CommonUIManager.Instance?.UpdateHighScore();
    }

    public virtual void GameStart()
    {
        isPlaying      = true;
    }

    public virtual void GameOver()
    {
        
    }


    void OnEnable()
    {
        Init();
    }
}