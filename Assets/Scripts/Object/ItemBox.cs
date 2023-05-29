using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBox : MonoBehaviour
{
    [SerializeField] GameObject healItem;
    [SerializeField] ParticleSystem popEffect;
    int randomInt;
    bool isHit=false;
    private void Awake()
    {
        randomInt = UnityEngine.Random.Range(1, 10);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet") && !isHit)
        {
            popEffect.Play();
            isHit = true;
            if(randomInt<4)
                Instantiate(healItem, gameObject.transform.position,Quaternion.identity);
            Invoke("OffBox", 0.5f);
        }
    }

    private void OffBox()
    {
        gameObject.SetActive(false);
    }
}
