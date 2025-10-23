using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGame : MonoBehaviour
{
    protected Camera    cam;
    protected Vector3 camOriginPosition;
    protected bool      ortho;
    
    public    bool      isDead { get; protected set; }

    public virtual void Init()
    {
        isDead            = false;
        cam               = Camera.main;
        camOriginPosition = Camera.main.transform.position;
    }

    public virtual void Release()
    {
        cam.orthographic       = false;
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

    void OnDisable()
    {
        Release();
    }
}