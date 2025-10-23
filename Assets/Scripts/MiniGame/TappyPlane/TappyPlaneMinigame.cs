using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TappyPlaneMinigame : MonoBehaviour
{
    [SerializeField] TappyPlaneBackground background;
    [SerializeField] TappyPlanePlayer player;

    private Vector3 playerOriginPos;
    private Vector3 backgroundOriginPos;

    void Awake()
    {
        playerOriginPos = player.transform.position;
        backgroundOriginPos = background.transform.position;
    }
    
    void OnEnable()
    {
        Init();
    }

    void Init()
    {
        player.transform.position = playerOriginPos;
        background.transform.position = backgroundOriginPos;
        
        this.transform.position = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, 0); 
        background.Init();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
