using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterBullet : MonoBehaviour
{
    // Start is called before the first frame update
    protected SpriteRenderer spriteRenderer;
    protected BoxCollider2D boxCollider2D;
    protected Rigidbody2D rigid;

    public float speed;
    public float currentTime;
    public float shotTime;
    protected int attack;
    protected Vector3 direction;
    // Start is called before the first frame update
    private void Awake()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        boxCollider2D = gameObject.GetComponent<BoxCollider2D>();
        rigid = gameObject.GetComponent<Rigidbody2D>();
        speed = 5;
        shotTime = 2.0f;
        attack = 10;
        currentTime = 0;
        direction = new Vector3();
        direction = Vector3.zero;
        direction.x = 1;
    }

    public void FireBullet(Vector3 dir, int damege)
    {
        this.attack = damege;
        this.direction = dir;
    }

    private void FixedUpdate()
    {
        currentTime += Time.fixedDeltaTime ;
        Vector3 temp = rigid.transform.position;
        temp += direction * speed * Time.fixedDeltaTime;
        rigid.MovePosition(temp);
        if (currentTime > shotTime)
            Dead();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
            Dead();

        if (collision.gameObject.CompareTag("Player"))
        {
            Dead();
            collision.gameObject.GetComponent<PlayerStat>().Damaged((int)attack);
        }
    }
    public virtual void Dead()
    {
        currentTime = 0;
        this.gameObject.SetActive(false);
    }
}
