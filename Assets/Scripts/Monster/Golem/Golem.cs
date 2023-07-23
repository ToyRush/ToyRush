using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Golem : Monster
{
    static public int itemcount = 13;
    public GameObject SplashObj = null;

    public bool bAttack;
    public bool bAttacking;
    public float attackTime;
    public float attackDelay;
    // Start is called before the first frame update
    protected void Awake()
    {
        base.Awake();
        SplashObj = transform.GetChild(0).gameObject;
    }
    public void Start()
    {
        base.Start();
        Reset();
    }
    public void Reset()
    {
        if (StunObj != null)
            StunObj.SetActive(false);

        if (slowEffect != null)
            slowEffect.SetActive(false);
        monsterInfo.speed = 1.0f;
        if (rigid != null)
            monsterInfo.targetPos = rigid.position;
        monsterInfo.speedDecrease = 0;
        bAttack = false;
        bAttacking = false;
        attackTime = 0;
        //move.targetPos = MonsterManager.Instance.GetNextPos(this.gameObject);
        monsterInfo.hp = 6.0f;
        monsterInfo.attack = 2;
        monsterInfo.state = MonsterState.Stop;
        monsterInfo.findDis = 5.0f;
        monsterInfo.attackDis = 3.0f;
        monsterInfo.currentTime = 0.0f;
        monsterInfo.delayTime = 2.0f;
        monsterInfo.speedIncrease = 3.0f;
        monsterInfo.speed *= monsterInfo.speedIncrease;
        monsterInfo.index = 0;

        spriteRenderer.enabled = true;
        capsuleCollider2D.enabled = true;
        SplashObj.GetComponentInChildren<SplashAnimator>().attack = monsterInfo.attack;
    }
    // Update is called once per frame

    public override MonsterState BehaviorTree()
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
            monsterInfo.state = MonsterState.Run;
            monsterInfo.targetPos = player.transform.position;
            if (Vector3.Distance(rigid.position, player.transform.position) <= monsterInfo.attackDis )
            {
                monsterInfo.state = MonsterState.Attack;
                attackTime += Time.deltaTime;
                if (attackTime > attackDelay)
                {
                    attackTime = 0;
                    bAttack = false;
                    bAttacking = false;
                }
                if (bAttack == false && bAttacking == false)
                {
                    bAttacking = true;
                    bAttack = true;
                   
                }
                else if (bAttack == true && bAttacking == false)
                {
                    monsterInfo.state = MonsterState.warning;
                }
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
                // move.targetPos = MonsterManager.Instance.GetNextPos(this.gameObject);
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
    public override void Move()
    {
        if (monsterInfo.bMoveable == false || SplashObj.GetComponent<GolemSplash>().bPlay == true)
            return;
        Vector3 currentV = rigid.position;
        Vector3 direction = (monsterInfo.targetPos - currentV).normalized;
        if (direction.x < 0)
            spriteRenderer.flipX = false;
        else
            spriteRenderer.flipX = true;
        Vector2 nextDir = currentV +
        (direction * Time.fixedDeltaTime * monsterInfo.speed * (1 - monsterInfo.speedDecrease / 100));

        if (Vector3.Distance(currentV, monsterInfo.targetPos) <= 0.1f)
        {
            monsterInfo.index++;
            if (Position.Count <= monsterInfo.index)
                monsterInfo.index = 0;
            monsterInfo.targetPos = Position[monsterInfo.index];
        }
        rigid.MovePosition(nextDir);
    }

    public override bool Damaged(int attack)
    {
        if (monsterInfo.state == MonsterState.Dead)
            return false;
        bhitted = true;
        monsterInfo.hp -= attack;
        if (monsterInfo.hp < 0.0f)
        {
            monsterInfo.state = MonsterState.Dead;
            hitEffect.GetComponent<MonsterHit>().PlayPartical();
            spriteRenderer.enabled = false;

            capsuleCollider2D.enabled = false;
            spriteRenderer.enabled = false;
            MonsterKeyManager.Instance.GetUnAtiveObject().transform.position = this.transform.position;
            Invoke("Dead", 1.5f);
        }
        return true;
    }
    public override void Attack()
    {
        if (monsterInfo.state != MonsterState.Attack)
            return;
    }
    public void Splash()
    {
        Vector3 direction = (monsterInfo.targetPos - rigid.transform.position).normalized;
        Vector3 pos = SplashObj.transform.localPosition;
        if (direction.x < 0)
        {
            spriteRenderer.flipX = false;
            pos.x = -2f;
        }
        else
        {
            spriteRenderer.flipX = true;
            pos.x = 2f;
        }
        SplashObj.transform.localPosition = pos;
        SplashObj.GetComponent<GolemSplash>().bPlay = true;

        for (int i = 0; i < 2; i++)
        {
            GameObject metor = MeteorManager.Instance.GetUnAtiveObject();
            {
                metor.SetActive(true);
                metor.GetComponent<Meteor>().attack = monsterInfo.attack;
                metor.GetComponent<Meteor>().bPlay = true;
                metor.transform.position = this.transform.position + new Vector3(Random.Range(-3,3), Random.Range(-3, 3), 0);
            }
        }
    }
    private void OnDrawGizmos()
    {
        for (int i = 1; i < Position.Count; i++)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(
            Position[i - 1], Position[i]);
        }
    }
    public void AttackEnd()
    {
        bAttacking = false;
    }
}