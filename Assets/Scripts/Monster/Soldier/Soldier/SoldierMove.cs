using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 5/1 lee
public class SoldierMove : MonsterMove
{
    public override void Move()
    {
        Vector3 currentV = rigid.position;
        Vector3 direction = (targetPos - currentV).normalized;
        Vector2 nextDir = currentV + (direction * speed * Time.deltaTime);
        rigid.MovePosition(nextDir);
    }
}
