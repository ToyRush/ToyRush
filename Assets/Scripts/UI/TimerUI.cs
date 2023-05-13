using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerUI : MonoBehaviour
{
    bool isBoss = false;
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
        if (slider.value == 0 && !isBoss)
        {
            Debug.Log("중간보스가 등장했습니다!");
            isBoss = true;
        }
       
    }
}
