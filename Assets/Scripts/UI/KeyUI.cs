using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyUI : MonoBehaviour
{
    // 키의 아이디는 이미지의 인덱스번호와 동일함
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
            slider.value += 1f;
            if (slider.value == slider.maxValue)
            {
                GameManager.instance.OpenDoor();
            }
        }
    }

    public void LostGauge()
    {
        int var = (int)slider.value / 2;
        int maxvar = (int)slider.maxValue;
        slider.value -= (float)var;
        Debug.Log(maxvar - var+"만큼 몬스터가 출현합니다!");
    }

    public void ChangeKeyInfo()
    {
        if (GameManager.instance.GetStageKeyCnt() == 1)
        {
            slider.maxValue = 1f;
            slider.value = 1f;
            GameManager.instance.OpenDoor();
        }
        else
        {
            slider.value = 0f;
            slider.maxValue = GameManager.instance.GetStageKeyCnt();
        }
        stageKeyID = GameManager.instance.GetStageKeyID();
    }
}
