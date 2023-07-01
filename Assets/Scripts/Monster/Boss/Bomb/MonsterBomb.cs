using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterBomb : MonoBehaviour
{
    CircleCollider2D circleCollider2;
    SpriteRenderer sprite;
    Animator anim;
    ParticleSystem[] particles;
    int attack;
    bool bActive;
    float collisionRadius;
    public float currenttime;
    float attacktime;
    private void Awake()
    {
        circleCollider2 = GetComponent<CircleCollider2D>();
        sprite = GetComponentInChildren<SpriteRenderer>();
        anim = GetComponentInChildren<Animator>();
        particles = GetComponentsInChildren<ParticleSystem>();
        currenttime = 0;
        attacktime = 3.05f;
        foreach (ParticleSystem particle in particles)
        {
            particle.Stop();
        }
        anim.SetBool("bActive", false);
        bActive = false;
        attack = 1;
        collisionRadius = 2.0f;
    }
    // Update is called once per frame
    private void FixedUpdate()
    {
        if (bActive == false)
            return;

        currenttime += Time.fixedDeltaTime;

        if (particles[1].isPlaying == false)
        {
            foreach (ParticleSystem particle in particles)
                particle.Stop();

            anim.SetBool("bActive", false);
            bActive = false;
            gameObject.SetActive(false);
            currenttime = 0;
        }
        else if (currenttime > attacktime)
        {
            sprite.enabled = false;
            currenttime = 0;
            RaycastHit2D[] hit2d = Physics2D.CircleCastAll(this.transform.position, collisionRadius, Vector2.zero, 0.0f);
            foreach (RaycastHit2D hit in hit2d)
            {
                if (hit.collider.gameObject.CompareTag("Player"))
                {
                    Debug.Log("Player");
                    Vector3 dir = (-this.transform.position + hit.transform.position).normalized * 10.0f;
                    hit.collider.gameObject.GetComponent<Rigidbody2D>().AddForce(dir, ForceMode2D.Force);

                    hit.collider.gameObject.GetComponent<PlayerStat>().Damaged(attack);
                    break;
                }
            }
        }

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, collisionRadius);
    }

    private void OnEnable()
    {
        sprite.enabled = true;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (bActive == true)
            return;

        if (collision.gameObject.CompareTag("Wall")|| collision.gameObject.CompareTag("Player"))
        {
            Debug.Log(collision.gameObject.name);
            foreach(ParticleSystem particle in particles)
                particle.Play();
           
            anim.SetBool("bActive", true);
            bActive = true;
            currenttime = 0;
        }
    }

}
