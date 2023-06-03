using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserStart : MonoBehaviour
{
    private ParticleSystem particle;

    public bool bPlay;
    private void Awake()
    {
        particle = transform.GetComponent<ParticleSystem>();
    }
    private void Start()
    {
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
