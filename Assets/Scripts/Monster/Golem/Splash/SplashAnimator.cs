using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplashAnimator : MonoBehaviour
{
    public Animator animator;

    private bool bAttackable;
    public int attack;
    public bool bPlay;
    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        bAttackable = false;
    }

    void Start()
    {
        // state reset
        AttackEnd();
    }

    // golem splash 에서 호출
    public void PlaySplash()
    {
        bPlay = true;
        animator.enabled = true;
        animator.SetBool("bAttack", true);
    }

    // earthquake_sprite_0 animation에서 호출
    public void Attack()
    {
        bAttackable = true;
    }

    // earthquake_sprite_0 animation에서 호출
    public void AttackEnd()
    {
        bPlay = false;
        animator.SetBool("bAttack", false);
        animator.enabled = false;
        bAttackable = false;
            
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (bAttackable && collision.tag == "Player")
        {
            print("Splash");
            bAttackable = false;
            collision.gameObject.transform.GetComponent<PlayerStat>().Damaged(attack);
        }
    }
}
