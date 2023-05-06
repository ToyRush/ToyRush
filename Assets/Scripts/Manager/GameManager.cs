using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    static public GameManager instance =null;
    public PoolManager poolManager;
    public PlayerStat playerStat;
    public PlayerShoot playerShoot;

    // ������ ������
    float health;
    public int bulletCnt=0;
    public int bulletID=0;

    // �������� ����
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
        poolManager = GetComponentInChildren<PoolManager>();
        //GetComponentInChildren�� �ڽ� ������Ʈ�� �޸� ù��° ������Ʈ�� �ҷ��´�.
        //GetComponentsInChildren�� �ڽ� ������Ʈ�� �޸� �ش�Ǵ� ��� ������Ʈ���� �迭�� �ҷ��´�.
    }

    public void SetHealth(float _health)
    {
        health = _health;
    }

    public void UpdateBullet(int id, int cnt)
    {
        bulletID = id-100;
        playerShoot.SetBullet(id-100, cnt);
    }
}
