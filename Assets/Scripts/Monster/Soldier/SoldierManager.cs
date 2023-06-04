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
                Instance.Objects[i].GetComponent<Soldier>().Reset();
                Instance.Objects[i].transform.position = responPos[positionIndex++];
                Instance.Objects[i].GetComponent<Soldier>().Position.Clear();
                Instance.Objects[i].GetComponent<Soldier>().Position.Add(GameObject.FindWithTag("Player").transform.position);
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
            monster.GetComponent<Soldier>().Reset();
            monster.transform.position = responPos[positionIndex++];
            monster.GetComponent<Soldier>().Position.Clear();
            monster.GetComponent<Soldier>().Position.Add(GameObject.FindWithTag("Player").transform.position);
        }
    }
}
