using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerShootingGame : CharacterShootingGame
{
    // 플레이어만 따로 할 수 있는 행동이 있으면 해주기
    // 근데 없당

    ShootingMiniGame nowMiniGame;
    SoundManager soundManager;

    public void Init(ShootingMiniGame miniGame)
    {
        nowMiniGame             = miniGame;
        this.transform.position = miniGame.transform.position;
        this.moveControlData.Init(this.transform);
        this.GetComponent<ResourceController>().Init();
        soundManager = SoundManager.GetInstance();
        
    }

    public override void GetDamage()
    {
        soundManager.PlayOnce("hit");
    }

    public override void Death()
    {
        nowMiniGame.GameOver();
    }


}