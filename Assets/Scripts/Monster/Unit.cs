using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// lee : todo object �� objectpolling������� ���� �ϵ��� ����غ���

public enum UnitState
{
    Idle = 0,
    Walk = 1,
    Patrol =2,

    Action = 10,
    Attack = 11,
    Hitted = 12,

    Dead = 20
}

public class Unit : MonoBehaviour
{
    protected SpriteManager spriteManager;
    protected Rigidbody2D rb;
    protected CapsuleCollider2D bodyCol;
    protected GameObject enemy;
    protected Moving moving;

    // private
    public UnitState unitState;
    public float attackDis;
    public float searchDis;
    public float Hp;
    //-- 

    protected virtual void  Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        bodyCol = GetComponent<CapsuleCollider2D>();
        spriteManager = GetComponent<SpriteManager>();
        moving = GetComponent<Moving>();

        if (rb == null)
            rb = gameObject.AddComponent<Rigidbody2D>();
        if (bodyCol == null)
            bodyCol = gameObject.AddComponent<CapsuleCollider2D>();
        if (spriteManager == null)
            spriteManager = gameObject.AddComponent<SpriteManager>();
        if (moving == null)
            moving = gameObject.AddComponent<Moving>();

        enemy = GameObject.FindWithTag("Player");
    }
    protected virtual void Start()
    {
        rb.bodyType = RigidbodyType2D.Kinematic;
        rb.gravityScale = 0.0f;
        spriteManager.Init(GetComponent<SpriteRenderer>().sprite, GetComponent<Animator>().runtimeAnimatorController);
        unitState = UnitState.Walk;
        spriteManager.SetState(unitState);
        Hp = 10.0f;
        attackDis = 2.0f;
        searchDis = 6.0f;
    }

    protected virtual void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha3)) // test�� Ű
        {
            Hp = 10.0f;
            unitState = UnitState.Idle;
        }
        BehaviorTree();
        Action();
    }

    protected virtual void FixedUpdate()
    {
    }

    public UnitState getUnitState()
    {
        return this.unitState;
    }


    // action
    protected virtual void BehaviorTree() // monster behavior tree
    {
        if (unitState == UnitState.Dead)
            return;

        Vector2 targetV = moving.GetTargetV();
        float enemyDis = Vector2.Distance(enemy.transform.position, rb.position);
        if (enemyDis < searchDis) // enemy detect
        {
            moving.SetTargetV(enemy.transform.position);
            moving.setCanMove(true);
            unitState = UnitState.Walk;
            if (enemyDis < attackDis) // enemy attck
            {
                unitState = UnitState.Attack;
                // ���� ��ų ������ ���⿡ �߰� 
                if (enemy.GetComponent<Unit>() != null &&
                    enemy.GetComponent<Unit>().getUnitState() == UnitState.Dead) // if enemy dead stop,
                {
                    moving.SetNextFixedPos();
                    unitState = UnitState.Idle;
                }
            }
        }
        else // patrol
        {
            float dis = Vector2.Distance(targetV, rb.position);
            if (dis < 0.1f)
                 moving.SetNextFixedPos();

            if (moving.getCanMove())
                unitState = UnitState.Patrol;
            else
                unitState = UnitState.Idle;
        }
    }
    protected virtual void Action()
    {
        spriteManager.SetState(unitState);
        switch (unitState)
        {
            case UnitState.Walk:
                {
                    moving.Move();
                    break;
                }
            case UnitState.Patrol:
                {
                    moving.Move();// get next patrol position
                    break;
                }
            case UnitState.Attack:
                {
                    Attack();
                    break;
                }
            default:
                break;   
        }
        spriteManager.SetFlips();
    }


    // attack dead
    protected virtual void Dead() // Delete obj
    {
        Debug.Log("Dead");
        Destroy(this.gameObject, 2.0f);
    }

    protected virtual void Attack()
    {
        moving.setCanMove(false);
        Vector2 direction = (moving.GetTargetV() - rb.position).normalized;
        hit = Physics2D.BoxCast(rb.position + direction * 1.0f, new Vector2(0.5f,1),
            0, direction) ;
        if (hit != false)
        {
            gizmos = true;
            if (hit.transform.gameObject.GetComponent<PlayerMove>() != null)
            {

                hit.transform.gameObject.GetComponent<SpriteRenderer>().color = Color.red;
            }
        }
        else
            gizmos = false;
        //Debug.Log("attack");
    }

    public virtual void Damaged(float attack)
    {
        if (unitState != UnitState.Dead)
        {
            Hp -= attack;
            if (Hp < 0.01f)
                unitState = UnitState.Dead;
            else
                unitState = UnitState.Hitted;
        }
    }
   


    // Action
    RaycastHit2D hit;
    bool gizmos;
    private void OnDrawGizmos()
    {
        if (unitState == UnitState.Attack)
            Gizmos.DrawCube((Vector2)transform.position + (moving.GetTargetV() - (Vector2)transform.position).normalized * 1.0f,
                new Vector2(0.5f, 1));
    }
}


// Physics.BoxCast (
// �������� �߻��� ��ġ,
// �簢���� �� ��ǥ�� ���� ũ��,
// �߻� ����,
// �浹 ���,
// ȸ�� ����,
// �ִ� �Ÿ�)
//RaycastHit hit;
////���콺 ��ġ
//Vector3 point = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -Camera.main.transform.position.z));
////z���� ����� Ray�� ������Ʈ�� �����Ѵ�. 
//Vector2 mouseP = new Vector2(point.x, point.y);
////��ġ Ȯ�ο� �ڵ�
///*if(Input.GetMouseButtonDown(0))
//{
//    Debug.Log(mouseP.ToString());
//}
// */
////����ĳ��Ʈ
//if (Physics.Raycast(transform.position, dir, out hit))
//{
//    if (hit.GetType().Equals("Wall"))
//    {
//    //�浹�� ������Ʈ�� ���� IObject������Ʈ �������� �õ�
//    IObject obj = hit.transform.GetComponent<IObject>();
//    //�������� �����ϸ�
//    if (obj != null && Input.GetMouseButtonDown(0))
//    {
//        //�Լ� �ߵ�
//        obj.Iam();
//        //���� �ٲٱ�
//        hit.transform.GetComponent<MeshRenderer>().material.color = Color.red;
//    }
//    }
//}
