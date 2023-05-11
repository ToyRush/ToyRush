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
                Debug.Log("다음층으로 가는 조건이 활성화되었습니다!");
            }
        }
    }
}
