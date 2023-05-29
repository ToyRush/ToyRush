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

public enum Direction // 마우스 방향
{
    Left,
    Right,
    Middle
}

public enum PlayerDirection // 방향키로 움직이는 방향
{
    Left,
    Right,
    Up,
    Down
}

public enum RushBearAnimation
{
    Idle=0,
    Run=1,
    Jump=2,
    Fall=3
}

public enum ConsumerType
{
    Heal,
    SpeedUp,
    Shield
}

public class PlayerMove : MonoBehaviour
{
    // 컴포넌트
    Animator anim;
    Animator dashAnim;
    Rigidbody2D rb;
    SpriteRenderer spr;
    DashUI dashUI;
    PlayerStat playerStat;


    Vector2 inputVec; // 플레이어의 이동 방향

    [Header("개발자 변수")]
    WaitForSeconds shootDelay = new WaitForSeconds(0.2f);
    RushBearAnimation rushBearAnimation;
    PlayerDirection playerDirection;

    [Header("기획자 변수 : 속도 조절 변수")]
    public float moveSpeed; // 플레이어의 이동 속도
    public float jumpPower; // 점프 강도
    bool canMove = true; // 플레이어의 움직임 제어
    bool isMove=false;
    bool isDash = false; // 구르기 제어
    bool canDash = true;
    bool canJmup = true;
    float gravity = 2f;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] GameObject dashObject;
    [SerializeField] ParticleSystem moveParticle;
    [SerializeField] ParticleSystem stopParticle;

    Vector3 leftVec = new Vector3(0, 0, 90);
    Vector3 rightVec = new Vector3(0, 0, 270);
    Vector3 downVec= new Vector3(0, 0, 180);
    Vector3 upVec = new Vector3(0, 0, 0);

    public bool bossStage=false;
    
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        dashUI = GetComponentInChildren<DashUI>();
        playerStat = GetComponent<PlayerStat>();
    }
    void Start()
    {
        isDash = false;
        rushBearAnimation = RushBearAnimation.Idle;
        dashAnim = dashObject.GetComponent<Animator>();
    }

    void RunAnimation()
    {
        if (inputVec.x == 0 && inputVec.y == 0)
            rushBearAnimation = RushBearAnimation.Idle;
        else
            rushBearAnimation = RushBearAnimation.Run;
    }

    void JumpAnimation()
    {
        if (rb.velocity.y > 0.1f)
            rushBearAnimation = RushBearAnimation.Jump;
        else if (rb.velocity.y < -0.1f)
            rushBearAnimation = RushBearAnimation.Fall;
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
            default:
                break;
        }
    }

    void SetPlatFormAnimation()
    {
        switch (rushBearAnimation)
        {
            case RushBearAnimation.Idle:
                anim.SetInteger("RushBearState", 0);
                break;
            case RushBearAnimation.Run:
                anim.SetInteger("RushBearState", 1);
                break;
            case RushBearAnimation.Jump:
                anim.SetInteger("RushBearState", 2);
                break;
            case RushBearAnimation.Fall:
                anim.SetInteger("RushBearState", 3);
                break;
        }
    }

    void Update()
    {
        inputVec.x = Input.GetAxisRaw("Horizontal");
        if (!bossStage) // 보스 스테이지 전
        {
            inputVec.y = Input.GetAxisRaw("Vertical");
            if ((inputVec.x != 0 || inputVec.y != 0) && !isMove)
            {
                moveParticle.Play();
                isMove = true;
            }
            else if ((inputVec.x == 0 && inputVec.y == 0) && isMove)
            {
                moveParticle.Stop();
                stopParticle.Play();
                isMove = false;
            }
            CheckMoveDir();

            if (Input.GetMouseButtonDown(1) && !isDash && canDash)
                StartCoroutine("Dash", inputVec.normalized);
            RunAnimation();
            SetAnimation();
        }
        else // 보스 스테이지
        {
            if (Physics2D.Raycast(transform.position, Vector2.down, 0.5f, groundLayer))
                canJmup = true;
            else
                canJmup = false;
            if (Input.GetMouseButtonDown(1) && canJmup) // 점프
            {
                rb.AddForce(Vector3.up * jumpPower, ForceMode2D.Impulse);
                canJmup = false;
            }
            RunAnimation();
            JumpAnimation();
            SetPlatFormAnimation();
        }
    }
    void FixedUpdate()
    {
        if (!bossStage)
        {
            if (canMove)
            {
                if (!isDash)
                {
                    Vector2 nextVec = inputVec.normalized * moveSpeed * Time.fixedDeltaTime;
                    rb.MovePosition(rb.position + nextVec);
                }
            }
        }
        else
        {
            PlatformMove();
        }
    }

    void PlatformMove()
    {
        rb.velocity = new Vector2(inputVec.x * moveSpeed, rb.velocity.y);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (bossStage)
        {
            if (collision.gameObject.CompareTag("Wall"))
            {
                canJmup = true;
            }
        }
    }

    IEnumerator Dash(Vector2 dashDir)
    {
        playerStat.StartCoroutine("DashInvincible");
        dashUI.UseDash();
        isDash = true;
        rb.velocity = dashDir * 3* moveSpeed;
        yield return new WaitForSeconds(0.2f);
        isDash = false;
        rb.velocity = Vector2.zero;
        dashAnim.SetTrigger("Dash");
        switch (playerDirection)
        {
            case PlayerDirection.Left:
                dashObject.transform.rotation = Quaternion.Euler(leftVec);
                break;
            case PlayerDirection.Right:
                dashObject.transform.rotation = Quaternion.Euler(rightVec);
                break;
            case PlayerDirection.Up:
                dashObject.transform.rotation = Quaternion.Euler(upVec);
                break;
            case PlayerDirection.Down:
                dashObject.transform.rotation = Quaternion.Euler(downVec);
                break;
        }
    }   

    public void Dead()
    {
        // 죽음 모션
        anim.SetTrigger("Dead");
        Invoke("GameOver", 1f);
    }

    void GameOver()
    {
        Time.timeScale = 0;
        GameManager.instance.GameOver();
    }

    public void OnOffDamaged(bool _isDamaged)
    {
        if (_isDamaged)
            spr.color = new Color(1, 1, 1, 0.4f);
        else
            spr.color = new Color(1, 1, 1, 1f);
    }

    public void CanDash(bool _canDash)
    {
        canDash = _canDash;
    }

    public void SetGravity()
    {
        rb.gravityScale = gravity;
    }
    void CheckMoveDir()
    {
        if (inputVec.x > 0 && inputVec.y==0)
        {
            playerDirection = PlayerDirection.Right;
        }
        else if (inputVec.x<0 && inputVec.y == 0)
        {
            playerDirection = PlayerDirection.Left;
        }
        else if (inputVec.y > 0)
        {
            playerDirection = PlayerDirection.Up;
        }
        else if(inputVec.y < 0) {
            playerDirection = PlayerDirection.Down;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.down * 0.5f);
    }
}
