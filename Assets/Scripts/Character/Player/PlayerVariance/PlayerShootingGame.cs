using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerShootingGame : CharacterShootingGame
{
    //[SerializeField] public CharacterAttackControlData attackControlData;

    protected WeaponHandler weaponHandler;


    protected void Awake()
    {
        base.Awake();

        if (WeaponPrefab != null)
            weaponHandler = Instantiate(WeaponPrefab, weaponPivot);
        else
            weaponHandler = GetComponentInChildren<WeaponHandler>();
    }

    protected void Update()
    {
        base.Update();

        // if (attackControlData.nowDelay > 0)
        // {
        //     attackControlData.nowDelay -= Time.deltaTime;
        // }
    }

    public void Attack()
    {
        weaponHandler.Attack();
    }

    void CollisionEnter2D(Collision2D collision)
    {
    }
}