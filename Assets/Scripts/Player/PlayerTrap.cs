using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerTrap : MonoBehaviour
{
    bool canTrap; // Ʈ���� ��� �ִ� ���
    bool setTrap; // Ʈ���� ��ġ ������ ���, Ŀ�� UI�� �ʷϻ��� ��쿡�� ��ġ ����
    Vector3 mousePos;
    Direction direction;

    [SerializeField] Text currentTrapText;
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
        mousePos = Cursor.cursorInstance.GetMousePos();
        if (gameObject.transform.position.x > mousePos.x)
            direction = Direction.Left;
        else
            direction = Direction.Right;
        setTrap = Cursor.cursorInstance.CanSetTrap();
    }

    void SetUpTrap() // �Ѿ��� ID�� �޾ƿͼ� ���. 
    {
        if (Input.GetMouseButtonDown(0) && setTrap)
        {
            Transform trap = GameManager.instance.poolManager.GetTrap(trapID).transform;
            trap.transform.position = Cursor.cursorInstance.transform.position;
            GameManager.instance.weaponManager.SetUpTrap();
            Cursor.cursorInstance.AddTrapPos(trap.transform.position);
        }
    }

    void Flip() // �÷��̾��� ��,�� ������ �����Ų��.
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

    public void LoadTrap(int _trapID) // Ʈ�� ���̵� �ҷ��´�.
    {
        trapID = _trapID-200;
    }

    public void ControlTrap(bool _canShoot) // WeaponManager���� 1��,2��Ű�� ���������� �� �� �ִ��� �ҷ��´�.
    {
        canTrap = _canShoot;
    }

    void CheckHoldWeapon() // ó���� �÷��̾ � ���⸦ ����ִ��� Ȯ���Ѵ�.
    {
        if (GameManager.instance.weaponManager.currentKey == 1)
            canTrap = false;
        else
            canTrap = true;
    }
}
