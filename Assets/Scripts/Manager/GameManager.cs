using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
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
    int health=5;
    int shiledCnt=0;

    // �������� ����
    private int stageID=0;
    private int[] stageKeyCnt = new int[] { 10, 15, 20, 25, 30,35,40};
    private int[] stageKeyID= new int[] {400,401,402,403,404,405};
    
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
        playerStat = player.GetComponent<PlayerStat>();
        key = GameObject.FindGameObjectWithTag("Key");
        keyUI = key.GetComponent<KeyUI>();
        //GetComponentInChildren�� �ڽ� ������Ʈ�� �޸� ù��° ������Ʈ�� �ҷ��´�.
        //GetComponentsInChildren�� �ڽ� ������Ʈ�� �޸� �ش�Ǵ� ��� ������Ʈ���� �迭�� �ҷ��´�.
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            NextStage(1);
            //SceneManager.LoadScene(1);
            SceneManager.LoadScene("FirstFloor");
        }
    }
    
    // �������� ���� ���� �Լ�
    public void NextStage(int _stageID) // ���� �������� ��ȯ ��, key ���� �����Ѵ�.
    {
        stageID = _stageID;
        keyUI.ChangeKeyInfo();
    }
    public int GetStageKeyID() // Ű ���̵� ��ȯ�Ѵ�.
    {
        return stageKeyID[stageID];
    }

    public int GetStageKeyCnt() // �ʿ��� Ű�� ���� ��ȯ�Ѵ�.
    {
        return stageKeyCnt[stageID];
    }

    public void PickUpKey(int _stageKeyID) // ������ Ű�� �����Ѵ�.
    {
        keyUI.ChargeGauge(_stageKeyID);
    }

    // HP ���� ����
    public void SetHealth(int _health) // ���� HP ������ �޾ƿ´�.
    {
        health = _health;
    }

    public int GetHealth() // ���� HP ������ ��ȯ�Ѵ�.
    {
        return health;
    }

    // ���� ���� ����
    public void SetShield(int _shieldCnt) // ���� ������ ������ �޾ƿ´�.
    {
        shiledCnt = _shieldCnt;
    }

    public int GetShield() // ���� ������ ������ ��ȯ�Ѵ�.
    {
        return shiledCnt;
    }

    public void RegisterPlayerStat(PlayerStat _playerStat) // �÷��̾��� ���� ��ũ��Ʈ�� �޾ƿ´�.
    {
        playerStat = _playerStat;
    }
}
