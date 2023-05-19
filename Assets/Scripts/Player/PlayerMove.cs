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

public enum Direction // ���콺 ����
{
    Left,
    Right,
    Middle
}

public enum PlayerDirection // ����Ű�� �����̴� ����
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
    Dead=2,
}

public class PlayerMove : MonoBehaviour
{
    // ������Ʈ
    Animator anim;
    Animator dashAnim;
    Rigidbody2D rb;
    SpriteRenderer spr;

    Vector2 inputVec; // �÷��̾��� �̵� ����

    [Header("������ ����")]
    WaitForSeconds shootDelay = new WaitForSeconds(0.2f);
    RushBearAnimation rushBearAnimation;
    PlayerDirection playerDirection;

    [Header("��ȹ�� ���� : �ӵ� ���� ����")]
    public float moveSpeed; // �÷��̾��� �̵� �ӵ�
    bool canMove = true; // �÷��̾��� ������ ����
    bool isMove=false;
    bool isDash = false; // ������ ����
    [SerializeField] GameObject dashObject;
    [SerializeField] ParticleSystem moveParticle;
    [SerializeField] ParticleSystem stopParticle;

    Vector3 leftVec = new Vector3(0, 0, 90);
    Vector3 rightVec = new Vector3(0, 0, 270);
    Vector3 downVec= new Vector3(0, 0, 180);
    Vector3 upVec = new Vector3(0, 0, 0);

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }
    void Start()
    {
        isDash = false;
        rushBearAnimation = RushBearAnimation.Idle;
        dashAnim = dashObject.GetComponent<Animator>();
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
        CheckMoveDir();

        if (Input.GetMouseButtonDown(1)&&!isDash)
            StartCoroutine("Dash", inputVec.normalized);
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
            if (!isDash)
            {
                Vector2 nextVec = inputVec.normalized * moveSpeed * Time.fixedDeltaTime;
                rb.MovePosition(rb.position + nextVec);
            }
        }
    }


    IEnumerator Dash(Vector2 dashDir)
    {
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
        // ���� ���
    }

    public void OnOffDamaged(bool _isDamaged)
    {
        if (_isDamaged)
            spr.color = new Color(1, 1, 1, 0.4f);
        else
            spr.color = new Color(1, 1, 1, 1f);
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
}
