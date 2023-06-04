using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterManager : MonoBehaviour
{
    public List<GameObject> Objects;
    public int ObjectCount;
   
    private void Awake()
    {
        ObjectCount = transform.childCount;
        FindObjects();
    }
    public void FindObjects()
    {
        for (int i = 0; i < ObjectCount; i++)
            Objects.Add(transform.GetChild(i).gameObject);
    }

    public GameObject GetUnAtiveObject()
    {
        for (int i = 0; i <ObjectCount; i++)
        {
            if (Objects[i].gameObject.activeSelf == false)
                return Objects[i].gameObject;
        }
        return null;
    }
    public virtual void ResponMonsters()
    {

    }
}
