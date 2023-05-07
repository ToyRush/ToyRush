using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    public GameObject[] bulletPrefabs;
    public GameObject[] trapPrefabs;

    List<GameObject>[] bulletPools;
    List<GameObject>[] trapPools;

    private void Awake()
    {
        bulletPools = new List<GameObject>[bulletPrefabs.Length];
        trapPools = new List<GameObject>[trapPrefabs.Length];

        for(int i=0; i<bulletPools.Length; i++)
        {
            bulletPools[i] = new List<GameObject>();
        }

        for(int i=0; i<trapPools.Length; i++)
        {
            trapPools[i] = new List<GameObject>();
        }
    }

    public GameObject GetBullet(int idx)
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
    public GameObject GetTrap(int idx)
    {
        GameObject select = null;
        foreach (GameObject trap in trapPools[idx])
        {
            if (!trap.activeSelf)
            {
                select = trap;
                select.SetActive(true);
                break;
            }
        }
        if (!select)
        {
            select = Instantiate(trapPrefabs[idx], transform);
            trapPools[idx].Add(select);
        }
        return select;
    }
}
