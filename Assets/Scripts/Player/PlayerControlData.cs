using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerControlData
{
    // 타일맵
    public readonly float collisionCheckDis = 0.14f;

    public bool  isBottomPivot;
    public float moveSpeed = 5;
    public Grid  grid;

    public float      moveLerpT      { get; set; }
    public Vector3Int nowGridPos     { get; set; }
    public Vector3    prevWorldPos   { get; set; }
    public Vector3    targetWorldPos { get; set; }
    public Vector3Int lastDirection  { get; set; }

    private Action OnMoveAction;

    public void Init(Transform transform)
    {
        moveLerpT = 0;

        nowGridPos = grid.WorldToCell(transform.position);
        Vector3 startPos = grid.GetCellCenterWorld(nowGridPos) - (isBottomPivot ? Vector3.down * 0.08f : Vector3.zero);

        transform.position = startPos;
        prevWorldPos       = startPos;
        targetWorldPos     = prevWorldPos;
    }

    public void AddOnMoveAction(Action action)
    {
        OnMoveAction += action;
    }

    public void SetNextPositionDir(Vector3Int _dir)
    {
        moveLerpT      =  0;
        nowGridPos     += (_dir);
        lastDirection = _dir;
        
        targetWorldPos =  grid.GetCellCenterWorld(nowGridPos) - (isBottomPivot ? Vector3.down * 0.1f : Vector3.zero);
        
        OnMoveAction?.Invoke();
    }

    public void SetLastDirection(Vector3Int _dir)
    {
        lastDirection = _dir;
    }
    
}