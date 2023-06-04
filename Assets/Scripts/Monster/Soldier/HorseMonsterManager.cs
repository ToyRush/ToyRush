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
    public GameObject solider;
    public List<Vector3> responPos;
    public int responCount = 0;
    public override void ResponMonsters()
    {
        int positionIndex = 0;
        for (int i = 0; i < Instance.ObjectCount && responCount > 0; i++)
        {
            if (Instance.Objects[i].activeSelf == false)
            {
                Instance.Objects[i].SetActive(true);
                Instance.Objects[i].GetComponent<HorseSoldier>().Reset();
                Instance.Objects[i].transform.position = responPos[positionIndex++];
                Instance.Objects[i].GetComponent<HorseSoldier>().Position.Clear();
                Instance.Objects[i].GetComponent<HorseSoldier>().Position.Add(GameObject.FindWithTag("Player").transform.position);
                if (positionIndex > responPos.Count)
                    positionIndex = 0;
                responCount--;

            }
        }
        for (int i = 0; i < responCount; i++)
        {
            GameObject monster =  Instantiate(solider, responPos[positionIndex++], Quaternion.identity, Instance.gameObject.transform);
            if (positionIndex > responPos.Count)
                positionIndex = 0;
            responCount--;
            monster.SetActive(true);
            monster.GetComponent<HorseSoldier>().Reset();
            monster.transform.position = responPos[positionIndex++];
            monster.GetComponent<HorseSoldier>().Position.Clear();
            monster.GetComponent<HorseSoldier>().Position.Add(GameObject.FindWithTag("Player").transform.position);
        }
    }
}