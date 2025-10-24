
using UnityEngine;

// 자주 쓰는 애니메이터 파라미터 써두기
public enum AnimParameter
{
    IsWalking,
    Hit,
}

public class CharacterAnimator : MonoBehaviour
{
    [SerializeField] private bool useSelfFlip = true;

    Animator animator;

    private Vector3 prevPos;
    
    // Start is called before the first frame update
    public void Awake()
    {
        animator = GetComponent<Animator>();
        prevPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
      
        
    }

    private void LateUpdate()
    {
        if (prevPos != transform.position)
        {
            Vector3 dir = Vector3.Normalize(transform.position - prevPos);
            animator.SetBool(nameof(AnimParameter.IsWalking),true);
            
            if(dir == Vector3.left)
                this.transform.localScale = new Vector3(-1, 1, 1);
            else if(dir == Vector3.right)
                this.transform.localScale = new Vector3(1, 1, 1);
        }
        else
        {
            animator.SetBool(nameof(AnimParameter.IsWalking),false);
        }
        
        prevPos = this.transform.position;
    }
}