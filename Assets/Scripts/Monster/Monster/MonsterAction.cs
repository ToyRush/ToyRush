using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface MonsterAction : MonsterMove, MonsterAttack, MonsterDamaged
{ 

}
public interface MonsterMove
{
    public abstract void Move();
}
public interface MonsterAttack
{
    public abstract void Attack();
}
public interface MonsterDamaged
{
    public abstract bool Damaged(int attack);
}
public interface MonsterDead
{
    public abstract void Dead(string eventname);
}