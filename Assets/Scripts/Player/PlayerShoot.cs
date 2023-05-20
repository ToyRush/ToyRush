using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField] GameObject rightGun;
    [SerializeField] GameObject leftGun;
    [SerializeField] GameObject rightPos;
    [SerializeField] GameObject leftPos;

    float gunAngle;
    Vector3 mousePos;
    Direction direction;
    int bulletID = 100;
    bool canShoot;
    bool isCoolDown = false;
    SpriteRenderer spr;
    
    WaitForSeconds shootDelay;
    float loadBullet;

    void Start()
    {
        spr = GetComponent<SpriteRenderer>();
        GameManager.instance.weaponManager.RegisterShoot(this);
        LoadBullet(GameManager.instance.weaponManager.bulletID);
        CheckHoldWeapon();
    }

    void Update() // ���콺�� �Է°� �Ѿ��� ���.
    {
     
        if (canShoot)
        {
            CheckMousePos();
            ShootBullet();
        }
    }

    void FixedUpdate() // ���� ����� ���� �����Ѵ�.
    {
        if (canShoot)
        {
            GunRotate();
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
    }

    void GunRotate() // �÷��̾ �ѱ��� �ܴ��� �������� ȸ���Ѵ�.
    {
        Vector3 mouseDir = mousePos - transform.position;
        gunAngle = Mathf.Atan2(mouseDir.y, mouseDir.x) * Mathf.Rad2Deg;
        switch (direction)
        {
            case Direction.Left:
                leftGun.SetActive(true);
                rightGun.SetActive(false);
                leftGun.transform.rotation = Quaternion.Euler(new Vector3(0, 0, gunAngle));
                break;
            case Direction.Right:
                leftGun.SetActive(false);
                rightGun.SetActive(true);
                rightGun.transform.rotation = Quaternion.Euler(new Vector3(0, 0, gunAngle));
                break;
        }
    }

    void ShootBullet() // �Ѿ��� ID�� �޾ƿͼ� ���. 
    {
        if (Input.GetMouseButtonDown(0) && !isCoolDown)
        {
            Vector3 dir = mousePos - transform.position;
            dir = dir.normalized;
            Transform bullet = GameManager.instance.poolManager.GetBullet(bulletID).transform;
            switch (direction)
            {
                case Direction.Left:
                    bullet.position = leftPos.transform.position;
                    break;
                case Direction.Right:
                    bullet.position = rightPos.transform.position;
                    break;
            }
            bullet.GetComponent<Bullet>().Init(dir);
            GameManager.instance.weaponManager.ShootGun();
            isCoolDown = true;
            Invoke("CoolDown", 0.2f);
        }
    }

    void Flip() // �÷��̾��� ��,�� ����
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

    public void LoadBullet(int _bulletID)  // WeaponManager���� ���� ��� �ִ� �Ѿ��� ID�� �޾ƿ´�.
    {
        bulletID = _bulletID - 100;
    }

    public void ControlGun(bool _canShoot) // WeaponManager���� 1��,2��Ű�� ���������� �� �� �ִ��� �ҷ��´�.
    {
        canShoot = _canShoot;
        if (!canShoot)
        {
            rightGun.SetActive(canShoot);
            leftGun.SetActive(canShoot);
        }
    }

    void CheckHoldWeapon() // ó���� �÷��̾ � ���⸦ ����ִ��� Ȯ���Ѵ�.
    {
        if (GameManager.instance.weaponManager.currentKey == 1)
            canShoot = true;
        else
            canShoot = false;
    }

    void CoolDown()
    {
        isCoolDown = false;
    }
}
