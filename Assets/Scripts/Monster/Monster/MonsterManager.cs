using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterManager : MonoBehaviour
{
    public List<GameObject> Objects;
    public List<Vector3> responPos;
    public GameObject sponeMonster;

    public int ObjectCount;
    public int currentIndex;
    public int responCount;
    public  int positionIndex;
    public bool bSpone;

    protected void Start()
    {
        bSpone = false;
        currentIndex = 0;
        positionIndex = 0;
        responCount = 0;
        ObjectCount = transform.childCount;
        FindObjects();
    }
    private void FixedUpdate()
    {
        if (bSpone)
            ResponMonsters();
    }
    public void FindObjects()
    {
        for (int i = 0; i < ObjectCount; i++)
            Objects.Add(transform.GetChild(i).gameObject);
    }
    public virtual GameObject GetUnAtiveObject()
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

    private void OnDrawGizmos()
    {
        if (responPos.Count > 0)
        {
            for(int i = 0; i < responPos.Count; i ++)
            {
                Gizmos.color = Color.blue;
                Gizmos.DrawCube(responPos[i], new Vector3(2, 2, 2));
            }
        }
    }
}
