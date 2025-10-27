using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingMiniGame : MiniGame
{
    [SerializeField] private PlayerShootingGame shootingPlayer;

    public PlayerShootingGame ShootingPlayer
    {
        get => shootingPlayer;
        private set => shootingPlayer = value;
    }

    private EnemyManager       enemyManager;
    private ResourceController _playerResourceController;

    [SerializeField] private int currentWaveIndex = 0;


    public override void Init()
    {
        base.Init();

        // 제자리로
        shootingPlayer.Init(this);
        
        followingCam.SetTarget(shootingPlayer.gameObject);

        // 이 미니게임에서 쓸 카메라 세팅
        followingCam.SetLimitCamPosX(this.transform.position.x, this.transform.position.x);
        followingCam.SetLimitCamPosY(this.transform.position.y, this.transform.position.y + 2.4f);

        ControlManager.Instance.SetControlPlayer(shootingPlayer.GetComponent<PlayerControl>());

        enemyManager = GetComponentInChildren<EnemyManager>();
        enemyManager.Init(this);
        
        
        CommonUIManager.Instance.StartCountDown(3.0f);
        StartCoroutine(Utility.DelayActionRealTime(3.0f, GameStart));
    }


    public override void GameStart()
    {
        base.GameStart();
        CommonUIManager.Instance.SetActiveScore(true);
        currentWaveIndex = 0;
        StartNextWave();
    }

    void StartNextWave()
    {
        currentWaveIndex += 1;
        enemyManager.StartWave(1 + currentWaveIndex / 5);
        
        CommonUIManager.Instance.UpdateScore(currentWaveIndex);
    }

    public void EndOfWave()
    {
        StartNextWave();
    }

    public override void GameOver()
    {
        base.GameOver();
        
        isPlaying = false;
        enemyManager.StopWave();
        enemyManager.RemoveAllEnemies();
        
        CommonUIManager.Instance.ShowGameOver();
        MiniGameSaveData.SaveMiniGameHighScore(MiniGameType.ShootingGame,currentWaveIndex);

        StartCoroutine(Utility.DelayActionRealTime(3.0f, () =>
        {
            this.gameObject.SetActive(false);
            CommonUIManager.Instance.SetActiveScore(false);
        }));
        

    }
}