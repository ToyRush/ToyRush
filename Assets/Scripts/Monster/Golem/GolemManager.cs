using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolemManager : MonsterManager
{
    private static GolemManager instance = null;
    public static GolemManager Instance
    {
        get
        {
            if (instance == null)
                instance = FindObjectOfType<GolemManager>();
            return instance;
        }
    }
    public override void ResponMonsters()
    {
        for (int i = 0; i < ObjectCount; i++)
        {
            if (Instance.Objects[i].GetComponent<Golem>().monsterInfo.state == MonsterState.Dead || Instance.Objects[i].activeSelf == false)
            {
                Instance.Objects[i].SetActive(true);
                if (Instance.Objects[i].GetComponent<Golem>().Position.Count != 0)
                    Instance.Objects[i].transform.position = Instance.Objects[i].GetComponent<Golem>().Position[0];
                Instance.Objects[i].GetComponent<Golem>().Reset();
                break;
            }
        }
    }
}
