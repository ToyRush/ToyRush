using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    static public GameManager instance =null;
    public PoolManager poolManager;
    public WeaponManager weaponManager;
    public PlayerStat playerStat;
    // ������ ������
    float health;

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
        weaponManager= GetComponentInChildren<WeaponManager>();
        //GetComponentInChildren�� �ڽ� ������Ʈ�� �޸� ù��° ������Ʈ�� �ҷ��´�.
        //GetComponentsInChildren�� �ڽ� ������Ʈ�� �޸� �ش�Ǵ� ��� ������Ʈ���� �迭�� �ҷ��´�.
    }


    public void SetHealth(float _health)
    {
        health = _health;
    }
}
