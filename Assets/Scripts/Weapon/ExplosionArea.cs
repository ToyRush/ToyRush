using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionArea : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("플레이어에게 데미지 입힘");
        }
    }
}
