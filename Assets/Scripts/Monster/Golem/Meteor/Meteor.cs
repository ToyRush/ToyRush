using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor : MonoBehaviour
{
    public ParticleSystem Head1;
    public MeteorWarning Head2;
    public int attack;
    float totalDuration;
    float currentTime;

    public bool bPlay;
    public bool isPlaying;
    // public bool castingEnd;
    private void Awake()
    {
        Head1 = transform.GetChild(0).GetComponent<ParticleSystem>();
        Head2 = transform.GetChild(1).GetComponent<MeteorWarning>();
    }
    private void Start()
    {
        totalDuration = Head1.main.duration + Head1.main.startLifetimeMultiplier;
        StopPartical();
    }
    private void FixedUpdate()
    {
        if (bPlay)
        {
            if (isPlaying == false)
                PlayPartical();
            currentTime += Time.fixedDeltaTime;
            if (currentTime > totalDuration)
                StopPartical();
        }
    }
    public void PlayPartical() //외부에서 호출 해주면 실행 
    {
        isPlaying = true;
        bPlay = true;
        currentTime = 0;
        Head1.Play();
        Head2.PlayPartical();
    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, new Vector2(3.0f, 2.5f));
    }
    public void StopPartical()
    {
        //        Collider2D[] hits = Physics2D.OverlapCapsuleAll(transform.position, new Vector2(3.0f, 2.5f), CapsuleDirection2D.Horizontal, 0);
       RaycastHit2D[] hits  = Physics2D.CapsuleCastAll(transform.position, new Vector2(3.0f, 2.5f), 
           CapsuleDirection2D.Horizontal, 0 , new Vector2(0,0) ,0);

        for (int i = 0; i < hits.Length; i++)
        {
            if (hits[i].transform.tag == "Player")
            {
                isPlaying = false;
                bPlay = false;
                Head1.Stop();
                Head2.StopPartical();
                print("Meteo");
                gameObject.SetActive(false);
                hits[i].transform.GetComponent<PlayerStat>().Damaged(attack);
            }
        }
        isPlaying = false;
        bPlay = false;
        Head1.Stop();
        Head2.StopPartical();

        gameObject.SetActive(false);
    }
}
