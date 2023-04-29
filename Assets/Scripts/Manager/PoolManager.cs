using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    public GameObject[] bulletPrefabs;
    List<GameObject>[] bulletPools;
    private void Awake()
    {
        bulletPools = new List<GameObject>[bulletPrefabs.Length];
        for(int i=0; i<bulletPools.Length; i++)
        {
            bulletPools[i] = new List<GameObject>();
        }
    }

    public GameObject Get(int idx)
    {
        GameObject select = null;
        foreach(GameObject bullet in bulletPools[idx])
        {
            if (!bullet.activeSelf)
            {
                select = bullet;
                select.SetActive(true);
                break;
            }
        }
        if (!select)
        {
            select = Instantiate(bulletPrefabs[idx], transform);
            bulletPools[idx].Add(select);
        }
        return select;
    }
}
