using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteSortOrder))]
public class Character : MonoBehaviour
{
    protected CharacterAnimatorHandler anim;
    protected SpriteSortOrder          sortOrder;
    
    // 몬스터나 NPC 나오면 Move해줄 애 필요 
    
    [SerializeField] public CharacterMoveControlData moveControlData;

    protected virtual void Awake()
    {
        sortOrder = this.GetComponent<SpriteSortOrder>();
        anim      = GetComponent<CharacterAnimatorHandler>();
        sortOrder.Sort();
        moveControlData.Init(this.transform);
    }

    protected virtual void Update()
    {
        Move();
    }

    protected virtual void Move()
    {
        // 일정하게 움직일 수 있도록~ prev에서 target으로 lerp 하기~
        if (moveControlData.moveLerpT < 1)
        {
            anim?.SetWalking(true);
            
            moveControlData.moveLerpT += Time.deltaTime * moveControlData.moveSpeed;
           
            this.transform.position =  Vector3.Lerp(moveControlData.prevWorldPos, 
                moveControlData.targetWorldPos, moveControlData.moveLerpT);
           
            
            anim?.SetDirection(Vector3.Normalize(moveControlData.targetWorldPos - moveControlData.prevWorldPos));
            
            sortOrder.Sort();
        }
        else
        {
            moveControlData.prevWorldPos = moveControlData.targetWorldPos;
            anim?.SetWalking(false);
        }
    }

    protected virtual void Knonckback()
    {
        
    }
}