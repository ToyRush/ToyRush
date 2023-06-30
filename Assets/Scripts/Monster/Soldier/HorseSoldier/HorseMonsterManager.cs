using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorseMonsterManager : MonsterManager
{
    private static HorseMonsterManager instance = null;
    public static HorseMonsterManager Instance
    {
        get
        {
            if (instance == null)
                instance = FindObjectOfType<HorseMonsterManager>();
            return instance;
        }
    }
    public override void ResponMonsters()
    {
        if (positionIndex >= responPos.Count)
            positionIndex = 0;

        // Debug.Log(Instance.Objects[currentIndex].name + "spone");

        for (int i = 0; i < ObjectCount; i++)
        {
            if (Instance.Objects[i].GetComponent<HorseSoldier>().monsterInfo.state == MonsterState.Dead || Instance.Objects[i].activeSelf == false)
            {
                Instance.Objects[i].SetActive(true);
                Instance.Objects[i].transform.position = responPos[positionIndex];
                if (Instance.Objects[i].GetComponent<HorseSoldier>().Position.Count != 0)
                    Instance.Objects[i].transform.position = Instance.Objects[i].GetComponent<HorseSoldier>().Position[0];
                Instance.Objects[i].GetComponent<HorseSoldier>().Reset();
                break;
            }
        }
        positionIndex++;
    }
}
