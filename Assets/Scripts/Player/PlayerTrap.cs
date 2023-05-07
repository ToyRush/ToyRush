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
        mousePos = Input.mousePosition;
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);
    }

  

    void SetUpTrap() // 총알의 ID를 받아와서 쏜더. 
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 dir = mousePos - transform.position;
            Transform trap = GameManager.instance.poolManager.GetTrap(trapID).transform;
            dir.z = 0f;
            dir = dir.normalized;
            SetTrapCntText();
        }
    }

    void Flip() // 총의 좌,우 반전을 적용시킨다.
    {
        switch (direction)
        {
            case Direction.Left:
                spr.flipX = true;
                break;
            case Direction.Right:
                spr.flipX = false;
                break;
        }
    }


    void SetTrapCntText() // 현재 총알의 개수를 불러온다.
    {
       /* if (bulletID == 0 || bulletCnt == 0)
            currentBulletCnt.text = "∞";
        else
            currentBulletCnt.text = bulletCnt.ToString();*/
    }

    public void SetTrap(int _trapID, int _trapCnt) // WeaponManager에서 현재 들고 있는 총알과 총알의 개수를 받아온다.
    {
        trapID = _trapID;
        trapCnt = _trapCnt;
        SetTrapCntText();
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

    public void LoadTrap() // 트랩의 ID와 개수를 저장한다.
    {
        GameManager.instance.weaponManager.trapID = trapID + 200;
        GameManager.instance.weaponManager.trapCnt = trapCnt;
    }
}
