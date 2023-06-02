using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserCasting : MonoBehaviour
{
    public ParticleSystem Head1;
    public ParticleSystem Head2;

    float totalDuration;
    float currentTime;
    float initX;

    public bool bRight;
    public bool bPlayBlackHole;
    public bool isPlaying;
    // public bool castingEnd;
    private void Awake()
    {
        Head1 = transform.GetChild(0).GetComponent<ParticleSystem>();
        Head2 = transform.GetChild(1).GetComponent<ParticleSystem>();
        totalDuration = Head1.main.duration + Head1.startLifetime;
        initX = transform.position.x;
        StopPartical(); 
    }
    private void FixedUpdate()
    {
        if (bPlayBlackHole)
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
        Vector3 temp = transform.position;
        if (bRight == true)
            temp.x = initX;
        else
            temp.x = -initX;
        transform.position = temp;
        isPlaying = true;
        bPlayBlackHole = true;
        currentTime = 0;
        Head1.Play();
        Head2.Play();
    }
    public void StopPartical()
    {
        isPlaying = false;
        bPlayBlackHole = false;
        Head1.Stop();
        Head2.Stop();
    }
}
