using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterHit : MonoBehaviour
{
    public List<ParticleSystem> particles;

    float totalDuration;
    float currentTime;

    public bool bPlayBlackHole;
    public bool isPlaying;
    private void Awake()
    {
        for (int i =0;i < transform.childCount; i++)
        {
            if (transform.GetChild(i).GetComponent<ParticleSystem>() != null)
                particles.Add(transform.GetChild(i).GetComponent<ParticleSystem>());
        }
        totalDuration = particles[0].main.duration + particles[0].main.startLifetimeMultiplier;
    }
    private void Start()
    {
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
        for (int i = 0; i < particles.Count; i++)
            particles[i].Play();
    }
    public void StopPartical()
    {
        isPlaying = false;
        for (int i = 0; i < particles.Count; i++)
            particles[i].Stop();
    }

}
