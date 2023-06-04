using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserEnd : MonoBehaviour
{
   
    private ParticleSystem particle;

    public bool bPlay;
    private void Awake()
    {
        particle = transform.GetComponent<ParticleSystem>();
    }

    private void Start()
    {
        bPlay = false;
        StopPartical();
    }
    public void PlayPartical()
    {
        particle.Play();
        bPlay = true;
    }
    public void StopPartical()
    {
        particle.Stop();
        bPlay = false;
    }
}
