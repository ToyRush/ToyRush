using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

// 스폰 함수 구현
// 기절 논의
//  

public class Patrol : Monster
{
    public float radius = 1.0f;
    public Vector3 initVec;
    float distance;
    void Start()
    {
        monsterInfo.speedIncrease = 2.5f;
        monsterInfo.speed *= monsterInfo.speedIncrease;
        distance = rigid.transform.localScale.x * 2;
        initVec = rigid.transform.position;
        monsterInfo.hp = 100.0f;
        monsterInfo.attack = 10;
        monsterInfo.state = MonsterState.Stop;
        monsterInfo.findDis = 10;
        monsterInfo.attackDis = 1.0f;
        monsterInfo.currentTime = 0.0f;
        monsterInfo.delayTime = 2.0f;

        gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().color = Color.red;
    }

    public new MonsterState BehaviorTree()
    {
        if (monsterInfo.state == MonsterState.Dead)
            return monsterInfo.state;

        if (Vector3.Distance(this.rigid.position, player.transform.position) <= monsterInfo.findDis)
        {
            monsterInfo.state = MonsterState.Move;
            monsterInfo.targetPos = player.transform.position;
            monsterInfo.speedDecrease = -10.0f;
            if (Vector3.Distance(this.rigid.position, player.transform.position) <= monsterInfo.attackDis)
            {
                monsterInfo.state = MonsterState.Attack;
            }
            animator.SetInteger("State", (int)monsterInfo.state);
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
                    animator.SetInteger("State", (int)monsterInfo.state);
                }
            }
            else if (monsterInfo.currentTime > monsterInfo.delayTime)
            {
                monsterInfo.currentTime = 0;
                monsterInfo.state = MonsterState.Move;
                animator.SetInteger("State", (int)monsterInfo.state);

            }
        }
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
        }
        return true;
    }

    public override void Attack()
    {
        //if (player.GetComponent<MonsterPlayer>() != null)
        //    player.GetComponent<MonsterPlayer>().Damaged(monsterInfo.attack);
        //monsterInfo.state = MonsterState.Dead;
        //animator.SetInteger("State", (int)monsterInfo.state);
    }

    public override void Move()
    {
        if (monsterInfo.bMoveable == false)
            return;

        Vector3 currentV = rigid.position;
        Vector3 direction = (monsterInfo.targetPos - currentV).normalized;
        Vector2 nextDir = currentV +
            (direction * Time.fixedDeltaTime * monsterInfo.speed * (1 - monsterInfo.speedDecrease));

        if (Vector3.Distance(currentV, monsterInfo.targetPos) <= 0.1f)
        {
            float angle = Random.Range(0, 360);
            monsterInfo.targetPos = new Vector3(Mathf.Cos(Mathf.Deg2Rad* angle) * distance + rigid.position.x, Mathf.Sin(Mathf.Deg2Rad * angle) * distance + rigid.position.y, 0);
        }

        rigid.MovePosition(nextDir);
    }

    public override bool Event(string eventname)
    {
        throw new System.NotImplementedException();
    }

    public float angleRange = 30f;

    //private void OnDrawGizmos() // 게임 화면에서도 그려지도록..
    //{
    //    Handles.color = new Color(1f, 0f, 0f, 0.2f);
    //    // DrawSolidArc(시작점, 노멀벡터(법선벡터), 그려줄 방향 벡터, 각도, 반지름)
    //    if (rigid != null)
    //    {
    //        Handles.DrawSolidArc(rigid.position, -Vector3.back, new Vector3(1, 0, 0), angleRange / 2, radius);
    //        Handles.DrawSolidArc(rigid.position, -Vector3.back, new Vector3(1, 0, 0), -angleRange / 2, radius);
    //    }
    //}
    //private void OnTriggerEnter2D(Collider2D collision)
    //{

    //}
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Wall")
        {
            monsterInfo.targetPos = initVec;
        }
    }
    public override void Dead()
    {
        Destroy(this.gameObject, 2.0f);
    }
}
