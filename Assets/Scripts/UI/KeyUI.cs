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
    [SerializeField] Image bgImage;
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

    public void LostGauge() // needVar*2 만큼 몬스터 생성시키면 됨   // 6월 30일 몬스터 출현 추가 - 은구 -
    {
        int var = (int)(slider.maxValue - slider.value); // 현재개수
        int needVar = (int)slider.maxValue - var; 
        slider.value += (float)needVar;
        Debug.Log( needVar*2+"만큼 몬스터가 출현합니다!");
        MonsterManager monmanager = GameObject.Find("MonstersGroup").GetComponent<MonsterManager>();
        monmanager.bSpone = true;
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
        SetKeyColor();
    }

    void SetKeyColor()
    {
        switch (stageKeyID)
        {
            case 400:
                bgImage.color = Color.red;
                break;
            case 401:
                bgImage.color = Color.green;
                break;
            default:
                bgImage.color = Color.white;
                break;
        }
    }
}
