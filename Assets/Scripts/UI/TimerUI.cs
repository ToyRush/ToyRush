using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerUI : MonoBehaviour
{
    [SerializeField]
    private Slider slider;
    [SerializeField]
    public float maxTime=0f;
    private float[] remainTime = new float[] { 0, 0, 10, 20, 30, 40, 50, 60};
    bool isCount = false;
    [SerializeField]Text timerText;
    int min;
    int sec; 
    // Update is called once per frame
    void Update()
    {
        if (isCount)
        {
            float time = maxTime - Time.timeSinceLevelLoad;
            slider.value = time;
            min = (int)time / 60;
            sec = (int)time % 60;
            timerText.text = min.ToString() + " : " + sec.ToString();
            if (slider.value == 0)
            {
                Debug.Log("시간 초과!!! 패널티 발생!!!");
                GameManager.instance.TimeOver();
                isCount = false;
            }
        }
        else
        {
            timerText.text = "0 : 00";
        }
    }

    public void ResetTimer(int _stageID)
    {
        maxTime = remainTime[_stageID];
        switch (_stageID)
        {
            case 0:
            case 1:
            case 4:
            case 7:
                slider.maxValue = 1f;
                slider.value = 1f;
                isCount = false;
                break;
            default:
                slider.maxValue = maxTime;
                slider.value = maxTime;
                isCount = true;
                break;
        }
    }
}
