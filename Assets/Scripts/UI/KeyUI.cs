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

    public void LostGauge() // needVar*2 ��ŭ ���� ������Ű�� ��   // 6�� 30�� ���� ���� �߰� - ���� -
    {
        int var = (int)(slider.maxValue - slider.value); // ���簳��
        int needVar = (int)slider.maxValue - var; 
        slider.value += (float)needVar;
        Debug.Log( needVar*2+"��ŭ ���Ͱ� �����մϴ�!");
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
