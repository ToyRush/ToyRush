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
    public GameObject Golem;
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
                Instance.Objects[i].GetComponent<Golem>().Reset();
                Instance.Objects[i].transform.position = responPos[positionIndex++];
                Instance.Objects[i].GetComponent<Golem>().Position.Clear();
                Instance.Objects[i].GetComponent<Golem>().Position.Add(GameObject.FindWithTag("Player").transform.position);
                if (positionIndex > responPos.Count)
                    positionIndex = 0;
                responCount--;

            }
        }
        for (int i = 0; i < responCount; i++)
        {
            GameObject monster = Instantiate(Golem, responPos[positionIndex++], Quaternion.identity, Instance.gameObject.transform);
            if (positionIndex > responPos.Count)
                positionIndex = 0;
            responCount--;
            monster.SetActive(true);
            monster.GetComponent<Golem>().Reset();
            monster.transform.position = responPos[positionIndex++];
            monster.GetComponent<Golem>().Position.Clear();
            monster.GetComponent<Golem>().Position.Add(GameObject.FindWithTag("Player").transform.position);
        }
    }
}
