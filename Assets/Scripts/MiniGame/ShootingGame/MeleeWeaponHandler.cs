using UnityEngine;

public class MeleeWeaponHandler : WeaponHandler
{
    [Header("Melee Attack Info")]
    public Vector2 collideBoxSize = Vector2.one;

    protected override void Start()
    {
        base.Start();
        collideBoxSize = collideBoxSize * WeaponSize;
    }

    public override void Attack()
    {
        base.Attack();
        
        // RaycastHit2D hit =  Physics2D.BoxCast(transform.position + (Vector3)character.moveControlData.lastDirection * collideBoxSize.x, 
        //     collideBoxSize, 0, Vector2.zero, 0, target);
        //
        Vector3 dirFloat = character.moveControlData.lastDirection;
        RaycastHit2D hit =
            Physics2D.CircleCast(character.transform.position + (dirFloat * 0.08f),
                0.16f, dirFloat, 0.0f, target);

        if (hit.collider != null)
        {
            ResourceController resourceController = hit.collider.GetComponent<ResourceController>();
            if(resourceController != null)
            {
                resourceController.ChangeHealth(-Power);
                if(IsOnKnockback)
                {
                    // 넉백은 없어... 타일맵 이동에 맞게 만들어야하는데 아직..
                }
            }
        }
    }

    public override void Rotate(bool isLeft)
    {
        if(isLeft)
            transform.eulerAngles = new Vector3(0, 180, 0);
        else
            transform.eulerAngles = new Vector3(0, 0, 0);
    }
}