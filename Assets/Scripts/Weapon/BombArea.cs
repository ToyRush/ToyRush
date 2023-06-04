using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombArea : MonoBehaviour
{
   [SerializeField] int damage;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Monster"))
        {
            Monster monster = collision.gameObject.GetComponent<Monster>();
            monster.Damaged(damage);
            monster.Event("Stun");
        }else if (collision.gameObject.CompareTag("Player"))
        {
            PlayerStat playerStat = collision.gameObject.GetComponent<PlayerStat>();
            playerStat.Damaged(damage-1);
        }
    }
}
