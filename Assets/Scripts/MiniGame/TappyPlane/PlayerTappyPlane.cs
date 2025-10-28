using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTappyPlane : MonoBehaviour
{
    [SerializeField] float       jumpForce;
    private          Rigidbody2D rb;
    private          Animator    anim;
    
    bool isDead = false;
    
    TappyPlaneMiniGame miniGame;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void Init(TappyPlaneMiniGame game)
    {
        miniGame = game;
        isDead   = false;
        anim     = GetComponent<Animator>();
        anim.SetBool("IsDead", isDead);
    }
    
    void Update()
    {
        if(isDead)
            return;
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.velocity = Vector2.up * jumpForce;
            SoundManager.GetInstance().PlayOnce("tappyplane_jump");
        }
        
        // 실제 앞으로 가지 않으니까 요렇게
        float tiltAngle = rb.velocity.y * 5f;         
        tiltAngle = Mathf.Clamp(tiltAngle, -30f, 30f); 
        transform.rotation = Quaternion.Euler(0f, 0f, tiltAngle);
        
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // 어딘가 부딪혔으면 그냥 끝난겨
        isDead = true;
        anim.SetBool("IsDead",isDead);
        miniGame.GameOver();
        
    }
    
    
}