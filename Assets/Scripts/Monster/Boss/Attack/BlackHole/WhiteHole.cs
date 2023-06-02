using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhiteHole : MonoBehaviour
{
    public ParticleSystem Head1;
    public ParticleSystem Head2;
    
    float totalDuration;
    float currentTime;

    public bool bPlayBlackHole;
    public bool isPlaying;
    private void Awake()
    {
        Head1 = transform.GetChild(0).GetComponent<ParticleSystem>();
        Head2 = transform.GetChild(1).GetComponent<ParticleSystem>();
        totalDuration = Head1.main.duration + Head1.startLifetime;
        StopPartical();
    }
    private void FixedUpdate()
    {
        if (isPlaying)
        {
            currentTime += Time.fixedDeltaTime;
            if (currentTime > totalDuration)
                StopPartical();
        }
    }

    public void PlayPartical() //외부에서 호출 해주면 실행 
    {
        isPlaying = true;
        currentTime = 0;
        Head1.Play();
        Head2.Play();
    }
    public void StopPartical()
    {
        isPlaying = false;
        Head1.Stop();
        Head2.Stop();
    }

}
