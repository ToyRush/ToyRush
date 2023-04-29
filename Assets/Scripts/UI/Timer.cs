using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    [SerializeField]
    private Slider slider;
    [SerializeField]
    private float maxTime=30f;

    private void Start()
    {
        slider.maxValue = maxTime;
        slider.value = maxTime;
    }
    // Update is called once per frame
    void Update()
    {
        float time = maxTime - Time.time;
        slider.value = time;    
    }
}
