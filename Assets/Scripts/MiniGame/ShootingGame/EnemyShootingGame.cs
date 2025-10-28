using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class EnemyShootingGame : CharacterShootingGame
{
    private EnemyManager enemyManager;
    private Transform    target;
    
    [SerializeField] private float followRange = 15f;
    
    protected override void Awake()
    {
        Grid grid = GameObject.Find("Grid_ShootingGame").GetComponent<Grid>();
        moveControlData.Init(this.transform, grid);
        
        base.Awake();
        
    }

    public void Init(EnemyManager _enemyManager, Transform _target)
    {
        enemyManager = _enemyManager;
        target = _target;
    }
    

    protected override void Update()
    {
        base.Update();
        
        float distance = DistanceToTarget();
        Vector2 direction = DirectionToTarget();
        moveControlData.SetLastDirection(new Vector3Int((int)direction.x, (int)direction.y, 0));
        
        
        isAttacking = false;
        
        if (distance <= followRange)
        {
            if (distance <= weaponHandler.AttackRange)
            {
                int layerMaskTarget = weaponHandler.target;
                RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, weaponHandler.AttackRange * 5.0f,
                    (1 << LayerMask.NameToLayer("Level")) | layerMaskTarget);

                if (hit.collider != null && layerMaskTarget == (layerMaskTarget | (1 << hit.collider.gameObject.layer)))
                {
                    isAttacking = true;
                }
                
                return;
            }
        }
        
        if (moveControlData.prevWorldPos != moveControlData.targetWorldPos)
            return;
        
        if(distance <= weaponHandler.AttackRange)
            return;
        
        
        // 벽 넘어서는 오지마

        Vector3 dirFloat = moveControlData.lastDirection;
        RaycastHit2D[] hits = Physics2D.LinecastAll(this.transform.position,
            this.transform.position + (dirFloat * moveControlData.collisionCheckDis));
        
        foreach (RaycastHit2D hit in hits)
        {
            if (hit.collider is TilemapCollider2D == true)
            {
                return;
            }
        }
        
        moveControlData.SetNextPositionDir(moveControlData.lastDirection);


    }
    
    protected float DistanceToTarget()
    {
        return Vector3.Distance(transform.position, target.position);
    }
    
    protected Vector2 DirectionToTarget()
    { 
        Vector2 dir = target.position - transform.position;

        // 상하 좌우 가까운데로 
        if (Mathf.Abs(dir.x) > Mathf.Abs(dir.y))
            return new Vector2(Mathf.Sign(dir.x), 0f);   // 좌우
        else
            return new Vector2(0f, Mathf.Sign(dir.y));   // 상하
    }


    public override void GetDamage()
    {
        SoundManager.GetInstance().PlayOnce("orc_damage_1",0.5f);
    }

    public override void Death()
    {
        base.Death();
        enemyManager.RemoveEnemyOnDeath(this);
        
    }
}
