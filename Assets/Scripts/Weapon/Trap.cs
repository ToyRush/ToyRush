using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Traps;

namespace Traps
{  
    public abstract class Trap : MonoBehaviour
    {
        Rigidbody2D rb;
        protected virtual void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
        }
        protected virtual void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                gameObject.SetActive(false);
            }
        }
    }
}

public class BublleGum : Trap
{

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            gameObject.SetActive(false);
            Debug.Log("»ç¶÷ÀÌ¶û ´êÀ½");
        }
    }
}

