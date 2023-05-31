using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricBullet : Bullet
{
    public GameObject damageArea;
    public GameObject bulletParticle;
    public AudioClip electricSound;
    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        audioSource.PlayOneShot(electricSound, 1f);
        if (collision.gameObject.CompareTag("Wall") || collision.gameObject.CompareTag("Monster"))
            BombBullet();
        else
            gameObject.SetActive(false);
    }

    void BombBullet()
    {
        damageArea.SetActive(true);
        rb.velocity = Vector3.zero;
        bulletParticle.SetActive(false);
        Invoke("TurnOffArea", 0.3f);
    }

    void TurnOffArea()
    {
        damageArea.SetActive(false);
        gameObject.SetActive(false);
    }

    public override void SetBulletDelay()
    {
        shootDelay = new WaitForSeconds(0.5f);
    }

    private void OnEnable()
    {
        bulletParticle.SetActive(true);
    }
}
