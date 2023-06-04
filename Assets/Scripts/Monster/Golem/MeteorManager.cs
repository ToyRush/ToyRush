using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorManager : MonsterManager
{
    private static MeteorManager instance = null;
    public static MeteorManager Instance
    {
        get
        {
            if (instance == null)
                instance = FindObjectOfType<MeteorManager>();
            return instance;
        }
    }
    public void ResponMeteor(Vector3 pos)
    {
        for (int i = 0; i < Instance.ObjectCount; i++)
        {
            if (Instance.Objects[i].activeSelf == false)
            {
                Instance.Objects[i].SetActive(true);
                Instance.Objects[i].GetComponent<Meteor>().PlayPartical();
                Instance.Objects[i].transform.position = pos;
                break;
            }
        }
    }
}
