using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSlow : MonoBehaviour
{
    public ParticleSystem particle;

    public float totalDuration;

    public bool bPlayBlackHole;
    public bool isPlaying;
    private void Start()
    {
        particle = GetComponent<ParticleSystem>();
        StopPartical();
    }
    private void FixedUpdate()
    {
        // if (isPlaying)
        //{
        //    currentTime += Time.fixedDeltaTime;
        //    if (currentTime > totalDuration)
        //       StopPartical();
        //}
    }

    public void PlayPartical() //�ܺο��� ȣ�� ���ָ� ���� 
    {
        if (isPlaying == true)
            return;
        isPlaying = true;
        particle.Play();
    }
    public void StopPartical()
    {
        isPlaying = false;
        particle.Stop();
        gameObject.SetActive(false);
    }
}
