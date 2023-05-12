using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    static public GameManager instance =null;
    public PoolManager poolManager;
    public WeaponManager weaponManager;
    PlayerStat playerStat;
    KeyUI keyUI;
    GameObject player;
    GameObject key;
    // ������ ������
    float health;

    // �������� ����
    private int stageID=0;
    private int[] stageKeyID= new int[] { 400,401,402,403,404,405};
    
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
        player = GameObject.FindGameObjectWithTag("Player");
        key = GameObject.FindGameObjectWithTag("Key");
        playerStat = player.GetComponent<PlayerStat>();
        keyUI = key.GetComponent<KeyUI>();
        //GetComponentInChildren�� �ڽ� ������Ʈ�� �޸� ù��° ������Ʈ�� �ҷ��´�.
        //GetComponentsInChildren�� �ڽ� ������Ʈ�� �޸� �ش�Ǵ� ��� ������Ʈ���� �迭�� �ҷ��´�.
    }

    
    public int GetStageKeyID()
    {
        return stageKeyID[stageID];
    }

    public void PickUpKey(int _stageKeyID)
    {
        keyUI.ChargeGauge(_stageKeyID);
    }

    public void ChangeStageID(int _stageID)
    {
        stageID = _stageID;
    }

    public void SetHealth(float _health)
    {
        health = _health;
    }
}
