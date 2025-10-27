using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGame : MonoBehaviour
{
    protected FollowingCamera followingCam;
    public bool               isDead { get; protected set; }

    public virtual void Init()
    {
        isDead            = false;
        followingCam      = Camera.main.GetComponent<FollowingCamera>();
        ControlManager.Instance.SetControlPlayer(null);
    }


    void OnDisable()
    {
        Release();
    }

    protected void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            this.gameObject.SetActive(false);
        }
    }

    public virtual void Release()
    {
        followingCam.SetMetaverseCamera();
        ControlManager.Instance.SetControlPlayer(MetaverseGameManager.Instance.player.GetComponent<PlayerControl>());
    }

    public virtual void GameStart()
    {
    }


    void OnEnable()
    {
        Init();
    }
}