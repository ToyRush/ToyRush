using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyUI : MonoBehaviour
{
    // Ű�� ���̵�� �̹����� �ε�����ȣ�� ������
    [SerializeField]
    private Slider slider;
    [SerializeField]
    private float maxTime;

    private int stageKeyID=400;

    private void Start()
    {
        ChangeKeyInfo();
    }

    public void ChargeGauge(int _stageKeyID)
    {
        if (stageKeyID == _stageKeyID)
        {
            slider.value -= 1f;
            if (slider.value == 0)
            {
                GameManager.instance.OpenDoor();
            }
        }
    }

    public void LostGauge()
    {
        int var = (int)(slider.maxValue - slider.value); // ���簳��
        int needVar = (int)slider.maxValue - var; 
        slider.value += (float)needVar;
        Debug.Log( needVar*2+"��ŭ ���Ͱ� �����մϴ�!");
    }

    public void ChangeKeyInfo()
    {
        if (GameManager.instance.GetStageKeyCnt() == 1)
        {
            slider.maxValue = 1f;
            slider.value = 0f;
        }
        else
        {
            slider.maxValue = GameManager.instance.GetStageKeyCnt();
            slider.value = slider.maxValue;
        }
        stageKeyID = GameManager.instance.GetStageKeyID();
    }
}
