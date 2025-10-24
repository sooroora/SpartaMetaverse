using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Search;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerControl : MonoBehaviour
{
    public Player targetPlayer;

    // [SerializeField] private bool  isBottomPivot;
    // [SerializeField] private float moveSpeed = 5;
    // [SerializeField] private Grid  targetGrid;
    // private const            float collisionCheckDis = 0.14f;
    // protected                Grid  grid;
    //
    //
    // public  Vector3Int nowGridPos     { get; private set; }
    // public  Vector3    targetWorldPos { get; private set; }
    // public  Vector3    prevWorldPos   { get; private set; }
    // private float      moveLerpT;
    //
    // private Action OnMoveAction;
    

    void Input()
    {
        // zep 처럼 움직이기
        // 한칸씩 lerp하면서
        // 꾹 눌렀을 때도 가능
        if (targetPlayer?.controlData.prevWorldPos != targetPlayer?.controlData.targetWorldPos)
            return;

        Vector3Int dir = Vector3Int.zero;

        if (UnityEngine.Input.GetKey(KeyCode.RightArrow))
        {
            dir = Vector3Int.right;
        }
        else if (UnityEngine.Input.GetKey(KeyCode.LeftArrow))
        {
            dir = Vector3Int.left;
        }
        else if (UnityEngine.Input.GetKey(KeyCode.UpArrow))
        {
            dir = Vector3Int.up;
        }
        else if (UnityEngine.Input.GetKey(KeyCode.DownArrow))
        {
            dir = Vector3Int.down;
        }
        else
            return;

        Vector3 dirFloat = dir;
        RaycastHit2D[] hits = Physics2D.LinecastAll(targetPlayer.transform.position,
            targetPlayer.transform.position + (dirFloat * targetPlayer.controlData.collisionCheckDis));
        // Debug.DrawLine(this.transform.position, this.transform.position + (dirFloat * targetPlayer.controlData.collisionCheckDis), Color.red, 3f);

        foreach (RaycastHit2D hit in hits)
        {
            if (hit.collider is TilemapCollider2D == true)
            {
                return;
            }
        }

        targetPlayer.controlData.SetNextPositionDir(dir);
        //
        // moveLerpT      =  0;
        // nowGridPos     += (dir);
        // targetWorldPos =  grid.GetCellCenterWorld(nowGridPos) - (isBottomPivot ? Vector3.down * 0.1f : Vector3.zero);
        // OnMoveAction?.Invoke();
    }

       
    private void Awake()
    {
        //Init();
    }

    // Update is called once per frame
    void Update()
    {
        if (targetPlayer == null)
            return;

        Input();
    }



    public virtual void SetTargetPlayer(Player player)
    {
        targetPlayer = player;
        
        targetPlayer.controlData.Init(targetPlayer.transform);
        
        // if (targetGrid == null)
        //     grid = FindObjectOfType<Grid>();
        //
        // grid = targetGrid;
        //
        // nowGridPos = grid.WorldToCell(transform.position);
        // Vector3 startPos = grid.GetCellCenterWorld(nowGridPos) - (isBottomPivot ? Vector3.down * 0.08f : Vector3.zero);
        //
        // this.transform.position = startPos;
        // prevWorldPos            = startPos;
        // targetWorldPos          = prevWorldPos;
    }

}