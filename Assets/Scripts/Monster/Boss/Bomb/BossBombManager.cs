using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBombManager : MonsterManager
{
    public Vector3[] positions;
    public int maxBombCounts;
    public int attackcount;
    private static BossBombManager instance = null;
    public static BossBombManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<BossBombManager>();
                instance.maxBombCounts = 7;
                instance.attackcount = 0;
                instance.positions = new Vector3[Instance.transform.childCount];
                for (int i = 0; i < Instance.transform.childCount; i++)
                    instance.positions.SetValue(instance.transform.GetChild(i).gameObject.transform.position,i);
            }
            return instance;
        }
    }
    public void ResponMeteor()
    {
        if (instance.attackcount >= instance.maxBombCounts)
            return;
        int startindex = Random.Range(1, Instance.ObjectCount);
        for (int i = startindex + 1; i != startindex; i++)
        {
            if (i >= Instance.ObjectCount)
                i = 0;
            if (Instance.Objects[i] != null && Instance.Objects[i].activeSelf == false)
            {
                instance.attackcount++;
                Instance.Objects[i].SetActive(true);
                Instance.Objects[i].gameObject.transform.position = positions[i];
                break;
            }

        }
    }
}