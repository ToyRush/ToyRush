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
    public Vector3 initScale;
    public Quaternion initRotate;
    public Vector3 RotatePos;
    private void Start()
    {
        rigid = gameObject.GetComponent<Rigidbody2D>();
        initPos = transform.position ;
        initRotate = transform.rotation;
        initScale = transform.localScale;
        anim = gameObject.GetComponentInChildren<Animator>();
        CurrentAngle = InitAngle;
        CurrentTime = 0;
        Active = false;
    }
    private void FixedUpdate()
    {
        if (Active == false)
            return;

        CurrentAngle += Time.fixedDeltaTime * speed;
        rigid = GetComponent<Rigidbody2D>();
        //rigid.rotation = CurrentAngle;
        transform.RotateAround(target, RotatePos, Time.fixedDeltaTime * speed);

        //if (Mathf.Abs(CurrentAngle - EndAngle) <= Mathf.Abs(Time.fixedDeltaTime * speed)) // Mathf.Abs(CurrentAngle - EndAngle) < 0.1f)
        //{
        //    CurrentAngle = InitAngle;
        //    Active = false;
        //    CurrentTime = 0;
        //    anim.SetBool("Active", false);
        //    transform.position = initPos;
        //    transform.rotation = initRotate;
        //    return;
        //}
        RaycastHit2D hit = Physics2D.Raycast(rigid.transform.position, rigid.transform.right, 50);
        Debug.DrawRay(rigid.transform.position, rigid.transform.right *5, Color.red, 2);
       // Debug.dot
        if (hit && hit.transform.name != "Boss")
        {
            float size = hit.distance;
            if (hit.distance != 0)
            {
                Vector3 temp = initScale;
                temp.x *= size / 3;
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
