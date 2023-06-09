using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InerBlackHole : MonoBehaviour
{
    public List<GameObject> whiteHole;

    public bool bBlackHole;
    public int targetIndex;

    private void Start()
    {
        targetIndex = 0;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && bBlackHole == true)
        {
            collision.transform.position = whiteHole[targetIndex].transform.position;
            collision.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
            collision.GetComponent<Rigidbody2D>().angularVelocity = 0;
            whiteHole[targetIndex].GetComponent<WhiteHole>().PlayPartical();

            targetIndex = Random.Range(0, whiteHole.Count);
        }
    }

}
