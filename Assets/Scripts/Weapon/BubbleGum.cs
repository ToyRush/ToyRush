using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleGum : Trap
{
    Animator bubbleAnim;
    BoxCollider2D bubbleCollider;
    [SerializeField] float debuffFigure;
    [SerializeField] ParticleSystem popParticle;
    private void Awake()
    {
        bubbleAnim = GetComponent<Animator>();
        bubbleCollider = GetComponent<BoxCollider2D>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            SlowDown();
        }
    }
 
    void SlowDown()
    {
        Debug.Log(debuffFigure);
        bubbleAnim.SetBool("isPop", false);
        bubbleCollider.enabled = false;
        gameObject.SetActive(false);
    }

    public void PopGum()
    {
        bubbleAnim.SetBool("isPop", true);
        bubbleCollider.enabled = true;
        popParticle.Play();

    }
}
