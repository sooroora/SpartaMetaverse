using UnityEngine;

public class ResourceController : MonoBehaviour
{
    [SerializeField] private float healthChangeDelay = .5f;

    private BaseController       baseController;
    private CharacterStatHandler statHandler;
    private CharacterAnimatorHandler     animationHandler;
    
    private float timeSinceLastChange = float.MaxValue;

    public float CurrentHp { get; private set; }
    public float MaxHp     => statHandler.MaxHp;

    private void Awake()
    {
        statHandler      = GetComponent<CharacterStatHandler>();
        animationHandler = GetComponent<CharacterAnimatorHandler>();
        baseController   = GetComponent<BaseController>();
    }

    private void Start()
    {
        CurrentHp = statHandler.MaxHp;
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

        if (change < 0)
        {
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
        baseController?.Death();
    }

}