using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorseManager : MonsterManager
{
    private static HorseManager instance = null;
    public bool hasPlayer;

    public static HorseManager Instance
    {
        get
        {
            if (instance == null)
                instance = FindObjectOfType<HorseManager>();
            instance.hasPlayer = false;
            return instance;
        }
    }
    public override void ResponMonsters()
    {
        ObjectCount = transform.childCount;
        if (positionIndex >= responPos.Count)
            positionIndex = 0;

        // Debug.Log(Instance.Objects[currentIndex].name + "spone");

        for (int i = 0; i < ObjectCount; i++)
        {
            if (Instance.Objects[i].GetComponent<HorseSoldier>().monsterInfo.state == MonsterState.Dead)
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
