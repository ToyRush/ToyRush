using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface MonsterAction : MonsterMove, MonsterAttack, MonsterDamaged, MonsterEvent
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
    public abstract bool Damaged(float attack);
}
public interface MonsterEvent
{
    public abstract bool Event(string eventname);
}
public interface MonsterDead
{
    public abstract void Dead(string eventname);
}