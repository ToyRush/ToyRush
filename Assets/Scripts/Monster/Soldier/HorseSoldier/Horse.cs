using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Horse : Monster
{
    static public float liveTime = 2.0f;
    private float DeadTime;
    private bool isDead;

    private void Start()
    {
        DeadTime = 0.0f;
        isDead = false;
        monsterInfo.speedIncrease = 15.0f;
        monsterInfo.speed *= monsterInfo.speedIncrease;

    }
    public override void Attack()
    {
       
    }

    public new MonsterState BehaviorTree()
    {
        base.BehaviorTree();
        if (DeadTime - liveTime >= 0.1f)
        {
            Destroy(this.gameObject, .2f);
        }
        return monsterInfo.state;
    }

    public override bool Damaged(float attack)
    {
        return false;
    }

    private new void FixedUpdate()
    {
        base.FixedUpdate();
        if (isDead)
            DeadTime += Time.fixedDeltaTime;
    }
    public void SetDead()
    {
        isDead = true;
        //gameObject.AddComponent<Rigidbody2D>();
        //gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        //gameObject.GetComponent<Rigidbody2D>().gravityScale = 0;
        //gameObject.AddComponent<BoxCollider2D>();
        //gameObject.GetComponent<BoxCollider2D>().enabled = false;
        //gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
    }

    public override void Move()
    {
        if (monsterInfo.bMoveable == false)
            return;
        Vector3 currentV = rigid.position;
        Vector3 direction = (monsterInfo.targetPos - currentV).normalized;
        if (direction.x < 0)
            spriteRenderer.flipX = false;
        else
            spriteRenderer.flipX = true;
        Vector2 nextDir = currentV +
            (direction * Time.fixedDeltaTime * monsterInfo.speed * (1 - monsterInfo.speedDecrease / 100));

        if (Vector3.Distance(currentV, monsterInfo.targetPos) <= 0.1f)
        {
            float angle = Random.Range(0, 360);
            monsterInfo.targetPos = new Vector3(Mathf.Cos(Mathf.Deg2Rad * angle) * 2, Mathf.Sin(Mathf.Deg2Rad * angle) * 2, 0);
        }
        rigid.MovePosition(nextDir);
    }

    public override bool Event(string eventname)
    {
        throw new System.NotImplementedException();
    }
    public override void Dead()
    {

    }
}
