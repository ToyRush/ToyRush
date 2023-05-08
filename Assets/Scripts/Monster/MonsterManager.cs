using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 일단은 sysyem 에서 monster 을 검색해서 patrol 위치 지정해주는 class
public class MonsterManager : MonoBehaviour
{
    private static MonsterManager instance = null;
 
    public static MonsterManager Instance
    {
        get
        {
            if (instance == null)
            {
                return null;
            }
            return instance;
        }
    }
    private GameObject[] monsters;
    private List<Vector2[]> Position;
    private int[] indexs;
    private void Awake()
    {
        if (instance == null)
            instance = this;
        monsters = GameObject.FindGameObjectsWithTag("Monster");
        DontDestroyOnLoad(gameObject);
    }
    void Start()
    {
        Position = new List<Vector2[]> {
            new Vector2[] { new Vector2(-6, 10), new Vector2(-3, 10), new Vector2(0, 10), 
                            new Vector2(3, 10), new Vector2(6, 10) },
            new Vector2[] { new Vector2(-10,-6), new Vector2(-10,-3),new Vector2(-10,0),new Vector2(-10, 3),
                            new Vector2(-10,6) }

        };
        indexs = new int[2] {0,0};

    }

    public Vector2 GetNextPos(GameObject monster)
    {
        Vector2 result = monster.transform.position;
        for (int i = 0; i < monsters.Length; i++)
        {
            if (monsters[i] == monster)
            {
                result =  Position[i][indexs[i]++];
                if (indexs[i] >= Position[i].Length)
                    indexs[i] = 0;
                break;
            }
        }
        return result;
    }

}
