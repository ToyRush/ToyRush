using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMeteor : MonoBehaviour
{
    public ParticleSystem particle;
    public List<ParticleCollisionEvent> collisionEvents;

    public int attack;
    float totalDuration;
    float currentTime;

    public bool bPlay;
    public bool isPlaying;
    // public bool castingEnd;
    private void Awake()
    {
        particle = GetComponent<ParticleSystem>();
    }
    private void Start()
    {
        totalDuration = particle.main.duration;
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
        particle.Play();
    }
    private void OnParticleCollision(GameObject other)
    {
        if (other.tag == "Player")
            other.GetComponent<PlayerStat>().Damaged(attack);
        Invoke("StopPartical", 0.5f);
    }
    public void StopPartical()
    {
        isPlaying = false;
        bPlay = false;
        particle.Stop();

        gameObject.SetActive(false);
    }
}
