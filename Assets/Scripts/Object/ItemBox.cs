using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBox : MonoBehaviour
{
    [SerializeField] GameObject healItem;
    int randomInt;
    private void Awake()
    {
        randomInt = UnityEngine.Random.Range(1, 10);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            Instantiate(healItem, gameObject.transform.position,Quaternion.identity);
            gameObject.SetActive(false);
        }
    }
}
