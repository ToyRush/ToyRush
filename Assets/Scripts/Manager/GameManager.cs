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
    Door door;
    TimerUI timerUI;
    GameObject player;
    GameObject key;
    // ������ ������
    int health=5;
    int shiledCnt=0;
    [SerializeField] GameObject uiObjects;
    [SerializeField] GameObject gameoverUI;

    // �������� ����
    private int stageID=1;
    private int[] stageKeyCnt = new int[] {1, 1, 10, 10, 1,10,1};
    private int[] stageKeyID= new int[] {0,0,400,401,0,402,0};
    bool isGameOver = false;
    
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
        key = GameObject.FindGameObjectWithTag("Key");
        keyUI = key.GetComponent<KeyUI>();
        timerUI = GetComponentInChildren<TimerUI>();
        //GetComponentInChildren�� �ڽ� ������Ʈ�� �޸� ù��° ������Ʈ�� �ҷ��´�.
        //GetComponentsInChildren�� �ڽ� ������Ʈ�� �޸� �ش�Ǵ� ��� ������Ʈ���� �迭�� �ҷ��´�.
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
            OpenDoor();
        if (isGameOver)
            if (Input.anyKeyDown)
                ReStart();
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
    public int GetStageID()
    {
        return stageID;
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
        RegisterTimer();
        playerStat = _playerStat;
        if (stageID == 6) // ������ ���������� �÷����� �������� ����
        {
            PlayerMove playerMove;
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            playerMove = player.GetComponent<PlayerMove>();
            playerMove.bossStage = true;
            playerMove.SetGravity();
        }
    }
    public void RegisterDoorState(Door _door)
    {
        door = _door;
    }

    public void RegisterTimer()
    {
        timerUI.ResetTimer(stageID);
    }

    public void OpenDoor()
    {
        door.Open();
    }

    public void TimeOver()
    {
        if (!GameManager.instance.door.CheckDoor())
            keyUI.LostGauge();
    }

    public void GameOver()
    {
        gameoverUI.SetActive(true);
        uiObjects.SetActive(false);
        isGameOver = true;
    }

    public void ReStart()
    {
        Destroy(this.gameObject);
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }
}
