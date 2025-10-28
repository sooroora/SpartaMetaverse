using UnityEngine;

public class ResourceController : MonoBehaviour
{
    [SerializeField] private float healthChangeDelay = .5f;

    private CharacterShootingGame    character;
    private CharacterStatHandler     statHandler;
    private CharacterAnimatorHandler animationHandler;
    
    private float timeSinceLastChange = float.MaxValue;
    
    public GameObject hpBar;

    public float CurrentHp { get; private set; }
    public float MaxHp     => statHandler.MaxHp;

    private void Awake()
    {
        Init();
    }

    public void Init()
    {
        character        = this.GetComponent<CharacterShootingGame>();
        statHandler      = GetComponent<CharacterStatHandler>();
        animationHandler = GetComponent<CharacterAnimatorHandler>();
        CurrentHp        = statHandler.MaxHp;
    }
    
    private void Update()
    {
        if (timeSinceLastChange < healthChangeDelay)
        {
            timeSinceLastChange += Time.deltaTime;
            if (timeSinceLastChange >= healthChangeDelay)
            {
                animationHandler.InvincibilityEnd();
            }
        }
    }

    public bool ChangeHealth(float change)
    {
        if (change == 0 || timeSinceLastChange < healthChangeDelay)
        {
            return false;
        }
        timeSinceLastChange =  0f;
        CurrentHp       += change;
        CurrentHp       =  CurrentHp > MaxHp ? MaxHp : CurrentHp;
        CurrentHp       =  CurrentHp < 0 ? 0 : CurrentHp;
        
        hpBar.transform.localScale = new Vector3(CurrentHp / MaxHp, 1f, 1f);
        
        if (change < 0)
        {
            character?.GetDamage();
            animationHandler.Damage();
        }

        if (CurrentHp <= 0f)
        {
            Death();
        }

        return true;
    }

    private void Death()
    {
        character?.Death();
    }

}