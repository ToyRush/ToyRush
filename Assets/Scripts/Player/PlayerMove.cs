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

public enum RushBearAnimation
{
    Idle=0,
    Run=1,
    Dead=2
}

public class PlayerMove : MonoBehaviour
{
    // 컴포넌트
    Animator anim;
    Rigidbody2D rb;
    SpriteRenderer spr;

    Vector2 inputVec; // 플레이어의 이동 방향

    [Header("개발자 변수")]
    WaitForSeconds shootDelay = new WaitForSeconds(0.2f);
    AnimationState animationState;
    RushBearAnimation rushBearAnimation;

    [Header("기획자 변수 : 속도 조절 변수")]
    public float moveSpeed; // 플레이어의 이동 속도
    bool canMove = true; // 플레이어의 움직임 제어
    bool isMove=false;
    bool isRolling = false; // 구르기 제어
    [SerializeField] ParticleSystem moveParticle;
    [SerializeField] ParticleSystem stopParticle;
    [SerializeField] ParticleSystem dahsParticle;
    ParticleSystemRenderer dashRenderer;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        dashRenderer = dahsParticle.GetComponent<ParticleSystemRenderer>();
    }
    void Start()
    {

        isRolling = false;
        animationState = AnimationState.Idle;
    }

    void SetAnimation()
    {
        switch (rushBearAnimation)
        {
            case RushBearAnimation.Idle:
                anim.SetInteger("RushBearState", 0);
                break;
            case RushBearAnimation.Run:
                anim.SetInteger("RushBearState", 1);
                break;
            case RushBearAnimation.Dead:
                anim.SetInteger("RushBearState", 2);
                break;
        }
    }
    void Update()
    {
        inputVec.x = Input.GetAxisRaw("Horizontal");
        inputVec.y = Input.GetAxisRaw("Vertical");
        if ((inputVec.x != 0 || inputVec.y != 0) && !isMove)
        {
            moveParticle.Play();
            isMove = true;
        }
        else if((inputVec.x == 0 && inputVec.y == 0 )&& isMove)
        {
            moveParticle.Stop();
            stopParticle.Play();
            isMove = false;
        }

        if (Input.GetMouseButtonDown(1)&&!isRolling)
            StartCoroutine("Roll", inputVec.normalized);
        if(inputVec.x==0 && inputVec.y==0)
            rushBearAnimation = RushBearAnimation.Idle;
        else
            rushBearAnimation = RushBearAnimation.Run;
        SetAnimation();
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


    IEnumerator Roll(Vector2 rollDir)
    {
        isRolling = true;
        rb.velocity = rollDir *3* moveSpeed;
        yield return new WaitForSeconds(0.2f);
        isRolling = false;
        rb.velocity = Vector2.zero;
        if (rollDir.x > 0)
            dashRenderer.flip = Vector3.right;
        else
            dashRenderer.flip = Vector3.zero;
        dahsParticle.Play();
    }   

    public void Dead()
    {
        // 죽음 모션
    }

    public void OnOffDamaged(bool _isDamaged)
    {
        if (_isDamaged)
            spr.color = new Color(1, 1, 1, 0.4f);
        else
            spr.color = new Color(1, 1, 1, 1f);
    }

}
