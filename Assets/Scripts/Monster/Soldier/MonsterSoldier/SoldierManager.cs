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
        if (responCount <= 0)
            return;
        if (currentIndex >= Instance.ObjectCount)
            currentIndex = 0;
        if (Instance.Objects[currentIndex].GetComponent<Soldier>().monsterInfo.state == MonsterState.Dead)
        {
           // Debug.Log(Instance.Objects[currentIndex].name + "spone");
            Instance.Objects[currentIndex].SetActive(true);
            Instance.Objects[currentIndex].transform.position = responPos[positionIndex++];
            Instance.Objects[currentIndex].GetComponent<Soldier>().Reset();
            //Instance.Objects[currentIndex].GetComponent<Soldier>().Position.Clear();
            //Instance.Objects[currentIndex].GetComponent<Soldier>().Position.Add(GameObject.FindWithTag("Player").transform.position);
            if (positionIndex >= responPos.Count)
                positionIndex = 0;
            responCount--;
        }
        currentIndex++;
        //}
        //for (int i = 0; i < responCount; i++)
        //{
        //    GameObject monster =  Instantiate(solider, responPos[positionIndex++], Quaternion.identity, Instance.gameObject.transform);
        //    if (positionIndex > responPos.Count)
        //        positionIndex = 0;
        //    responCount--;
        //    monster.SetActive(true);
        //    monster.GetComponent<Soldier>().Reset();
        //    monster.transform.position = responPos[positionIndex++];
        //    monster.GetComponent<Soldier>().Position.Clear();
        //    monster.GetComponent<Soldier>().Position.Add(GameObject.FindWithTag("Player").transform.position);
        //}
    }
}
