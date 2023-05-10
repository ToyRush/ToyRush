using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleGum : Trap
{
    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            SlowDown();
        }
    }

    void SlowDown()
    {
        Debug.Log(debuffFigure);
        gameObject.SetActive(false);
    }
}
