using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Moving : MonoBehaviour
{
    // target Postion private
    public Vector2 targetV;
    public float speed;
    private float idleMaxTime;
    public float moveTime;
    private bool canMove;

    // patrol positions
    public Vector2[] fixedPos;
    private int curindex;
    private bool isplus;

    private void Awake()
    {
    }
    // Start is called before the first frame update
    void Start()
    {
        speed = 14.0f;
        curindex = 0;
        isplus = true;
        idleMaxTime = 3.0f;
        moveTime = 3.0f;
        canMove = false;
    }

    private void Update()
    {
        moveTime -= Time.deltaTime;
        if (moveTime < 0.0f)
            canMove = true;
    }
    public void SetTargetV(Vector2 target)
    {
        targetV = target;
    }
    public Vector2 GetTargetV()
    {
        return targetV;
    }

    public void setCanMove(bool move)
    {
        canMove = move;
    }
    public bool getCanMove()
    {
        return canMove;
    }
    public void setFixedPos(Vector2[] pos)
    {
        fixedPos = pos;
        targetV = pos[0];
    }

    public void Move()
    {
        if (canMove)
        {
            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            if (rb == null)
                return;

            Vector2 currentV = rb.position;
            Vector2 direction = (targetV - currentV).normalized;
            Vector2 nextDir = currentV + (direction * speed * Time.deltaTime);
            rb.MovePosition(nextDir);
        }
    }

    public void SetNextFixedPos()
    {
        if (isplus)
            curindex++;
        else
            curindex--;
        moveTime = idleMaxTime;
        canMove = false;
        if (curindex >= fixedPos.Length)
        {
            isplus = false;
            curindex = fixedPos.Length - 1;
        }
        if (curindex < 0)
        {
            isplus = true;
            curindex = 0;
        }
        targetV = fixedPos[curindex];
    }
}



//protected virtual void Move(Vector2 targetV)
//{
//    Vector2 currentV = rb.position;
//    Vector2 direction = (targetV - currentV).normalized;

//    if (direction.x < 0)
//        spriteManager.SetSprFlip(true);
//    else
//        spriteManager.SetSprFlip(false);

//    Vector2 nextDir = currentV + (direction * speed * Time.deltaTime);
//    rb.MovePosition(nextDir);
//}