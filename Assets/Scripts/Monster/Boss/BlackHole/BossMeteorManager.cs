using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMeteorManager : MonsterManager
{
    private static BossMeteorManager instance = null;
    public static BossMeteorManager Instance
    {
        get
        {
            if (instance == null)
                instance = FindObjectOfType<BossMeteorManager>();
            return instance;
        }
    }
    public void ResponMeteor()
    {
        int startindex = Random.Range(1, Instance.ObjectCount );
        for (int i = startindex + 1; i != startindex; i++)
        {
            if (i >= Instance.ObjectCount)
                i = 0;
            if (Instance.Objects[i].activeSelf == false)
            {
                Instance.Objects[i].SetActive(true);
                Instance.Objects[i].GetComponent<BossMeteor>().PlayPartical();
                break;
            }
           
        }
    }
}