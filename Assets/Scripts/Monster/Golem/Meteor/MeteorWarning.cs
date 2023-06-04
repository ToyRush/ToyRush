using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorWarning : MonoBehaviour
{
    public ParticleSystem Head1;
    public ParticleSystem Head2;

    public int attack;
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
        StopPartical();
    }
    public void PlayPartical() //외부에서 호출 해주면 실행 
    {
        isPlaying = true;
        bPlay = true;
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
