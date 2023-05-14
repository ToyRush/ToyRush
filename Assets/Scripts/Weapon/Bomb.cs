using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : Trap
{
    float timeLimit=3f;
    float currentTime = 0f;
    public GameObject explosionArea;
    
    private void Update()
    {
        currentTime += Time.deltaTime;
        if (currentTime > timeLimit)
        {
            ExplosionOn();
        }
    }
    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Enemy"))
            ExplosionOn();
    }

    void ExplosionOn() {
        explosionArea.SetActive(true);
        Invoke("ExplosionOff", 0.5f);
    }

    void ExplosionOff()
    {
        explosionArea.SetActive(false);
        Cursor.cursorInstance.DeleteTrapPos(gameObject.transform.position);
        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        currentTime = 0f;
    }
}
