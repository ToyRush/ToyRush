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
        mousePos = Cursor.instance.mousePos;
        if (gameObject.transform.position.x > mousePos.x)
            direction = Direction.Left;
        else
            direction = Direction.Right;
    }

    void SetUpTrap() // �Ѿ��� ID�� �޾ƿͼ� ���. 
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 dir = mousePos - transform.position;
            Transform trap = GameManager.instance.poolManager.GetTrap(trapID).transform;
            dir = dir.normalized;
        }
    }

    void Flip() // �÷��̾��� ��,�� ������ �����Ų��.
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
    public void SetTrap(int _trapID, int _trapCnt) // WeaponManager���� ���� ��� �ִ� �Ѿ˰� �Ѿ��� ������ �޾ƿ´�.
    {
        trapID = _trapID;
        trapCnt = _trapCnt;
    }

    public void LoadTrap(int _trapID) // Ʈ�� ���̵� �ҷ��´�.
    {
        trapID = _trapID;
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


}
