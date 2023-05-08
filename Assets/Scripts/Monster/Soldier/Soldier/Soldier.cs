using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// 5/1 lee
public class Soldier : Monster
{
    // Start is called before the first frame update
    void Start()
    {
        move = gameObject.AddComponent<SoldierMove>();
        move.targetPos = MonsterManager.Instance.GetNextPos(this.gameObject);
        move.speed = 5.0f;

        monsterInfo.hp = 100.0f;
        monsterInfo.attack = 10.0f;
        monsterInfo.state = MonsterState.Stop;
        monsterInfo.findDis = 3.0f;
        monsterInfo.attackDis = 1.0f;
        monsterInfo.currentTime = 0.0f;
        monsterInfo.delayTime = 2.0f;
    }

    // Update is called once per frame
    protected void FixedUpdate()
    {
        if (monsterInfo.state == MonsterState.Stop)
            monsterInfo.currentTime += Time.fixedDeltaTime;
    }

    public override void BehaviorTree()
    {
        if (Vector3.Distance(this.rigid.position, player.transform.position) <= monsterInfo.findDis)
        {
            monsterInfo.state = MonsterState.Move;
            move.targetPos = player.transform.position;
            if (Vector3.Distance(this.rigid.position, player.transform.position) <= monsterInfo.attackDis)
            {
                monsterInfo.state = MonsterState.Attack;
            }
            animator.SetInteger("State", (int)monsterInfo.state);
        }
        else
        {
            if (monsterInfo.state == MonsterState.Move)
            {
                if (Vector3.Distance(this.transform.position, move.targetPos) <= 0.1f)
                {
                    rigid.velocity = Vector2.zero;
                    monsterInfo.state = MonsterState.Stop;
                    animator.SetInteger("State", (int)monsterInfo.state);
                }
            }
            else if (monsterInfo.currentTime > monsterInfo.delayTime)
            {
                monsterInfo.currentTime = 0;
                move.targetPos = MonsterManager.Instance.GetNextPos(this.gameObject);
                monsterInfo.state = MonsterState.Move;
                animator.SetInteger("State", (int)monsterInfo.state);

            }
        }
    }

    public override bool Damaged(float attack)
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
       
    }
}
