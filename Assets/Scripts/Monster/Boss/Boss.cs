using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Monster
{
    public int AttackCase;
    public GameObject Lazer;

    bool bAttack;
    void Start()
    {
        monsterInfo.hp = 100.0f;
        monsterInfo.attack = 10;
        monsterInfo.state = MonsterState.Stop;
        monsterInfo.findDis = 100.0f;
        monsterInfo.attackDis = 100.0f;
        monsterInfo.currentTime = 0.0f;
        monsterInfo.delayTime = 5.0f;
       // Lazer = this.transform.GetComponentInChildren<GameObject>();
        AttackCase = -1;
        bAttack = false;
    }

    private new void FixedUpdate()
    {
        if (bAttack == false)
            monsterInfo.currentTime += Time.fixedDeltaTime;
        BehaviorTree();
    }
    // Update is called once per frame
    new void Update()
    {
        
    }

    public new MonsterState BehaviorTree()
    {
        if (monsterInfo.currentTime < monsterInfo.delayTime)
            return monsterInfo.state;

        AttackCase++;
        if (AttackCase > 3)
            AttackCase = 0;
        monsterInfo.state = MonsterState.Attack;
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
            Destroy(this.gameObject, 1.5f);
        }
        return true;
    }

    public override void Attack()
    {
        throw new System.NotImplementedException();
    }

    public override void Move()
    {
        throw new System.NotImplementedException();
    }

    public override bool Event(string eventname)
    {
        throw new System.NotImplementedException();
    }
    public override void Dead()
    {
        Destroy(this.gameObject, 2.0f);
    }
}
