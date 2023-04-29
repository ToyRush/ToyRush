using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Bullets;

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
    Right
}



public class PlayerMove : MonoBehaviour
{
    Vector2 inputVec;
    Vector3 mousePos;

    bool canMove = true;

    Animator anim;
    Rigidbody2D rb;
    SpriteRenderer spr;

    [Header("개발자 변수")]
    public GameObject weaponRight;
    public GameObject weaponLeft;
    public Transform[] shootPos;

    [Header("기획자 변수 : 속도 조절 변수")]
    public float moveSpeed;

    WaitForSeconds shootDelay = new WaitForSeconds(0.2f);
    public int bulletCnt;
    public int bulletMax;
    private bool canShoot=true;

    bool isRolling=false;
    public int bulletNum = 0;
    Direction direction;
    AnimationState animationState;
    public Text currentText;
    public Text maxText;

    void Awake()    
    {
        rb = GetComponent<Rigidbody2D>();
        spr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        bulletCnt = bulletMax;
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
        ShowBulletText();
        MouseMove();
        WeaponRotate();
        ShootBullet();


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
                //rb.velocity = inputVec * speed;
                Vector2 nextVec = inputVec.normalized * moveSpeed * Time.fixedDeltaTime;
                rb.MovePosition(rb.position + nextVec);
            }
        }
    }
    IEnumerator ShootDelay()
    {
        canShoot = false;
        yield return shootDelay;
        canShoot = true;
    }

    public void ChargeBullet()
    {
        bulletCnt = bulletMax;
        canShoot = true;
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
        rb.velocity = rollDir *3* moveSpeed;
        yield return new WaitForSeconds(0.2f);
        isRolling = false;
        rb.velocity = Vector2.zero;
    }   

    void ShowBulletText()
    {
        if (bulletNum == 0)
        {
            currentText.text = "∞";
            maxText.text = "∞";
        }
        else
        {
            currentText.text = bulletCnt.ToString();
            maxText.text = bulletMax.ToString();
        }
        if (bulletCnt > bulletMax)
            bulletCnt = bulletMax;
    }

    void WeaponRotate()
    {
        Vector3 playerPosition = Camera.main.WorldToScreenPoint(transform.position);
        Vector3 mousePosition = mousePos;
        mousePos -= playerPosition;
        float angle = Mathf.Atan2(mousePosition.y, mousePosition.x) * Mathf.Rad2Deg;
        weaponRight.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        weaponLeft.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        if (angle < 90 && angle > -90)
        {
            weaponRight.SetActive(true);
            weaponLeft.SetActive(false);
            direction = Direction.Right;
        }
        else
        {
            weaponRight.SetActive(false);
            weaponLeft.SetActive(true);
            direction = Direction.Left;
        }
    }
    void ShootBullet()
    {
        if (Input.GetMouseButtonDown(0) && canShoot)
        {
            if (bulletCnt <= 0)
            {
                if (bulletNum != 0)
                {
                    bulletNum = 0;
                    canShoot = false;
                    Invoke("ChargeBullet", 2f);
                }
                else
                {
                    ChargeBullet();
                }
            }
            else
            {
                bulletCnt -= 1;
                StartCoroutine("ShootDelay");
            }

            Vector3 dir = mousePos - transform.position;
            Transform bullet = GameManager.instance.poolManager.Get(bulletNum).transform;
            switch (direction)
            {
                case Direction.Left:
                    bullet.position = shootPos[0].position;
                    break;
                case Direction.Right:
                    bullet.position = shootPos[1].position;
                    break;
            }
            dir.z = 0f;
            dir = dir.normalized;
            bullet.GetComponent<Bullet>().Init(dir);

        }
    }
}
