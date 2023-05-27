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
    // 저장할 데이터
    int health=5;
    int shiledCnt=0;
    [SerializeField] GameObject uiObjects;
    [SerializeField] GameObject gameoverUI;

    // 스테이지 정보
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
        //GetComponentInChildren는 자식 오브젝트에 달린 첫번째 컴포넌트를 불러온다.
        //GetComponentsInChildren는 자식 오브젝트에 달린 해당되는 모든 컴포넌트들을 배열로 불러온다.
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
            OpenDoor();
        if (isGameOver)
            if (Input.anyKeyDown)
                ReStart();
    }
    // 스테이지 정보 관련 함수
    public void NextStage(int _stageID) // 다음 스테이지 전환 시, key 정보 전달한다.
    {
        stageID = _stageID;
        keyUI.ChangeKeyInfo();
    }
    public int GetStageKeyID() // 키 아이디 반환한다.
    {
        return stageKeyID[stageID];
    }

    public int GetStageKeyCnt() // 필요한 키의 개수 반환한다.
    {
        return stageKeyCnt[stageID];
    }
    public int GetStageID()
    {
        return stageID;
    }

    public void PickUpKey(int _stageKeyID) // 습득한 키를 전달한다.
    {
        keyUI.ChargeGauge(_stageKeyID);
    }


    // HP 정보 관련
    public void SetHealth(int _health) // 현재 HP 정보를 받아온다.
    {
        health = _health;
    }

    public int GetHealth() // 현재 HP 정보를 반환한다.
    {
        return health;
    }

    // 쉴드 정보 관련
    public void SetShield(int _shieldCnt) // 현재 쉴드의 개수를 받아온다.
    {
        shiledCnt = _shieldCnt;
    }

    public int GetShield() // 현재 쉴드의 개수를 반환한다.
    {
        return shiledCnt;
    }


    public void RegisterPlayerStat(PlayerStat _playerStat) // 플레이어의 스탯 스크립트를 받아온다.
    {
        RegisterTimer();
        playerStat = _playerStat;
        if (stageID == 6) // 마지막 스테이지면 플래포머 형식으로 변경
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
