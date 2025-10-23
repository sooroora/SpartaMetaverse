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

        cam.transform.position = this.transform.position + (Vector3.back * 10);
        cam.orthographicSize   = 3.0f;

        player.transform.localPosition     = playerOriginPos;
        player.transform.rotation     = Quaternion.identity;
        background.transform.localPosition = backgroundOriginPos;

        //this.transform.position = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, 0);

        background.Init();
    }

    // Update is called once per frame
    void Update()
    {
    }
}