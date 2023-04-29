using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatFormPlayer : MonoBehaviour
{
    [SerializeField]
    float moveSpeed;
    [SerializeField]
    float maxSpeed;
    [SerializeField]
    float jumpPower;

    bool isGround;

    Vector2 inputVec=Vector2.zero;

    Rigidbody2D rb;
    SpriteRenderer spr;
    Animator anim;
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        inputVec.x = Input.GetAxisRaw("Horizontal");
        FlipX();
    }

    void FixedUpdate()
    {
        rb.AddForce(inputVec * moveSpeed, ForceMode2D.Impulse);
        if (isGround && Input.GetKeyDown(KeyCode.Space))
            rb.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
        if (Mathf.Abs(rb.velocity.x) > maxSpeed)
            rb.velocity = new Vector2(inputVec.x * moveSpeed, rb.velocity.y);
    }

    void FlipX()
    {
        if (inputVec.x == 1)
            spr.flipX = false;
        else if (inputVec.x == -1)
            spr.flipX = true;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
            isGround = true;
        
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
            isGround = false;
    }
}
