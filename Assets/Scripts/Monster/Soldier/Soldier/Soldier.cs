using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// 5/1 lee
public class Soldier : Monster
{
    static public int itemcount = 13;
    // Start is called before the first frame update
    void Start()
    {
        //move.targetPos = MonsterManager.Instance.GetNextPos(this.gameObject);
        monsterInfo.hp = 100.0f;
        monsterInfo.attack = 10.0f;
        monsterInfo.state = MonsterState.Stop;
        monsterInfo.findDis = 10.0f;
        monsterInfo.attackDis = 10.0f;
        monsterInfo.currentTime = 0.0f;
        monsterInfo.delayTime = 2.0f;
        monsterInfo.speedIncrease = 3.0f;
        monsterInfo.speed *= monsterInfo.speedIncrease;
    }

    // Update is called once per frame

    public new MonsterState BehaviorTree()
    {
         return base.BehaviorTree();
    }
    public override void Move()
    {
        if (monsterInfo.bMoveable == false)
            return;
        Vector3 currentV = rigid.position;
        Vector3 direction = (monsterInfo.targetPos - currentV).normalized;
        Vector2 nextDir = currentV +
            (direction * Time.fixedDeltaTime * monsterInfo.speed * (1 - monsterInfo.speedDecrease / 100));

        if (Vector3.Distance(currentV, monsterInfo.targetPos) <= 0.1f)
        {
            monsterInfo.index++;
            if ( Position.Count <= monsterInfo.index)
                monsterInfo.index = 0;
            monsterInfo.targetPos = Position[monsterInfo.index];
        }
        rigid.MovePosition(nextDir);
    }

    public override bool Damaged(float attack)
    {
        if (monsterInfo.state == MonsterState.Dead)
            return false;

        monsterInfo.hp -= attack;
        if (monsterInfo.hp < 0.0f)
        {
            monsterInfo.state = MonsterState.Dead;
            Destroy(this.gameObject, 1.5f);
        }
        return true;
    }
    public override void Attack()
    {

    }

    public override bool Event(string eventname)
    {
        throw new System.NotImplementedException();
    }
    public override void Dead()
    {

    }
}