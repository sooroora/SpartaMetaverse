using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterShootingGame : Character
{
    protected Rigidbody2D _rigidbody;

    [SerializeField] protected SpriteRenderer characterRenderer;
    [SerializeField] protected Transform      weaponPivot;

    private Vector2 knockback         = Vector2.zero;
    private float   knockbackDuration = 0.0f;

    protected CharacterStatHandler statHandler;

    [SerializeField] public WeaponHandler WeaponPrefab;
    protected               WeaponHandler weaponHandler;

    protected bool  isAttacking;
    private   float timeSinceLastAttack = float.MaxValue;

    protected override void Awake()
    {
        base.Awake();
        
        _rigidbody       = GetComponent<Rigidbody2D>();
        statHandler      = GetComponent<CharacterStatHandler>();

        if (WeaponPrefab != null)
            weaponHandler = Instantiate(WeaponPrefab, weaponPivot);
        else
            weaponHandler = GetComponentInChildren<WeaponHandler>();
    }

    protected virtual void Start()
    {
    }

    protected override void Update()
    {
        base.Update();
        
        HandleAction();
        HandleAttackDelay();
    }

    protected virtual void FixedUpdate()
    {
        //Movment(movementDirection);
        if (knockbackDuration > 0.0f)
        {
            knockbackDuration -= Time.fixedDeltaTime;
        }
    }

    protected virtual void HandleAction()
    {
    }

    private void Movment(Vector2 direction)
    {
        direction = direction * moveControlData.moveSpeed;
        if (knockbackDuration > 0.0f)
        {
            direction *= 0.2f;
            direction += knockback;
        }

        _rigidbody.velocity = direction;
    }

    private void Rotate(Vector2 direction)
    {
        float rotZ   = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        bool  isLeft = Mathf.Abs(rotZ) > 90f;

        characterRenderer.flipX = isLeft;

        if (weaponPivot != null)
        {
            weaponPivot.rotation = Quaternion.Euler(0, 0, rotZ);
        }

        weaponHandler?.Rotate(isLeft);
    }

    public void ApplyKnockback(Transform other, float power, float duration)
    {
        knockbackDuration = duration;
        knockback         = -(other.position - transform.position).normalized * power;
    }

    private void HandleAttackDelay()
    {
        if (weaponHandler == null)
            return;

        if (timeSinceLastAttack <= weaponHandler.Delay)
        {
            timeSinceLastAttack += Time.deltaTime;
        }

        if (isAttacking && timeSinceLastAttack > weaponHandler.Delay)
        {
            timeSinceLastAttack = 0;
            Attack();
        }
    }

    public virtual void Attack()
    {
        weaponHandler?.Attack();
    }

    public virtual void GetDamage()
    {
        
    }
    
    public virtual void Death()
    {
        _rigidbody.velocity = Vector3.zero;

        foreach (SpriteRenderer renderer in transform.GetComponentsInChildren<SpriteRenderer>())
        {
            Color color = renderer.color;
            color.a        = 0.3f;
            renderer.color = color;
        }

        foreach (Behaviour component in transform.GetComponentsInChildren<Behaviour>())
        {
            component.enabled = false;
        }

        Destroy(gameObject, 2f);
    }
}
