using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blackhole : MonoBehaviour
{
    private ParticleSystem Glow;
    private ParticleSystem Head1;
    private ParticleSystem Head2;
    private GameObject center;

    public float blckHoleSpeed;
    public bool bPlayBlackHole;
    public bool isPlaying;
    private void Awake()
    {
        Glow = transform.GetChild(0).GetComponent<ParticleSystem>();
        Head1 = transform.GetChild(1).GetComponent<ParticleSystem>();
        Head2 = transform.GetChild(2).GetComponent<ParticleSystem>();
        center = transform.GetChild(3).gameObject;
        
    }

    private void Start()
    {
        StopPartical();
    }

    private void FixedUpdate()
    {
        if (bPlayBlackHole == false)
            StopPartical();
        else if (isPlaying == false)
            PlayPartical();
    }

    public void PlayPartical()
    {
        isPlaying = true;
        center.GetComponent<InerBlackHole>().bBlackHole = true;
        Glow.Play();
        Head1.Play();
        Head2.Play();
    }
    public void StopPartical()
    {
        center.GetComponent<InerBlackHole>().bBlackHole = false;
        isPlaying = false;
        Glow.Stop();
        Head1.Stop();
        Head2.Stop();
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && isPlaying)
        {
            Vector3 dir = (this.transform.position - collision.transform.position).normalized * blckHoleSpeed;
            collision.GetComponent<Rigidbody2D>().AddForce(dir, ForceMode2D.Force); // add force라 이상현상이 생김 (벽에  갖히거나  날라 가는 등)

        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player" && isPlaying)
        {
            Vector3 dir = (this.transform.position - collision.transform.position).normalized * blckHoleSpeed;
            collision.GetComponent<Rigidbody2D>().AddForce(dir, ForceMode2D.Force); // add force라 이상현상이 생김
        }
    }
}