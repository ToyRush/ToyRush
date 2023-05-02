using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    static public GameManager instance =null;
    public PoolManager poolManager;
    public PlayerStat playerStat;
    public ItemDataBase itemDataBase;

    // 저장할 데이터
    float health;
    public int bulletCnt;
    public int bulletID;

    // 스테이지 정보
    public int stageID;
    public int[] stageKeyID;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else 
        {
            if(instance!=this)
                Destroy(this.gameObject);
        }
        itemDataBase = GetComponentInChildren<ItemDataBase>();
        poolManager = GetComponentInChildren<PoolManager>();
        //GetComponentInChildren는 자식 오브젝트에 달린 첫번째 컴포넌트를 불러온다.
        //GetComponentsInChildren는 자식 오브젝트에 달린 해당되는 모든 컴포넌트들을 배열로 불러온다.
    }

    public void SetHealth(float _health)
    {
        health = _health;
    }
}
