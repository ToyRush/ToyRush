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

    void Update() // ���콺�� �Է°� �Ѿ��� ���.
    {
        if (canTrap)
        {
            CheckMousePos();
            SetUpTrap();
        }
    }

    void FixedUpdate() // ���� ����� ���� �����Ѵ�.
    {
        if (canTrap)
        {
            Flip();
        }
    }

    void CheckMousePos() // ���� ���콺�� ��ġ�� �޾ƿ´�.
    {
        mousePos = Input.mousePosition;
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);
    }

  

    void SetUpTrap() // �Ѿ��� ID�� �޾ƿͼ� ���. 
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

    void Flip() // ���� ��,�� ������ �����Ų��.
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


    void SetTrapCntText() // ���� �Ѿ��� ������ �ҷ��´�.
    {
       /* if (bulletID == 0 || bulletCnt == 0)
            currentBulletCnt.text = "��";
        else
            currentBulletCnt.text = bulletCnt.ToString();*/
    }

    public void SetTrap(int _trapID, int _trapCnt) // WeaponManager���� ���� ��� �ִ� �Ѿ˰� �Ѿ��� ������ �޾ƿ´�.
    {
        trapID = _trapID;
        trapCnt = _trapCnt;
        SetTrapCntText();
    }

    public void ControlTrap(bool _canShoot) // WeaponManager���� 1��,2��Ű�� ���������� �� �� �ִ��� �ҷ��´�.
    {
        canTrap = _canShoot;
        if (!canTrap)
        {
            rightTrap.SetActive(canTrap);
            leftTrap.SetActive(canTrap);
        }
    }

    void CheckHoldWeapon() // ó���� �÷��̾ � ���⸦ ����ִ��� Ȯ���Ѵ�.
    {
        if (GameManager.instance.weaponManager.currentKey == 1)
            canTrap = false;
        else
            canTrap = true;
    }

    public void LoadTrap() // Ʈ���� ID�� ������ �����Ѵ�.
    {
        GameManager.instance.weaponManager.trapID = trapID + 200;
        GameManager.instance.weaponManager.trapCnt = trapCnt;
    }
}
