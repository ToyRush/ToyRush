using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : MonoBehaviour
{
    [SerializeField]AudioClip hitSound;
    Animator anim;
    AudioSource audioSource;
    void Awake()
    {
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            anim.SetTrigger("Hit");
            audioSource.PlayOneShot(hitSound, 0.8f);
        }
    }
}
