using System;
using UnityEngine;

// 자주 쓰는 애니메이터 파라미터 써두기
public enum AnimParameter
{
    IsWalking,
    Direction,
    IsAttack,
    IsDamage,
}

public enum AnimDirection
{
    Forward = 0,
    Back    = 1,
    Right   = 2,
    Left    = 3,
}

public class CharacterAnimatorHandler : MonoBehaviour
{
    [SerializeField] private bool useSelfFlip = true;

    private CharacterMoveControlData moveControlData;

    [SerializeField] Animator animator;


    public void Awake()
    {
        if (animator == null)
            animator = GetComponent<Animator>();
        //animator = GetComponentInChildren<Animator>();

        moveControlData = this.GetComponent<Character>().moveControlData;
    }

    private void Update()
    {
        SetDirection(moveControlData.lastDirection);
    }

    public void SetWalking(bool isWalking)
    {
        animator.SetBool(nameof(AnimParameter.IsWalking), isWalking);
    }

    public void SetDirection(Vector3 dir)
    {
        if (dir == Vector3.up)
        {
            animator.SetFloat(nameof(AnimParameter.Direction), (int)AnimDirection.Back / 4f);
        }
        else if (dir == Vector3.down)
        {
            animator.SetFloat(nameof(AnimParameter.Direction), (int)AnimDirection.Forward / 4f);
        }
        else
        {
            if (useSelfFlip)
            {
                if (dir == Vector3.left)
                    this.transform.localScale = new Vector3(-1, 1, 1);
                else if (dir == Vector3.right)
                    this.transform.localScale = new Vector3(1, 1, 1);
            }
            else
            {
                if (dir == Vector3.right)
                    animator.SetFloat(nameof(AnimParameter.Direction), (int)AnimDirection.Right / 4f);
                else if (dir == Vector3.left)
                    animator.SetFloat(nameof(AnimParameter.Direction), (int)AnimDirection.Left / 4f);
            }
        }
    }

    public void Damage()
    {
        animator.SetTrigger(nameof(AnimParameter.IsDamage));
    }

    public void InvincibilityEnd()
    {
        //animator.SetBool(nameof(AnimParameter.IsDamage), false);
    }
}