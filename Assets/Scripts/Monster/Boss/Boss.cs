using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Monster
{
    public BossText bossText; // ����

    public int AttackCase;
    public GameObject BlackHole;
    public GameObject Laser; 
    public GameObject Bomb;

    public bool bAttack;
    public bool bAttacking;
    public new float currentTime;
    public float blockTime;
    public float bombTime;
    private new  void Awake()
    {
        base.Awake();
        BlackHole = transform.GetChild(0).gameObject;
        Laser = transform.GetChild(1).gameObject;
        Bomb = transform.GetChild(2).gameObject;
    }
   new void  Start()
    {
        base.Start();
        monsterInfo.hp = 60.0f;
        monsterInfo.attack = 2;
        monsterInfo.state = MonsterState.Stop;
        monsterInfo.findDis = 100.0f;
        monsterInfo.attackDis = 100.0f;
        monsterInfo.currentTime = 0.0f;
        monsterInfo.delayTime = 5.0f;
        // Lazer = this.transform.GetComponentInChildren<GameObject>();
        blockTime = 0.2f;
        bombTime = 2.7f;
        AttackCase = -1;
        bAttack = false;
        bAttacking = false;
        Laser.GetComponent<BossLaser>().attack = monsterInfo.attack;
    }

    private new void FixedUpdate()
    {
        if (monsterInfo.state == MonsterState.Dead)
            return ;
        if (bAttack == false)
             monsterInfo.currentTime += Time.deltaTime;
        BehaviorTree();
    }
    private new void Update()
    {
        if (monsterInfo.state == MonsterState.Dead)
            return ;
        if (bAttack == true)
            Attack();
        if (Input.GetKeyDown(KeyCode.L))
            Damaged(100);
    }
    // Update is called once per frame
    public override MonsterState BehaviorTree()
    {
        if (bAttack == false && monsterInfo.currentTime > monsterInfo.delayTime)
        {
            bAttack = true;
            AttackCase++;
            if (AttackCase >= 3)
                AttackCase = 0;
            monsterInfo.state = MonsterState.Attack;
        }
        if (bAttack == true && bAttacking == false)
        {
            monsterInfo.state = MonsterState.Stop;
        }
        animator.SetInteger("State", (int)monsterInfo.state);
        return monsterInfo.state;
    }

    public override bool Damaged(int attack)
    {
        if (monsterInfo.state == MonsterState.Dead)
            return false;

        monsterInfo.hp -= attack;
        if (monsterInfo.hp < 0.1f)
        {
            monsterInfo.state = MonsterState.Dead;
            hitEffect.GetComponent<MonsterHit>().PlayPartical();
            spriteRenderer.enabled = false;
            Invoke("Dead", 2.0f);
        }
        return true;
    }
    public void SkillEnd()
    {
        bAttacking = false;
    }
    public override void Attack()
    {
        if (bAttacking == false)
        {
            bAttacking = true;
            if (AttackCase == 0)
            {
                BlackHole.SetActive(true);
                BlackHole.GetComponent<Blackhole>().bPlayBlackHole = true;
                Invoke("EndBlackHole", 5.0f);
            }
            else if (AttackCase == 1)
            {
                Laser.SetActive(true);
                int bright = Random.Range(0,9);
                if (bright > 4 )
                    Laser.GetComponent<BossLaser>().bRight = true;
                else
                    Laser.GetComponent<BossLaser>().bRight = false;
                Laser.GetComponent<BossLaser>().bActive = true;

            }
            else if (AttackCase == 2)
            {
                Bomb.SetActive(true);
                Bomb.GetComponent<BossBombAttack>(); // ~~
                BossBombManager.Instance.attackcount = 0;
            }
        }
        else
        {
            if (AttackCase == 0)
            {
                currentTime += Time.fixedDeltaTime;
                if (currentTime > blockTime)
                {
                    currentTime = 0;

                    BossMeteorManager.Instance.ResponMeteor();
                }
            }
            else if (AttackCase == 1)
            {
                if (Laser.GetComponent<BossLaser>().bActive == false)
                {
                    monsterInfo.currentTime = 0;
                    bAttacking = false;
                    bAttack = false;
                    Laser.SetActive(false);
                }
            }
            else if (AttackCase == 2)
            {
                currentTime += Time.fixedDeltaTime;
                if (currentTime > bombTime)
                {
                    currentTime = 0;

                    BossBombManager.Instance.ResponMeteor();
                    if (BossBombManager.Instance.attackcount == BossBombManager.Instance.maxBombCounts)
                    {
                        bAttacking = false;
                        bAttacking = false;
                        bAttack = false;
                    }
                }
            }
        }
    }

    void EndBlackHole()
    {
        monsterInfo.currentTime = 0;
        bAttacking = false;
        bAttack = false;
        BlackHole.GetComponent<Blackhole>().StopPartical();
    }

    public override void Move()
    {
        return;
    }

    public override void Dead()
    {
        hitEffect.GetComponent<MonsterHit>().StopPartical();
        bossText.BossEndText(); // ����
        this.gameObject.SetActive(false);
    }
}
