using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Monster
{
    void Start()
    {
        monsterInfo.hp = 100.0f;
        monsterInfo.attack = 10.0f;
        monsterInfo.state = MonsterState.Stop;
        monsterInfo.findDis = 3.0f;
        monsterInfo.attackDis = 1.0f;
        monsterInfo.currentTime = 0.0f;
        monsterInfo.delayTime = 2.0f;
    }

    // Update is called once per frame
    new void Update()
    {
        
    }

    public override void BehaviorTree()
    {
        throw new System.NotImplementedException();
    }

    public override bool Damaged(float attack)
    {
        throw new System.NotImplementedException();
    }

    public override void Attack()
    {
        throw new System.NotImplementedException();
    }
}
