using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Horse : Monster
{
    static public float liveTime = 1.0f;
    private float DeadTime;
    public bool isDead;
    float playerSpeed;
    public bool isGet;
    static 
    int playerHp;
    Vector3 left = new Vector3(-0.0390000008f, -0.323000014f, 0); //왼쪽 방향
    Vector3 right = new Vector3(0.0700000003f,-0.323000014f,0); //오른 방향
    Vector3 direct;
    private void Start()
    {
        base.Start();
        Reset();
    }

    public void Reset()
    {
        playerSpeed = 0;
        DeadTime = 0.0f;
        isGet = false;
        isDead = true;
        monsterInfo.speedIncrease = 1.0f;
        monsterInfo.speed = 10.0f;
        playerHp = 0;
    }
    public override void Attack()
    {
        return;
    }

    public new MonsterState BehaviorTree()
    {
        return monsterInfo.state;
    }

    public override bool Damaged(int attack)
    {
        return false;
    }

    private new void FixedUpdate()
    {
        if (isDead)
        {
            DeadTime += Time.fixedDeltaTime;

            if (DeadTime >= liveTime)
            {
                Dead();
            }
        }
    }
    private new void Update()
    {
        if (isGet == false)
            return;

        if (playerHp > player.GetComponent<PlayerStat>().currentHealth)
        {
            player.GetComponent<PlayerMove>().moveSpeed = playerSpeed;
            HorseManager.Instance.hasPlayer = false;
            player.gameObject.GetComponent<PlayerMove>().canDash = true;
            Dead();
            return;
        }
        else if (playerHp < player.GetComponent<PlayerStat>().currentHealth)
            playerHp = player.GetComponent<PlayerStat>().currentHealth;
        Vector2 inputVec = new Vector2();
        inputVec.x = Input.GetAxisRaw("Horizontal");
        this.transform.localPosition = Vector3.zero;
        if (inputVec.x > 0)
        {
            spriteRenderer.flipX = true;
            direct = right;// 오른 방향
        }
        else if (inputVec.x < 0)
        {
            spriteRenderer.flipX = false;
            direct = left;
        }
        player.GetComponent<PlayerMove>().moveSpeed = monsterInfo.speed;
        this.transform.localPosition += direct;
    }

    public override void Move()
    {
    }

    public override void Dead()
    {
        this.transform.SetParent(HorseManager.Instance.gameObject.transform);
        Reset();
        this.gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && isGet == false && HorseManager.Instance.hasPlayer == false)
        {
            HorseManager.Instance.hasPlayer = true;
            playerSpeed = player.GetComponent<PlayerMove>().moveSpeed;
           // this.transform.SetParent(null);
            this.transform.SetParent(collision.gameObject.transform);
            collision.gameObject.GetComponent<PlayerMove>().canDash = false;
            this.transform.position = Vector3.zero;
            isGet = true;
            isDead = false;
            playerHp = player.GetComponent<PlayerStat>().currentHealth;
        }

        
    }
}
