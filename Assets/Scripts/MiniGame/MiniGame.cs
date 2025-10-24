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
    }

    public virtual void GameStart()
    {
    }


    void OnEnable()
    {
        Init();
    }
}