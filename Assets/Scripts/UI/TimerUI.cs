using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerUI : MonoBehaviour
{
    public float maxTime=0f;
    private float[] remainTime = new float[] { 0, 0, 90, 90, 90, 90, 0, 0};
    bool isCount = false;
    [SerializeField]Text timerText;
    [SerializeField] GameObject timerInfoUI;
    int min;
    int sec; 
    // Update is called once per frame
    void Update()
    {
        if (isCount)
        {
            float time = maxTime - Time.timeSinceLevelLoad;
            min = (int)time / 60;
            sec = (int)time % 60;
            timerText.text = min.ToString() + "  " + sec.ToString();
            if (time<=0)
            {
                GameManager.instance.TimeOver();
                timerInfoUI.SetActive(true);
                isCount = false;
            }
        }
        else
        {
            timerText.text = "0  0";
        }
    }

    public void ResetTimer(int _stageID)
    {
        maxTime = remainTime[_stageID];
        switch (_stageID)
        {
            case 0:
            case 1:
            case 6:
            case 7:
                isCount = false;
                break;
            default:
                isCount = true;
                break;
        }
    }
}
