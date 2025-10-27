using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyManager : MonoBehaviour
{
    private Coroutine waveRoutine;
        
    [SerializeField]
    private List<GameObject> enemyPrefabs; // 생성할 적 프리팹 리스트

    [SerializeField]
    private List<Rect> spawnAreas; // 적을 생성할 영역 리스트

    [SerializeField]
    private Color gizmoColor = new Color(1, 0, 0, 0.3f); // 기즈모 색상

    private List<EnemyShootingGame> activeEnemies = new List<EnemyShootingGame>(); // 현재 활성화된 적들

    private bool enemySpawnComplite;
    
    [SerializeField] private float timeBetweenSpawns = 0.2f;
    [SerializeField] private float timeBetweenWaves = 1f;

    private ShootingMiniGame shootingGameManager;

    public void Init(ShootingMiniGame gameManager)
    {
        shootingGameManager = gameManager;
    }

    public void StartWave(int waveCount)
    {
        if (waveCount <= 0)
        {
            shootingGameManager.EndOfWave();
            return;
        }
    
        if(waveRoutine != null)
            StopCoroutine(waveRoutine);
        waveRoutine =  StartCoroutine(SpawnWave(waveCount));
    }

    public void StopWave()
    {
        StopAllCoroutines();
    }

    private IEnumerator SpawnWave(int waveCount)
    {
        enemySpawnComplite = false;
        yield return new WaitForSeconds(timeBetweenWaves);
        for (int i = 0; i < waveCount; i++)
        {
            yield return new WaitForSeconds(timeBetweenSpawns); 
            SpawnRandomEnemy();
        }

        enemySpawnComplite = true;
    }

    private void SpawnRandomEnemy()
    {
        if (enemyPrefabs.Count == 0 || spawnAreas.Count == 0)
        {
            Debug.LogWarning("Enemy Prefabs 또는 Spawn Areas가 설정되지 않았습니다.");
            return;
        }

        // 랜덤한 적 프리팹 선택
        GameObject randomPrefab = enemyPrefabs[Random.Range(0, enemyPrefabs.Count)];

        // 랜덤한 영역 선택
        Rect randomArea = spawnAreas[Random.Range(0, spawnAreas.Count)];
        
        
        // Rect 영역 내부의 랜덤 위치 계산
        Vector2 randomPosition = new Vector2(
            Random.Range(randomArea.xMin, randomArea.xMax),
            Random.Range(randomArea.yMin, randomArea.yMax)
        );

        Vector3 worldPos = this.transform.TransformPoint(randomPosition);
        
        // 적 생성 및 리스트에 추가
        GameObject spawnedEnemy = Instantiate(randomPrefab, new Vector3(worldPos.x, worldPos.y), Quaternion.identity);
        spawnedEnemy.transform.SetParent(shootingGameManager.transform);
        EnemyShootingGame enemy = spawnedEnemy.GetComponent<EnemyShootingGame>();
        enemy.Init(this,shootingGameManager.ShootingPlayer.transform);
        
        activeEnemies.Add(enemy);
        
    }

    // 기즈모를 그려 영역을 시각화 (선택된 경우에만 표시)
    private void OnDrawGizmosSelected()
    {
        if (spawnAreas == null) return;

        Gizmos.color = gizmoColor;
        foreach (var area in spawnAreas)
        {
            // 미니게임들이 로컬 안에 있어서 변환해줘야함
            Matrix4x4 matrix = Matrix4x4.TRS(this.transform.position, this.transform.rotation, this.transform.lossyScale);
            Gizmos.matrix = matrix;
            
            Vector3 center = new Vector3(area.x + area.width / 2, area.y + area.height / 2);
            Vector3 size = new Vector3(area.width, area.height);
            Gizmos.DrawCube(center, size);
        }
        Gizmos.matrix = Matrix4x4.identity;
    }

    private void Update()
    {
        // if (Input.GetKeyDown(KeyCode.Space))
        // {
        //     StartWave(1);
        // }
        
    }
    
    public void RemoveEnemyOnDeath(EnemyShootingGame enemy)
    {
        activeEnemies.Remove(enemy);
        if (enemySpawnComplite && activeEnemies.Count == 0)
            shootingGameManager.EndOfWave();
    }

    public void RemoveAllEnemies()
    {
        foreach (EnemyShootingGame enemy in activeEnemies)
        {
            Destroy(enemy.gameObject);
        }
        
        activeEnemies.Clear();
    }
}
