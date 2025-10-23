using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGame : MonoBehaviour
{
    protected Camera  cam;
    protected Vector3 camOriginPosition;
    float             camOrthoSize;
    public bool       isDead { get; protected set; }

    public virtual void Init()
    {
        isDead            = false;
        cam               = Camera.main;
        camOriginPosition = Camera.main.transform.position;
        camOrthoSize      = cam.orthographicSize;
    }


    void OnDisable()
    {
        Release();
    }

    public virtual void Release()
    {
        if (cam == null) return;

        cam.orthographic     = true;
        cam.orthographicSize = camOrthoSize;

        cam.transform.parent   = null;
        cam.transform.position = camOriginPosition;
        cam.transform.rotation = Quaternion.identity;
    }

    public virtual void GameStart()
    {
    }


    void OnEnable()
    {
        Init();
    }
}