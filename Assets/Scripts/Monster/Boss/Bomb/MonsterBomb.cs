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
    private void Awake()
    {
        circleCollider2 = GetComponent<CircleCollider2D>();
        sprite = GetComponentInChildren<SpriteRenderer>();
        anim = GetComponentInChildren<Animator>();
        particles = GetComponentsInChildren<ParticleSystem>();
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
    void Update()
    {
        if (bActive == false)
            return;

        if (particles[1].isPlaying == false)
        {
            foreach (ParticleSystem particle in particles)
                particle.Stop();

            anim.SetBool("bActive", false);
            bActive = false;
            gameObject.SetActive(false);
            RaycastHit2D[] hit2d = Physics2D.CircleCastAll(this.transform.position, collisionRadius, Vector2.zero, 0.0f);
            foreach(RaycastHit2D hit in hit2d)
            {
                if (hit.collider.gameObject.CompareTag("Player"))
                {
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
        if (collision.gameObject.CompareTag("Wall")&& bActive == false)
        {
            Debug.Log(collision.gameObject.name);
            foreach(ParticleSystem particle in particles)
                particle.Play();

            anim.SetBool("bActive", true);
            bActive = true;
        }
    }
}
