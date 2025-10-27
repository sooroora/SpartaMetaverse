using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingMiniGame : MiniGame
{
    [SerializeField] private Character shootingPlayer;

    public Character ShootingPlayer
    {
        get => shootingPlayer;
        private set => shootingPlayer = value;
    }

    private EnemyManager enemyManager;

    public override void Init()
    {
        base.Init();
        
        // 제자리로
        shootingPlayer.transform.position = this.transform.position;
        shootingPlayer.moveControlData.Init(shootingPlayer.transform);
        
        followingCam.SetTarget(shootingPlayer.gameObject);

        // 이 미니게임에서 쓸 카메라 세팅
        followingCam.SetLimitCamPosX(this.transform.position.x, this.transform.position.x);
        followingCam.SetLimitCamPosY(this.transform.position.y, this.transform.position.y + 2.4f);

        ControlManager.Instance.SetControlPlayer(shootingPlayer.GetComponent<PlayerControl>());

        enemyManager = GetComponentInChildren<EnemyManager>();
        enemyManager.Init(this);
    }

    private ResourceController _playerResourceController;

    [SerializeField] private int currentWaveIndex = 0;


    public void StartGame()
    {
        StartNextWave();
    }

    void StartNextWave()
    {
        currentWaveIndex += 1;
        enemyManager.StartWave(1 + currentWaveIndex / 5);
    }

    public void EndOfWave()
    {
        StartNextWave();
    }

    public void GameOver()
    {
        enemyManager.StopWave();
        
        //enemyManager.RemoveAllEnemies();
    }

    private void Update()
    {
        base.Update();
        
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            StartGame();
        }
    }

    public override void Release()
    {
        base.Release();
    }
}