using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AnimationState
{
    Idle=0,
    Walk=1,
    Roll=2,
    Hit=3,
    Dead=4
}

public enum Direction
{
    Left,
    Right,
    Middle
}

public class PlayerMove : MonoBehaviour
{
    // ������Ʈ
    Animator anim;
    Rigidbody2D rb;
    SpriteRenderer spr;

    Vector2 inputVec; // �÷��̾��� �̵� ����

    [Header("������ ����")]
    WaitForSeconds shootDelay = new WaitForSeconds(0.2f);
    Direction direction=Direction.Middle;
    AnimationState animationState;


    [Header("��ȹ�� ���� : �ӵ� ���� ����")]
    public float moveSpeed; // �÷��̾��� �̵� �ӵ�
    public int bulletCnt; // ���� ���� �Ѿ� ����
    public int bulletMaxCnt; // �Ѿ� �ִ� ����

    bool canMove = true; // �÷��̾��� ������ ����
    bool isRolling=false; // ������ ����

    public int bulletNum = 0;

    void Awake()    
    {
        rb = GetComponent<Rigidbody2D>();
        spr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        bulletCnt = bulletMaxCnt;
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

        if (Input.GetMouseButtonDown(1)&&!isRolling)
            StartCoroutine("Roll", inputVec.normalized);
        if(inputVec.x==0 && inputVec.y==0)
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
                Vector2 nextVec = inputVec.normalized * moveSpeed * Time.fixedDeltaTime;
                rb.MovePosition(rb.position + nextVec);
            }
        }
    }

    void LateUpdate()
    {
        SetAnimation();
    }

    IEnumerator Roll(Vector2 rollDir)
    {
        isRolling = true;
        rb.velocity = rollDir *3* moveSpeed;
        yield return new WaitForSeconds(0.2f);
        isRolling = false;
        rb.velocity = Vector2.zero;
    }   

    public void Dead()
    {
        // ���� ���
    }

}
