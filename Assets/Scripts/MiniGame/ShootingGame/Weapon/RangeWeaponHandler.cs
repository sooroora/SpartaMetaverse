using System;
using JetBrains.Annotations;
using UnityEngine;
using Random = UnityEngine.Random;

public class RangeWeaponHandler : WeaponHandler
{
    [Header("Ranged Attack Data")]
    [SerializeField] private Transform projectileSpawnPosition;
    
    [SerializeField] private int bulletIndex;
    public                   int BulletIndex {get{return bulletIndex;}}
  
    [SerializeField] private float bulletSize = 1;
    public                   float BulletSize {get{return bulletSize;}}
    
    [SerializeField] private float duration;
    public                   float Duration {get{return duration;}}
    
    [SerializeField] private float spread;
    public                   float Spread {get{return spread;}}
    
    [SerializeField] private int numberofProjectilesPerShot;
    public                   int NumberofProjectilesPerShot {get{return numberofProjectilesPerShot;}}
    
    [SerializeField] private float multipleProjectilesAngel;
    public                   float MultipleProjectilesAngel {get{return multipleProjectilesAngel;}}
    
    [SerializeField] private Color projectileColor;
    public                   Color ProjectileColor {get{return projectileColor;}}

    private ProjectileManager projectileManager;

    protected override void Start()
    {
        base.Start();
        projectileManager = ProjectileManager.Instance;
    }

    private void Update()
    {
        if(character==null)
            return;
        
        Vector2 dir   = (Vector3)character.moveControlData.lastDirection;
        // 활 각도 계산
        float   angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        if (character?.transform.localScale.x < 0)
        {
            angle =  180 - angle;
            angle *= -1f;
        }
        
        this.transform.rotation = Quaternion.Euler(0, 0, angle); 
        
        // 스페이스로만 공격, 타일맵에 맞게 상하좌우만으로 공격
        //this.transform.rotation = Quaternion.LookRotation(character.moveControlData.lastDirection);
    }

    public override void Attack()
    {
        base.Attack();
        
        float projectilesAngleSpace      = multipleProjectilesAngel;
        int   numberOfProjectilesPerShot = numberofProjectilesPerShot;
        float minAngle = -(numberOfProjectilesPerShot / 2f) * projectilesAngleSpace + 0.5f * multipleProjectilesAngel;

        StartCoroutine(Utility.DelayAction(0.15f,
            () =>
            {
                for (int i = 0; i < numberOfProjectilesPerShot; i++)
                {
                    float angle        = minAngle + projectilesAngleSpace * i;
                    float randomSpread = Random.Range(-spread, spread);
                    angle += randomSpread;
                    CreateProjectile((Vector3)character.moveControlData.lastDirection, angle);
                }
            })
        );


    }
    
    private void CreateProjectile(Vector2 _lookDirection, float angle)
    {
        projectileManager.ShootBullet(
            this,
            projectileSpawnPosition.position,
            RotateVector2(_lookDirection, angle));
    }
    
    private static Vector2 RotateVector2(Vector2 v, float degree)
    {
        return Quaternion.Euler(0, 0, degree) * v;
    }
}