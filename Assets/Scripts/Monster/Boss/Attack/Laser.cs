using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    public float InitAngle;
    public float CurrentAngle;
    public float EndAngle;
    public float speed;
    public bool Active;
    protected float CurrentTime;
    protected Rigidbody2D rigid;
    protected Animator anim;
    public Vector3 target;
    public Vector3 initPos;
    private void Start()
    {
        rigid = gameObject.GetComponent<Rigidbody2D>();
        initPos = transform.position ;
        target = rigid.transform.position;
        target.x -= 8;
        anim = gameObject.GetComponentInChildren<Animator>();
        InitAngle = 0;
        CurrentAngle = InitAngle;
        EndAngle = -120;
        CurrentTime = 0;
        speed = 20;
        Active = false;
        transform.localScale = new Vector2(1, 5);
    }
    private void FixedUpdate()
    {
        if (Active == false)
            return;

        CurrentTime += Time.fixedDeltaTime * speed;
        CurrentAngle =  Mathf.Lerp(InitAngle, EndAngle, CurrentTime);
        rigid = GetComponent<Rigidbody2D>();
        //rigid.rotation = CurrentAngle;
        transform.RotateAround(target, new Vector3(0, 0, 1), Time.fixedDeltaTime * speed);

        if (CurrentTime >= 100.0f) // Mathf.Abs(CurrentAngle - EndAngle) < 0.1f)
        {
            CurrentAngle = InitAngle;
            Active = false;
            CurrentTime = 0;
            anim.SetBool("Active", false);
            transform.position = initPos;
            transform.rotation = Quaternion.identity;
            return;
        }
        RaycastHit2D hit = Physics2D.Raycast(rigid.transform.position, Vector2.down, 5);
        Debug.DrawRay(target, transform.right * 20,Color.red,  2);

        if (hit)
        {
            float size = hit.distance;
            if (hit.distance != 0)
            {
                Vector3 temp = transform.localScale;
                temp.x = size;
                transform.localScale = temp;
            }
        }

    }
    public void ActiveLazer(bool Left = false)
    {
        if (Left == true)
            EndAngle = 120;
        else
            EndAngle = -120;
        Active = true;
        anim.SetBool("Active", Active);
    }

    public void AttackEnd()
    {
        //CurrentAngle = InitAngle;
        //Active = false;
        //CurrentTime = 0;
        //anim.SetBool("Active", false);
        //transform.rotation = Quaternion.identity;
    }
}
