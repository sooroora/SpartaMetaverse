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

    void Awake()
    {
    }

    public override void Init()
    {
        base.Init();

        followingCam.SetIsFixed(true);
        followingCam.transform.position = this.transform.position + (Vector3.back * 10);
        followingCam.SetOrthographicSize(3.0f);
    
        // 위치 초기화
        player.transform.localPosition     = playerOriginPos;
        player.transform.rotation          = Quaternion.identity;
        background.transform.localPosition = backgroundOriginPos;

        background.Init();
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();
    }
}