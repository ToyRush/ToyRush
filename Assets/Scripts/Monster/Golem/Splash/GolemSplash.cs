using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolemSplash : MonoBehaviour
{
    // Start is called before the first frame update
    public SplashAnimator anim;
    public ParticleSystem particle;
    public bool bPlay;
    public bool bPlaying;
    private void Awake()
    {
        anim = GetComponentInChildren<SplashAnimator>();
        particle = GetComponentInChildren<ParticleSystem>();
    }

    void Start()
    {
        StopSplash();
    }

    private void FixedUpdate()
    {
        if (bPlay == true && bPlaying == false)
            PlaySplash();
        if (bPlaying == true && anim.bPlay == false)
            StopSplash();
    }
    public void PlaySplash()
    {
        bPlaying = true;
        anim.PlaySplash();
        particle.Play();
    }

    public void StopSplash()
    {
        bPlay = false;
        bPlaying = false;
        particle.Stop();
    }
}
