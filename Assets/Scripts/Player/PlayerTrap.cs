using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerTrap : MonoBehaviour
{
    [SerializeField] GameObject rightTrap;
    [SerializeField] GameObject leftTrap;
    
    bool canTrap;
    Vector3 mousePos;
    Direction direction;

    [SerializeField] Text currentTrapText;
    SpriteRenderer spr;

    int trapID;
    int trapCnt;

    void Awake()
    {
        spr = GetComponent<SpriteRenderer>();
        SetTrap(GameManager.instance.weaponManager.trapID, GameManager.instance.weaponManager.trapCnt);
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
        mousePos = Cursor.instance.mousePos;
        if (gameObject.transform.position.x > mousePos.x)
            direction = Direction.Left;
        else
            direction = Direction.Right;
    }

    void SetUpTrap() // 총알의 ID를 받아와서 쏜더. 
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 dir = mousePos - transform.position;
            Transform trap = GameManager.instance.poolManager.GetTrap(trapID).transform;
            dir = dir.normalized;
        }
    }

    void Flip() // 플레이어의 좌,우 반전을 적용시킨다.
    {
        switch (direction)
        {
            case Direction.Left:
                leftTrap.SetActive(true);
                rightTrap.SetActive(false);
                spr.flipX = true;
                break;
            case Direction.Right:
                leftTrap.SetActive(false);
                rightTrap.SetActive(true);
                spr.flipX = false;
                break;
        }
    }
    public void SetTrap(int _trapID, int _trapCnt) // WeaponManager에서 현재 들고 있는 총알과 총알의 개수를 받아온다.
    {
        trapID = _trapID;
        trapCnt = _trapCnt;
    }

    public void LoadTrap(int _trapID) // 트랩 아이디를 불러온다.
    {
        trapID = _trapID;
    }

    public void ControlTrap(bool _canShoot) // WeaponManager에서 1번,2번키를 누를때마다 쏠 수 있는지 불러온다.
    {
        canTrap = _canShoot;
        if (!canTrap)
        {
            rightTrap.SetActive(canTrap);
            leftTrap.SetActive(canTrap);
        }
    }

    void CheckHoldWeapon() // 처음에 플레이어가 어떤 무기를 들고있는지 확인한다.
    {
        if (GameManager.instance.weaponManager.currentKey == 1)
            canTrap = false;
        else
            canTrap = true;
    }


}
