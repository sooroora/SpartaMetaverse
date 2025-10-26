using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Search;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerControl : MonoBehaviour
{
    public PlayerControl Instance;
    public Character     animTarget;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (animTarget == null)
            return;

        Input();
    }
    
    void Input()
    {
        // zep 처럼 움직이기
        // 한칸씩 lerp하면서
        // 꾹 눌렀을 때도 가능
        if (animTarget?.controlData.prevWorldPos != animTarget?.controlData.targetWorldPos)
            return;


        if (UnityEngine.Input.GetKey(KeyCode.RightArrow))
        {
            animTarget.controlData.SetLastDirection(Vector3Int.right);
        }
        else if (UnityEngine.Input.GetKey(KeyCode.LeftArrow))
        {
            animTarget.controlData.SetLastDirection(Vector3Int.left);
        }
        else if (UnityEngine.Input.GetKey(KeyCode.UpArrow))
        {
            animTarget.controlData.SetLastDirection(Vector3Int.up);
        }
        else if (UnityEngine.Input.GetKey(KeyCode.DownArrow))
        {
            animTarget.controlData.SetLastDirection(Vector3Int.down);
        }
        else
            return;

        Vector3 dirFloat = animTarget.controlData.lastDirection;
        RaycastHit2D[] hits = Physics2D.LinecastAll(animTarget.transform.position,
            animTarget.transform.position + (dirFloat * animTarget.controlData.collisionCheckDis));

        foreach (RaycastHit2D hit in hits)
        {
            if (hit.collider is TilemapCollider2D == true)
            {
                return;
            }
        }

        animTarget.controlData.SetNextPositionDir(animTarget.controlData.lastDirection);
    }


   


    public virtual void SetTargetPlayer(Player player)
    {
        animTarget = player;

        animTarget.controlData.Init(animTarget.transform);
    }
}