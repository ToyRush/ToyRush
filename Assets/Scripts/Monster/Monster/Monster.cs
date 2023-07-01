using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MonsterState
{
    Stop = 1,
    Move = 2,
    Attack = 3,
    Dead = 4,
    Run = 5,
    warning = 6,
    Stun = 7,
};

[System.Serializable]
public struct MonsterInfo
{
    public float hp;
    public int attack;
    public float findDis;
    public float attackDis;
    public float currentTime;
    public float delayTime;
    public MonsterState state;

    // move
    public float speed;
    public float speedIncrease;
    public float speedDecrease; // 0~ 100

    public Vector3 targetPos;
    public Vector3 direction;

    public bool bMoveable;

    public int index;
}

public abstract class Monster : MonoBehaviour , MonsterAction
{
    protected SpriteRenderer spriteRenderer;
    protected Animator animator;
    protected GameObject weapon;
    public GameObject StunObj;
    public GameObject hitEffect;
    public GameObject slowEffect;

    public CapsuleCollider2D capsuleCollider2D;
    public Rigidbody2D rigid;

    public MonsterInfo monsterInfo;

    protected GameObject player;
    public List<Vector3> Position;

    public float currentTime;
    public int hitcount;
    public bool bhitted;
    public float hittedTime;
    Color pre;

    protected  void Awake()
    {
        bhitted = false;
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        animator = gameObject.GetComponent<Animator>();
        capsuleCollider2D = gameObject.GetComponent<CapsuleCollider2D>();
        rigid = gameObject.GetComponent <Rigidbody2D>();
        hittedTime = 0.5f;
        monsterInfo = new MonsterInfo();
        currentTime = 0;
        pre = spriteRenderer.color;
        hitcount = 0;
    }

    protected  void Start()
    {
        player = GameObject.FindWithTag("Player");
        monsterInfo.bMoveable = true;
        monsterInfo.speed = 1.0f;
        if (rigid != null)
            monsterInfo.targetPos = rigid.position;
        monsterInfo.speedDecrease = 0;
    }
    protected void FixedUpdate()
    {
        BehaviorTree();
        if (monsterInfo.state == MonsterState.Move || monsterInfo.state == MonsterState.Run)
            Move();
        if (bhitted == true)
        {
            currentTime += Time.fixedDeltaTime;
            if (currentTime < hittedTime)
            {
            hitcount++;
                if (hitcount >= 3)
                {
                    hitcount = 0;
                    if (spriteRenderer.color == Color.red)
                        spriteRenderer.color = pre;
                    else
                        spriteRenderer.color = Color.red;
                }
            }
            else
            {
                bhitted = false;
                currentTime = 0;
                hitcount = 0;
                spriteRenderer.color = pre;
            }
        }
    }

    protected void Update()
    {
        rigid.velocity = Vector3.zero;
        rigid.angularVelocity = 0;
    }
    public void SetState(MonsterState state)
    {
        monsterInfo.state = state;
        animator.SetInteger("State", (int)state);
    }
    public void  SetMonsterInfo(ref MonsterInfo info) 
    {
        monsterInfo = info; 
    }

    public virtual MonsterState BehaviorTree()
    {
        if (monsterInfo.state == MonsterState.Dead)
            return monsterInfo.state;
        if (monsterInfo.state == MonsterState.Stun)
        {
            monsterInfo.currentTime += Time.deltaTime;
            if (monsterInfo.currentTime >= monsterInfo.delayTime)
            {
                monsterInfo.currentTime = 0;
                monsterInfo.state = MonsterState.Stop;
            }
            else
                return monsterInfo.state;
        }
        if (Vector3.Distance(rigid.position, player.transform.position) <= monsterInfo.findDis)
        {
            monsterInfo.state = MonsterState.Move;
            monsterInfo.targetPos = player.transform.position;
            if (Vector3.Distance(rigid.position, player.transform.position) <= monsterInfo.attackDis)
                monsterInfo.state = MonsterState.Attack;
        }
        else
        {
            if (monsterInfo.state == MonsterState.Attack)
            {
                monsterInfo.targetPos = rigid.position;
                monsterInfo.state = MonsterState.Stop;
            }

            if (monsterInfo.state == MonsterState.Move)
            {
                if (Vector3.Distance(transform.position, monsterInfo.targetPos) <= 0.1f)
                {
                    rigid.velocity = Vector2.zero;
                    monsterInfo.currentTime = 0;
                    monsterInfo.state = MonsterState.Stop;
                }
            }
        }
        if (monsterInfo.state == MonsterState.Stop)
        {
            if (monsterInfo.currentTime >= monsterInfo.delayTime)
            {
                monsterInfo.currentTime = 0;
                monsterInfo.state = MonsterState.Move;
            }
            else
                monsterInfo.currentTime += Time.deltaTime;
        }
        if (monsterInfo.bMoveable == false)
            monsterInfo.state = MonsterState.Stop;

       
        if (animator != null)
        animator.SetInteger("State", (int)monsterInfo.state);
        return monsterInfo.state;
    }
    public abstract bool Damaged(int attack);
    public abstract void Attack();
    public abstract void Move();
    public virtual void Dead()
    {
        if (Position.Count > 0)
            monsterInfo.targetPos =Position[0];
        bhitted = false;
        spriteRenderer.color = pre;

        if (StunObj != null)
        {
            StunObj.GetComponent<MonsterStun>().StopPartical();
            StunObj.SetActive(false);
        }

        if (slowEffect != null)
        {
            slowEffect.GetComponent<MonsterSlow>().StopPartical();
            slowEffect.SetActive(false);
        }

        monsterInfo.currentTime = 0;

        if (hitEffect != null && hitEffect.GetComponent<MonsterHit>() != null)
            hitEffect.GetComponent<MonsterHit>().StopPartical();
        this.gameObject.SetActive(false);
    }
    public bool Event(string eventname)
    {
        if (monsterInfo.state ==  MonsterState.Dead)
            return false;
        if (eventname == "Stun")
        {
            if (StunObj == null)
                return false;
            StunObj.SetActive(true);
            StunObj.GetComponent<MonsterStun>().PlayPartical();
            StunObj.GetComponent<MonsterStun>().totalDuration = monsterInfo.delayTime;
            monsterInfo.state = MonsterState.Stun;
            monsterInfo.currentTime = 0;
        }
        else if (eventname == "Slow")
        {
            if (slowEffect == null)
                return false;
            monsterInfo.speedDecrease = 80;
            slowEffect.SetActive(true);
            slowEffect.GetComponent<MonsterSlow>().PlayPartical();
            slowEffect.GetComponent<MonsterSlow>().totalDuration = monsterInfo.delayTime;
            monsterInfo.state = MonsterState.Stop;
            monsterInfo.currentTime = 0;
        }
        return true;
    }
}
