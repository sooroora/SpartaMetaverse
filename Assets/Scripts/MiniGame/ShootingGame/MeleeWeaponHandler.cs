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
            //character.transform.position + (dirFloat * character.moveControlData.collisionCheckDis),target);

        // foreach (RaycastHit2D hit in hits)
        // {
        //     
        // }
        if (hit.collider != null)
        {
            Debug.Log(hit.collider.name);
            ResourceController resourceController = hit.collider.GetComponent<ResourceController>();
            if(resourceController != null)
            {
                resourceController.ChangeHealth(-Power);
                if(IsOnKnockback)
                {
                    BaseController controller = hit.collider.GetComponent<BaseController>();
                    if(controller != null)
                    {
                        controller.ApplyKnockback(transform, KnockbackPower, KnockbackTime);
                    }
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