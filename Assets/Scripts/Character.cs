using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    protected               CharacterAnimatorHandler anim;
    protected               SpriteSortOrder          sortOrder;
    
    // 몬스터 나오면 분리 필요
    [SerializeField] public PlayerControlData        controlData;


    protected void Awake()
    {
        sortOrder = this.GetComponent<SpriteSortOrder>();
        anim      = GetComponent<CharacterAnimatorHandler>();
    }

    protected void Update()
    {
        Move();
    }

    protected virtual void Move()
    {
        // 일정하게 움직일 수 있도록~ prev에서 target으로 lerp 하기~
        // if (controlData.moveLerpT < 1)
        // {
        //     controlData.moveLerpT += Time.deltaTime * controlData.moveSpeed;
        //     this.transform.position =  Vector3.Lerp(controlData.prevWorldPos, controlData.targetWorldPos, controlData.moveLerpT);
        //     anim.SetWalking(true);
        //     anim.SetDirection(Vector3.Normalize(controlData.targetWorldPos - controlData.prevWorldPos));
        // }
        // else
        // {
        //     controlData.prevWorldPos = controlData.targetWorldPos;
        //     anim.SetWalking(false);
        // }
    }
}