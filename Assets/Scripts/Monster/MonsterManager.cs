using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �ϴ��� sysyem ���� monster �� �˻��ؼ� patrol ��ġ �������ִ� class
public class MonsterManager : MonoBehaviour
{
    private GameObject[] monsters;
    private List<Vector2[]> Position;
    private void Awake()
    {
        monsters = GameObject.FindGameObjectsWithTag("Monster");
    }
    void Start()
    {
        Position = new List<Vector2[]> {
            new Vector2[] { new Vector2(-6, 10), new Vector2(-3, 10), new Vector2(0, 10), 
                            new Vector2(3, 10), new Vector2(6, 10) },
            new Vector2[] { new Vector2(-10,-6), new Vector2(-10,-3),new Vector2(-10,0),new Vector2(-10, 3),
                            new Vector2(-10,6) }

        };


        for(int i = 0; i < Position.Count; i++)
        {
            if (i < monsters.Length)
            {
                monsters[i].GetComponent<Moving>().setFixedPos(Position[i]);
            }
            else
                break;
        }

    }
}
