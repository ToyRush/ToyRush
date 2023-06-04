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

    public CapsuleCollider2D capsuleCollider2D;
    public Rigidbody2D rigid;

    public MonsterInfo monsterInfo;

    protected GameObject player;
    public List<Vector3> Position;

    protected  void Awake()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        animator = gameObject.GetComponent<Animator>();
        capsuleCollider2D = gameObject.GetComponent<CapsuleCollider2D>();
        rigid = gameObject.GetComponent <Rigidbody2D>();
        monsterInfo = new MonsterInfo();
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
        if (monsterInfo.state == MonsterState.Move || monsterInfo.state == MonsterState.Run  )
            Move();
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
        hitEffect.GetComponent<MonsterHit>().StopPartical();
        this.gameObject.SetActive(false);
    }
    public bool Event(string eventname)
    {
        if (eventname == "Stun")
        {
            StunObj.SetActive(true);
            StunObj.GetComponent<MonsterStun>().PlayPartical();
            StunObj.GetComponent<MonsterStun>().totalDuration = monsterInfo.delayTime;
            monsterInfo.state = MonsterState.Stun;
            monsterInfo.currentTime = 0;
        }
        if (eventname == "Slow")
        {
            monsterInfo.speedDecrease = 80;
        }
        return false;
    }


}
