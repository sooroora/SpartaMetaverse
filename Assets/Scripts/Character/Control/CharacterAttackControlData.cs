using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class CharacterAttackControlData
{
    [SerializeField] private float speed = 1f;
    public                   float delay = 1f;

    public float Speed
    {
        get => speed;
        set => speed = value;
    }


    public float nowDelay = 0f;
    public bool  attackable { get; private set; } = true;

    public bool GetAttackable()
    {
        if (nowDelay > 0)
            return false;
        else if (attackable == false)
            return false;

        return true;
    }

    // 넉백중일때공격 못하게
    public void SetAttackable(bool _attackable)
    {
        attackable = _attackable;
    }

    public void Attack()
    {
        nowDelay = delay;
    }
    
    
}