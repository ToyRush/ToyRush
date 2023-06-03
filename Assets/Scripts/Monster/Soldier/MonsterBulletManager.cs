using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterBulletManager : MonsterManager
{
    private static MonsterBulletManager instance = null;
    public static MonsterBulletManager Instance
    {
        get
        {
            if (instance == null)
                instance = FindObjectOfType<MonsterBulletManager>();
            return instance;
        }
    }
}