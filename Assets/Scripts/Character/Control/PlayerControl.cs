using System;
using UnityEngine;
using UnityEngine.Tilemaps;

[Serializable]
public class PlayerControl : MonoBehaviour
{
    public Character character;

    protected void Awake()
    {
        
    }

    // Update is called once per frame
    public virtual void ManualUpdate()
    {
        if (character == null)
            return;

        PlayerInput();
    }

    protected virtual void PlayerInput()
    {
        // zep 처럼 움직이기
        // 한칸씩 lerp하면서
        // 꾹 눌렀을 때도 가능
        if (character.moveControlData.prevWorldPos != character.moveControlData.targetWorldPos)
            return;


        if (Input.GetKey(KeyCode.RightArrow))
        {
            character.moveControlData.SetLastDirection(Vector3Int.right);
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            character.moveControlData.SetLastDirection(Vector3Int.left);
        }
        else if (Input.GetKey(KeyCode.UpArrow))
        {
            character.moveControlData.SetLastDirection(Vector3Int.up);
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            character.moveControlData.SetLastDirection(Vector3Int.down);
        }
        else
            return;

        Vector3 dirFloat = character.moveControlData.lastDirection;
        RaycastHit2D[] hits = Physics2D.LinecastAll(character.transform.position,
            character.transform.position + (dirFloat * character.moveControlData.collisionCheckDis));

        foreach (RaycastHit2D hit in hits)
        {
            if (hit.collider is TilemapCollider2D == true)
            {
                return;
            }
        }

        character.moveControlData.SetNextPositionDir(character.moveControlData.lastDirection);
    }


    public virtual void SetTargetPlayer(Player player)
    {
        character = player;
        character.moveControlData.Init(character.transform);
    }
}