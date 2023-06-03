using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserCasting : MonoBehaviour
{
    public ParticleSystem Head1;
    public ParticleSystem Head2;

    float totalDuration;
    float currentTime;

    public bool bPlay;
    public bool isPlaying;
    // public bool castingEnd;
    private void Awake()
    {
        Head1 = transform.GetChild(0).GetComponent<ParticleSystem>();
        Head2 = transform.GetChild(1).GetComponent<ParticleSystem>();
      
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
    public void PlayPartical() //�ܺο��� ȣ�� ���ָ� ���� 
    {
        isPlaying = true;
        bPlay = true;
        currentTime = 0;
        Head1.Play();
        Head2.Play();
    }
    public void StopPartical()
    {
        isPlaying = false;
        bPlay = false;
        Head1.Stop();
        Head2.Stop();
    }
}
