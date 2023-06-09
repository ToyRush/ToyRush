using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerTrap : MonoBehaviour
{
    bool canTrap; // 트랩을 들고 있는 경우
    bool setTrap; // 트랩을 설치 가능한 경우, 커서 UI가 초록색인 경우에만 설치 가능
    Vector3 mousePos;
    Direction direction;

    SpriteRenderer spr;

    int trapID;
    int trapCnt;

    void Start()
    {
        spr = GetComponent<SpriteRenderer>();
        GameManager.instance.weaponManager.RegisterTrap(this);
        LoadTrap(GameManager.instance.weaponManager.trapID);
        CheckHoldWeapon();

    }

    void Update() // 마우스의 입력과 총알을 쏜다.
    {
        if (canTrap)
        {
            CheckMousePos();
            SetUpTrap();
        }
    }

    void FixedUpdate() // 총의 방향과 각도 조절한다.
    {
        if (canTrap)
        {
            Flip();
        }
    }

    void CheckMousePos() // 현재 마우스의 위치를 받아온다.
    {
        mousePos = Cursor.cursorInstance.GetMousePos();
        if (gameObject.transform.position.x > mousePos.x)
            direction = Direction.Left;
        else
            direction = Direction.Right;
        setTrap = Cursor.cursorInstance.CanSetTrap();
    }

    void SetUpTrap() // 총알의 ID를 받아와서 쏜더. 
    {
        if (Input.GetMouseButtonDown(0) && setTrap)
        {
            Transform trap = GameManager.instance.poolManager.GetTrap(trapID).transform;
            trap.transform.position = Cursor.cursorInstance.transform.position;
            GameManager.instance.weaponManager.SetUpTrap();
            Cursor.cursorInstance.AddTrapPos(trap.transform.position);
        }
    }

    void Flip() // 플레이어의 좌,우 반전을 적용시킨다.
    {
        switch (direction)
        {
            case Direction.Left:
                spr.flipX = false;
                break;
            case Direction.Right:
                spr.flipX = true;
                break;
        }
    }

    public void LoadTrap(int _trapID) // 트랩 아이디를 불러온다.
    {
        trapID = _trapID-200;
    }

    public void ControlTrap(bool _canShoot) // WeaponManager에서 1번,2번키를 누를때마다 쏠 수 있는지 불러온다.
    {
        canTrap = _canShoot;
    }

    void CheckHoldWeapon() // 처음에 플레이어가 어떤 무기를 들고있는지 확인한다.
    {
        if (GameManager.instance.weaponManager.currentKey == 1)
            canTrap = false;
        else
            canTrap = true;
    }
}
