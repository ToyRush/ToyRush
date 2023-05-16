using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MonsterState
{
    Stop = 1,
    Move = 2,
    Attack = 3,
    Dead = 4
};

[System.Serializable]
public struct MonsterInfo
{
    public float hp;
    public float attack;
    public float findDis;
    public float attackDis;
    public float currentTime;
    public float delayTime;
    public MonsterState state;

    // move
    public float speed;
    public float speedIncrease;
    public float speedDecrease;

    public Vector3 targetPos;
    public Vector3 direction;

    public bool bMoveable;

    public int index;
}

public abstract class Monster : MonoBehaviour , MonsterAction
{
    protected SpriteRenderer spriteRenderer;
    protected Animator animator;

    public BoxCollider2D boxCollider2D;
    public Rigidbody2D rigid;

    public MonsterInfo monsterInfo;

    protected GameObject player;
    public List<Vector2> Position;

    protected void Awake()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        animator = gameObject.GetComponent<Animator>();
        boxCollider2D = gameObject.GetComponent<BoxCollider2D>();
        rigid = gameObject.GetComponent <Rigidbody2D>();
        monsterInfo = new MonsterInfo();
        player = GameObject.FindWithTag("Player");
        monsterInfo.bMoveable = true;
        monsterInfo.speed = 1.0f;
        if (rigid != null)
            monsterInfo.targetPos = rigid.position;
    }
    protected void FixedUpdate()
    {
        if (monsterInfo.state == MonsterState.Stop)
            monsterInfo.currentTime += Time.fixedDeltaTime;
        if (monsterInfo.state == MonsterState.Move)
            Move();
    }

    protected void Update()
    {
        BehaviorTree();
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

    public MonsterState BehaviorTree()
    {
        if (monsterInfo.state == MonsterState.Dead)
            return monsterInfo.state;

        if (Vector3.Distance(this.rigid.position, player.transform.position) <= monsterInfo.findDis)
        {
            monsterInfo.state = MonsterState.Move;
            monsterInfo.targetPos = player.transform.position;
            if (Vector3.Distance(this.rigid.position, player.transform.position) <= monsterInfo.attackDis)
            {
                monsterInfo.state = MonsterState.Attack;
            }
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
                if (Vector3.Distance(this.transform.position, monsterInfo.targetPos) <= 0.1f)
                {
                    rigid.velocity = Vector2.zero;
                }
            }
            else if (monsterInfo.currentTime > monsterInfo.delayTime)
            {
                monsterInfo.currentTime = 0;
                // move.targetPos = MonsterManager.Instance.GetNextPos(this.gameObject);
                monsterInfo.state = MonsterState.Move;
            }
        }
        if (monsterInfo.bMoveable == false)
            monsterInfo.state = MonsterState.Stop;

        if (animator != null)
        animator.SetInteger("State", (int)monsterInfo.state);
        return monsterInfo.state;
    }
    public abstract bool Damaged(float attack);
    public abstract void Attack();
    public abstract void Move();
    public abstract void Dead();
    public abstract bool Event(string eventname);
}
