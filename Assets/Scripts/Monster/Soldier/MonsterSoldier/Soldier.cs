using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// 5/1 lee
public class Soldier : Monster
{
     public GameObject Bullet = null;
    // Start is called before the first frame update
    public void Start()
    {
        base.Start();
        Reset();
    }
    public void Reset()
    {
        monsterInfo.speed = 1.0f;
        if (rigid != null)
            monsterInfo.targetPos = rigid.position;
        monsterInfo.speedDecrease = 0;
        if (this.transform.GetChild(0) != null && weapon == null)
            weapon = this.transform.GetChild(0).gameObject;
        weapon.GetComponent<SpriteRenderer>().enabled = true;
        monsterInfo.hp = 4.0f;
        monsterInfo.attack = 1;
        monsterInfo.state = MonsterState.Stop;
        monsterInfo.findDis = 10.0f;
        monsterInfo.attackDis = 5.0f;
        monsterInfo.currentTime = 0.0f;
        monsterInfo.delayTime = 2.0f;
        monsterInfo.speedIncrease = 3.0f;
        monsterInfo.speed *= monsterInfo.speedIncrease;
        monsterInfo.index = 0;
        spriteRenderer.enabled = true;
        capsuleCollider2D.enabled = true;
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
        if (direction.x < 0)
        {
            spriteRenderer.flipX = false;
            weapon.GetComponent<SpriteRenderer>().flipX = true;
        }
        else
        {
            spriteRenderer.flipX = true;
            weapon.GetComponent<SpriteRenderer>().flipX = false;
        }
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
    private void OnDrawGizmos()
    {
        for(int i = 1; i < Position.Count; i++)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(
            Position[i -1 ], Position[i]);
        }
    }

    public override bool Damaged(int attack) // 붉은색 0.5초 맞았을 때
    {
        if (monsterInfo.state == MonsterState.Dead)
            return false;
        bhitted = true;
        monsterInfo.hp -= attack;
        if (monsterInfo.hp < 0.0f)
        {
            monsterInfo.state = MonsterState.Dead;
            hitEffect.GetComponent<MonsterHit>().PlayPartical();
            capsuleCollider2D.enabled = false;
            weapon.GetComponent<SpriteRenderer>().enabled = false;
            spriteRenderer.enabled = false;
            MonsterKeyManager.Instance.GetUnAtiveObject().transform.position = this.transform.position;
            Invoke("Dead", 1.0f);
        }
        return true;
    }

    public override void Attack()
    {
        if (monsterInfo.state != MonsterState.Attack || Bullet == null)
            return;

        GameObject bullet = MonsterBulletManager.Instance.GetUnAtiveObject();
        Vector3 direction = (monsterInfo.targetPos -rigid.transform.position).normalized;
        Vector3 pos = rigid.transform.position;
        pos.y -= 0.2f;
        if (direction.x < 0)
        {
            spriteRenderer.flipX = false;
            weapon.GetComponent<SpriteRenderer>().flipX = true;
            pos.x -= 0.8f;
        }
        else
        {
            spriteRenderer.flipX = true;
            weapon.GetComponent<SpriteRenderer>().flipX = false;
            pos.x += 0.8f;
        }
        bullet.SetActive(true);
        bullet.transform.position = pos;
        if (bullet.GetComponent<MonsterBullet>() == null)
            bullet.AddComponent<MonsterBullet>();
        bullet.GetComponent<MonsterBullet>().FireBullet(direction, monsterInfo.attack);
    }
}

/*
 *  player 말타는거 
 *  boos patern 
 *  일정 시간 마다 스폰
 * 
 * 
 */