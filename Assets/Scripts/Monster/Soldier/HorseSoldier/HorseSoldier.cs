using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorseSoldier : Monster
{
    static public int itemcount = 10;
    public Monster soldier;
    protected Monster horse;
    void Start()
    {
        soldier = gameObject.GetComponentInChildren<Soldier>();
        horse = gameObject.GetComponentInChildren<Horse>();

        monsterInfo.speedIncrease = 5.0f;
        monsterInfo.speed *= monsterInfo.speedIncrease;
        monsterInfo.hp = 100.0f;
        monsterInfo.attack = 10.0f;
        monsterInfo.state = MonsterState.Stop;
        monsterInfo.findDis = 10.0f;
        monsterInfo.attackDis = 10.0f;
        monsterInfo.currentTime = 0.0f;
        monsterInfo.delayTime = 2.0f;
        soldier.Position = this.Position;
        horse.Position = this.Position;
        soldier.rigid = this.rigid;
        horse.rigid = this.rigid;
        soldier.SetMonsterInfo(ref monsterInfo);
        horse.SetMonsterInfo(ref monsterInfo);
    }

    protected new void  FixedUpdate()
    {
        if (monsterInfo.state == MonsterState.Stop)
            monsterInfo.currentTime += Time.fixedDeltaTime;
    }

    public new MonsterState BehaviorTree()
    {
        base.BehaviorTree();

        if (soldier.monsterInfo.state != MonsterState.Dead)
        {
            MonsterState state =    soldier.BehaviorTree();
            monsterInfo.state = state;
            horse.SetState(monsterInfo.state);
            return state;
        }
        else if (horse.monsterInfo.state != MonsterState.Dead)
            return horse.BehaviorTree();

        return MonsterState.Dead;
    }
    public new void Update()
    {
        base.Update();
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            ChangeMonster();
        }
    }

    public override bool Damaged(float attack)
    {
        if (monsterInfo.hp >= 0.01f)
        {
            monsterInfo.hp -= attack;
            attack = 0;
            if (monsterInfo.hp < 0)
                attack = monsterInfo.hp * -1;
        }

        soldier.Damaged(attack);
        if (soldier.monsterInfo.state != MonsterState.Dead)
            return true;
        else
            ChangeMonster();

        if (horse.monsterInfo.state == MonsterState.Dead)
            return false;

        horse.Damaged(attack);
        return true;
    }

    public override void Attack()
    {
        if (soldier.monsterInfo.state != MonsterState.Dead)
            soldier.Attack();
    }

    public void ChangeMonster()
    {
        soldier.gameObject.transform.SetParent(null);
        soldier.Damaged(1000);

        ((Horse)horse).SetDead();

        //Destroy(this.gameObject, 1.5f);
    }

    public override void Move()
    {
        if (soldier.monsterInfo.state != MonsterState.Dead)
            soldier.Move();

        else if (horse.monsterInfo.state != MonsterState.Dead)
            horse.Move();
    }

    public override bool Event(string eventname)
    {
        throw new System.NotImplementedException();
    }
    public override void Dead()
    {

    }
}
