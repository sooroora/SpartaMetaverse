using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingMiniGame : MiniGame
{
    [SerializeField] private Player shootingPlayer;
    public override void Init()
    {
        base.Init();
        followingCam.SetTarget(shootingPlayer.gameObject);
        
    }
}
