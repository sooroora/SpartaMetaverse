using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;

public class ShootingGamePlayerControl : PlayerControl
{
    PlayerShootingGame player;



    protected void Awake()
    {
        player = character as PlayerShootingGame;
        
    }

    public override void ManualUpdate()
    {
        base.ManualUpdate();

        // 딜레이 
    }

    protected override void PlayerInput()
    {
        base.PlayerInput();

        //if(player.attackControlData.Delay)


        if (Input.GetKeyDown(KeyCode.Space))
        {
            player.Attack();
            // if (player.attackControlData.GetAttackable())
            // {
            //     player.Attack();
            //     player.attackControlData.Attack();
            // }
            //if (player.attackControlData.attackable)
            //player.attackControlData
        }
    }
}