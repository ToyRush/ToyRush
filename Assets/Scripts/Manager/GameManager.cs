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

    // 스테이지 정보
    private int stageID=1;
    private int[] stageKeyCnt = new int[] {1, 1, 10, 10, 1,10,1};
    private int[] stageKeyID= new int[] {0,0,400,401,0,402,0};
    
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
        timerUI = GetComponentInChildren<TimerUI>();
        //GetComponentInChildren는 자식 오브젝트에 달린 첫번째 컴포넌트를 불러온다.
        //GetComponentsInChildren는 자식 오브젝트에 달린 해당되는 모든 컴포넌트들을 배열로 불러온다.
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
            door.Open();
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

    public void OpenDoor()
    {
        door.Open();
    }

    public void RegisterPlayerStat(PlayerStat _playerStat) // 플레이어의 스탯 스크립트를 받아온다.
    {
        RegisterTimer();
        playerStat = _playerStat;
    }
    public void RegisterDoorState(Door _door)
    {
        door = _door;
    }

    public void RegisterTimer()
    {
        timerUI.ResetTimer(stageID);
    }

    public void TimeOver()
    {
        if (!GameManager.instance.door.CheckDoor())
            keyUI.LostGauge();
    }
}
