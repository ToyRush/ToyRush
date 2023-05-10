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

public struct MonsterInfo
{
    public float hp;
    public float attack;
    public float findDis;
    public float attackDis;
    public float currentTime;
    public float delayTime;
    public MonsterState state;
}

public abstract class Monster : MonoBehaviour
{
    protected SpriteRenderer spriteRenderer;
    protected Animator animator;
    protected BoxCollider2D boxCollider2D;
    protected Rigidbody2D rigid;

    protected MonsterInfo monsterInfo;
    protected MonsterMove move;

    protected GameObject player;

    protected void Awake()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        animator = gameObject.GetComponent<Animator>();
        boxCollider2D = gameObject.GetComponent<BoxCollider2D>();
        rigid = gameObject.GetComponent <Rigidbody2D>();
        monsterInfo = new MonsterInfo();
        player = GameObject.FindWithTag("Player");
    }

    protected void Update()
    {
        BehaviorTree();
        if (monsterInfo.state == MonsterState.Move && move != null)
            move.Move();
    }

    public abstract void BehaviorTree();
    public abstract bool Damaged(float attack);
    public abstract void Attack();
}
