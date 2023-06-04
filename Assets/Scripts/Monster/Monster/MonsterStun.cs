using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterStun : MonoBehaviour
{
    public ParticleSystem Star1;
    public ParticleSystem Star2;
    public ParticleSystem Star3;

    public float totalDuration;
    float currentTime;

    public bool bPlayBlackHole;
    public bool isPlaying;
    private void Start()
    {
        Star1 = transform.GetChild(0).GetComponent<ParticleSystem>();
        Star2 = transform.GetChild(1).GetComponent<ParticleSystem>();
        Star3 = transform.GetChild(1).GetComponent<ParticleSystem>();
        totalDuration = Star1.main.duration + Star1.main.startLifetimeMultiplier;
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
        if (isPlaying == true)
            return;
        isPlaying = true;
        currentTime = 0;
        Star1.Play();
        Star2.Play();
        Star3.Play();
    }
    public void StopPartical()
    {
        isPlaying = false;
        Star1.Stop();
        Star2.Stop();
        Star3.Stop();
        gameObject.SetActive(false);
    }

}
