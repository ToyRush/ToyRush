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
        slider.maxValue = maxTime;
        slider.value = 0f;
        stageKeyID = GameManager.instance.GetStageKeyID();
    }

    public void ChargeGauge(int _stageKeyID)
    {
        if (stageKeyID == _stageKeyID)
        {
            slider.value += 1f;
            if (slider.value == slider.maxValue)
            {
                Debug.Log("���������� ���� ������ Ȱ��ȭ�Ǿ����ϴ�!");
            }
        }
    }
}
