using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterPlayer : MonoBehaviour
{
    Vector2 inputVec;
    Vector3 mousePos;

    bool canMove = true;

    Animator anim;
    Rigidbody2D rb;
    SpriteRenderer spr;

    public float moveSpeed;

    bool isRolling = false;
    AnimationState animationState;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        playerMove = GetComponent<PlayerMove>();
        canMove = true;
        moveSpeed = 5.0f;
    }
    void Start()
    {
        isRolling = false;
        animationState = AnimationState.Idle;
    }

    void SetAnimation()
    {
        switch (animationState)
        {
            case AnimationState.Idle:
                anim.SetInteger("PlayerState", 0);
                break;
            case AnimationState.Walk:
                anim.SetInteger("PlayerState", 1);
                break;
            case AnimationState.Roll:
                anim.SetInteger("PlayerState", 2);
                break;
            case AnimationState.Hit:
                anim.SetInteger("PlayerState", 3);
                break;
            case AnimationState.Dead:
                anim.SetInteger("PlayerState", 4);
                break;
        }
    }
    void Update()
    {
        inputVec.x = Input.GetAxisRaw("Horizontal");
        inputVec.y = Input.GetAxisRaw("Vertical");
        MouseMove();

        if (inputVec.x == 0 && inputVec.y == 0)
            animationState = AnimationState.Idle;
        else
            animationState = AnimationState.Walk;
        if (Input.GetMouseButtonDown(1))
            animationState = AnimationState.Roll;
    }
    void FixedUpdate()
    {
        if (canMove)
        {
            if (!isRolling)
            {
                //rb.velocity = inputVec * speed;
                Vector2 nextVec = inputVec.normalized * moveSpeed * Time.fixedDeltaTime;
                rb.MovePosition(rb.position + nextVec);
            }
        }
    }
 
    void LateUpdate()
    {
        SetAnimation();
    }
    void MouseMove()
    {
        mousePos = Input.mousePosition;
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);
        if (transform.position.x - mousePos.x > 0)
            spr.flipX = true;
        else
            spr.flipX = false;
    }
    IEnumerator Roll(Vector2 rollDir)
    {
        isRolling = true;
        rb.velocity = rollDir * 3 * moveSpeed;
        yield return new WaitForSeconds(0.2f);
        isRolling = false;
        rb.velocity = Vector2.zero;
    }
    PlayerMove playerMove;
    float maxHealth = 150;
    float currentHealth;
    float damage;
    public void Damaged(float damage)
    {
        if (currentHealth > 0)
        {
            currentHealth -= damage;
            GameManager.instance.SetHealth(currentHealth);
        }
        else
            playerMove.Dead();
    }
    public void Dead()
    {
        // Á×À½ ¸ð¼Ç
    }
}
