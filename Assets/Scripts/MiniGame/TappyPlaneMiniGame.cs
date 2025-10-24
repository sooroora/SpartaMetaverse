using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TappyPlaneMiniGame : MiniGame
{
    [SerializeField] BackgroundTappyPlane background;
    [SerializeField] PlayerTappyPlane     player;

    private Vector3 playerOriginPos     = new Vector3(-2, 0, 0);
    private Vector3 backgroundOriginPos = new Vector3(-5.5f, -3, 0);

    void Awake()
    {
    }

    public override void Init()
    {
        base.Init();
    
        followingCam.enabled = false;
        followingCam.transform.position = this.transform.position + (Vector3.back * 10);
        followingCam.SetOrthographicSize(3.0f);

        player.transform.localPosition     = playerOriginPos;
        player.transform.rotation     = Quaternion.identity;
        background.transform.localPosition = backgroundOriginPos;

        //this.transform.position = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, 0);

        background.Init();
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();
    }
}