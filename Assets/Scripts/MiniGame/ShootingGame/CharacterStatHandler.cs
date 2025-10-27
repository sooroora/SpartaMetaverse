using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStatHandler : MonoBehaviour
{
    [Range(1, 100)] [SerializeField] int maxHp;

    public int MaxHp
    {
        get => maxHp;
        set => maxHp = value;
    }

}