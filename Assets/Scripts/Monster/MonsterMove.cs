using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MonsterMove : MonoBehaviour
{
    public float speed;
    public float speedDecrease;
    public Vector3 direction;
    public Vector3 targetPos;
    public bool bMoveable;
    protected Rigidbody2D rigid;
    private void Awake()
    {
        speed = 1.0f;
        speedDecrease = 0.0f;
        direction = this.gameObject.transform.forward;
        targetPos = direction;
        bMoveable = false;
        rigid = GetComponent<Rigidbody2D>();
    }
    public abstract void Move();
}
