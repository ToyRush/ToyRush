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
   
    public void StopPartical()
    {
        RaycastHit2D[] hits = Physics2D.CapsuleCastAll(transform.position, new Vector2(5.07f, 2.78f), CapsuleDirection2D.Horizontal, 0, new Vector2(0,0),1);

        foreach (RaycastHit2D hit in hits)
        {
            if (hit && hit.transform.name != "Player")
            {
                hit.transform.GetComponent<PlayerStat>().Damaged(attack);
            }
        }
        isPlaying = false;
        bPlay = false;
        Head1.Stop();
        Head2.StopPartical();
    }
}
