using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scene : MonoBehaviour
{
    public bool isDead { get; protected set; }

    public void Init()
    {
        isDead = false;
    }

    void OnEnable()
    {
        Init();
    }

    void Awake()
    {
        Init();
    }

    void Update()
    {
    }
    
}