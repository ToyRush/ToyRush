using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerUI : MonoBehaviour
{
    [SerializeField]
    private Slider slider;
    [SerializeField]
    private float maxTime=30f;
    private float[] remainTime = new float[] { 0, 0, 300, 400, 500, 600, 0 };
    bool isCount = false;
    // Update is called once per frame
    void Update()
    {
        if (isCount)
        {
            float time = maxTime - Time.time;
            slider.value = time;

            if (slider.value == 0)
            {
                Debug.Log("시간 초과!!! 패널티 발생!!!");
                GameManager.instance.TimeOver();
            }
        } 
    }

    public void ResetTimer(int _stageID)
    {
        maxTime = remainTime[_stageID];
        switch (_stageID)
        {
            case 0:
            case 1:
            case 7:
                isCount = false;
                break;
            default:
                isCount = true;
                break;
        }
    }
}
