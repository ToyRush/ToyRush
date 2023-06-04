using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.Rendering.Universal;
// 스폰 함수 구현
// 기절 논의
//  

public class Patrol : Monster
{
    public float radius = 1.0f;
    public Vector3 initVec;
    float distance;
    public Vector3 direction;
    void Start()
    {
        Reset();
    }
    public void Reset()
    {
        base.Start();
        monsterInfo.speedIncrease = 2.5f;
        monsterInfo.speed *= monsterInfo.speedIncrease;
        distance = rigid.transform.localScale.x * 2;
        initVec = rigid.transform.position;
        monsterInfo.hp = 2.5f;
        monsterInfo.attack = 2;
        monsterInfo.state = MonsterState.Stop;
        monsterInfo.findDis = 4;
        monsterInfo.attackDis = 1.0f;
        monsterInfo.currentTime = 0.0f;
        monsterInfo.delayTime = 4.0f;

        gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().color = Color.red;
    }
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
        if (Vector3.Distance(this.rigid.position, player.transform.position) <= monsterInfo.findDis)
        {
            monsterInfo.state = MonsterState.Move;
            monsterInfo.targetPos = player.transform.position;
            monsterInfo.speedDecrease = -0.5f;
            if (Vector3.Distance(this.rigid.position, player.transform.position) <= monsterInfo.attackDis)
            {
                monsterInfo.state = MonsterState.Attack;
                Attack();
            }
        }
        else
        {
            if (monsterInfo.speedDecrease < 0)
            {
                monsterInfo.targetPos = this.rigid.position;
                monsterInfo.speedDecrease = 0.0f;
            }

            if (monsterInfo.state == MonsterState.Move)
            {
                if (Vector3.Distance(this.transform.position, monsterInfo.targetPos) <= 0.1f)
                {
                    rigid.velocity = Vector2.zero;
                    monsterInfo.state = MonsterState.Stop;
                    gameObject.transform.GetChild(0).GetComponent<Light2D>().pointLightOuterRadius = 4;
                    gameObject.transform.GetChild(0).GetComponent<CircleCollider2D>().radius = 1.3f;
                    gameObject.transform.GetChild(1).GetComponent<SpriteRenderer>().enabled = true;
                    monsterInfo.findDis = 4;
                }
            }
            else if (monsterInfo.currentTime > monsterInfo.delayTime)
            {
                monsterInfo.currentTime = 0;
                monsterInfo.state = MonsterState.Move;
                gameObject.transform.GetChild(0).GetComponent<Light2D>().pointLightOuterRadius = 2;
                gameObject.transform.GetChild(1).GetComponent<SpriteRenderer>().enabled = false;
                gameObject.transform.GetChild(0).GetComponent<CircleCollider2D>().radius = 0.65f;
                monsterInfo.findDis = 2;
            }
            else
                monsterInfo.currentTime += Time.fixedDeltaTime;
        }
        animator.SetInteger("State", (int)monsterInfo.state);
        return monsterInfo.state;
    }

    public override bool Damaged(int attack)
    {
        if (monsterInfo.state == MonsterState.Dead)
            return false;

        monsterInfo.hp -= attack;
        if (monsterInfo.hp < 0.0f)
        {
            monsterInfo.state = MonsterState.Dead;
            Dead();
        }
        return true;
    }

    public override void Attack()
    {
        return;
    }

    public override void Move()
    {
        if (monsterInfo.bMoveable == false)
            return;

        Vector3 currentV = rigid.position;
        direction = (monsterInfo.targetPos - currentV).normalized;
        Vector2 nextDir = currentV +
            (direction * Time.fixedDeltaTime * monsterInfo.speed * (1 - monsterInfo.speedDecrease / 100));
        if (direction.x < 0)
            spriteRenderer.flipX = false;
        else
            spriteRenderer.flipX = true;
        if (Vector3.Distance(currentV, monsterInfo.targetPos) <= 0.1f)
        {
            float angle = Random.Range(0, 360);
            monsterInfo.targetPos = new Vector3(Mathf.Cos(Mathf.Deg2Rad* angle) * distance + rigid.position.x, Mathf.Sin(Mathf.Deg2Rad * angle) * distance + rigid.position.y, 0);
        }

        rigid.MovePosition(nextDir);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Wall")
        {
            Vector3 currentV = rigid.position;
            Vector2 nextDir = currentV +
                (direction * -1 * Time.fixedDeltaTime * monsterInfo.speed * (1 - monsterInfo.speedDecrease / 100));
            monsterInfo.targetPos = nextDir;
        }

        if (collision.gameObject.tag == "Player")
        {
            Damaged(1000);
            collision.gameObject.GetComponent<PlayerStat>().Damaged(monsterInfo.attack);
        }
    }
}
