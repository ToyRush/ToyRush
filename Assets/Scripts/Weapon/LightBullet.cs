using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bullets;

public class LightBullet : Bullet
{
    public GameObject damageArea;
    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
            BombBullet();
    }

    void BombBullet()
    {
        damageArea.SetActive(true);
        Invoke("TurnOffArea", 2f);
    }

    void TurnOffArea()
    {
        damageArea.SetActive(false);
        gameObject.SetActive(false);
    }
}
