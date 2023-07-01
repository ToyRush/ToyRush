using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierManager : MonsterManager
{
    private static SoldierManager instance = null;
    public static SoldierManager Instance
    {
        get
        {
            if (instance == null)
                instance = FindObjectOfType<SoldierManager>();
            return instance;
        }
    }
    public override void ResponMonsters()
    {
        // Debug.Log(Instance.Objects[currentIndex].name + "spone");

        for (int i = 0; i < ObjectCount; i++)
        {
            if (Instance.Objects[i].GetComponent<Soldier>().monsterInfo.state == MonsterState.Dead || Instance.Objects[i].activeSelf == false)
            {
                Instance.Objects[i].SetActive(true);
                if (Instance.Objects[i].GetComponent<Soldier>().Position.Count != 0)
                    Instance.Objects[i].transform.position = Instance.Objects[i].GetComponent<Soldier>().Position[0];
                Instance.Objects[i].GetComponent<Soldier>().Reset();
                break;
            }
        }
    }
}
