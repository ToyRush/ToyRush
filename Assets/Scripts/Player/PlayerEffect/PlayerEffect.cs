using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEffect : MonoBehaviour
{
    public ParticleSystem dashEffect;
    //public GameObject dash;

    void Start()
    {
        dashEffect.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.T))
            dashEffect.Play();
    }
}
