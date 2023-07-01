using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterManager : MonoBehaviour
{
    public List<GameObject> Objects;
    public GameObject sponeMonster;

    public int ObjectCount;
    public int positionIndex;
    public float ResoneTime;
    public float currentTime;
    public bool bSpone;

    protected void Start()
    {
        ResoneTime = 3.0f;
        currentTime = 0;
        bSpone = false;
        positionIndex = 0;
        ObjectCount = transform.childCount;
        FindObjects();
    }
    private void FixedUpdate()
    {
        if (bSpone)
        {
            currentTime += Time.fixedDeltaTime;
            if (currentTime > ResoneTime)
            {
                currentTime = 0;
                ResponMonsters();
            }
        }
    }
    public void FindObjects()
    {
        ObjectCount = transform.childCount;
        for (int i = 0; i < ObjectCount; i++)
            Objects.Add(transform.GetChild(i).gameObject);
    }
    public virtual GameObject GetUnAtiveObject()
    {
        ObjectCount = transform.childCount;
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
