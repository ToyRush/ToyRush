using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Bullets;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField] GameObject rightGun;
    [SerializeField] GameObject leftGun;

    float gunAngle;
    Vector3 mousePos;
    Direction direction;
    [SerializeField] int bulletID = 0;
    int bulletCnt = 0;
    
    bool canShoot;

    [SerializeField] Text currentBulletCnt;
    SpriteRenderer spr;

    void Awake()
    {
        spr = GetComponent<SpriteRenderer>();
        SetBullet(GameManager.instance.weaponManager.bulletID, GameManager.instance.weaponManager.bulletCnt);
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
            WeaponRotate();
            Flip();
        }
    }

    void CheckMousePos() // ���� ���콺�� ��ġ�� �޾ƿ´�.
    {
        mousePos = Input.mousePosition;
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);
    }

    void WeaponRotate() // �÷��̾ �ѱ��� �ܴ��� �������� ȸ���Ѵ�.
    {
        gunAngle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;
        rightGun.transform.rotation = Quaternion.Euler(new Vector3(0, 0, gunAngle));
        leftGun.transform.rotation = Quaternion.Euler(new Vector3(0, 0, gunAngle));
        if (gunAngle <= 90 && gunAngle > -90)
        {
            rightGun.SetActive(true);
            leftGun.SetActive(false);
            direction = Direction.Right;
        }
        else
        {
            rightGun.SetActive(false);
            leftGun.SetActive(true);
            direction = Direction.Left;
        }
    }

    void ShootBullet() // �Ѿ��� ID�� �޾ƿͼ� ���. 
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 dir = mousePos - transform.position;
            Transform bullet = GameManager.instance.poolManager.GetBullet(bulletID).transform;
            switch (direction)
            {
                case Direction.Left:
                    bullet.position = leftGun.transform.position;
                    break;
                case Direction.Right:
                    bullet.position = rightGun.transform.position;
                    break;
            }
            dir.z = 0f;
            dir = dir.normalized;
            bullet.GetComponent<Bullet>().Init(dir);
            bulletCnt -= 1;
            SetBulletCntText();
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


    void SetBulletCntText() // ���� �Ѿ��� ������ �ҷ��´�.
    {
        if (bulletID == 0 || bulletCnt==0)
            currentBulletCnt.text = "��";
        else
            currentBulletCnt.text = bulletCnt.ToString();
    }

    public void SetBullet(int _bulletID, int _bulletCnt) // WeaponManager���� ���� ��� �ִ� �Ѿ˰� �Ѿ��� ������ �޾ƿ´�.
    {
        bulletID = _bulletID;
        bulletCnt = _bulletCnt;
        SetBulletCntText();
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

    public void LoadBullet() // �Ѿ��� ID�� ������ �����Ѵ�.
    {
        GameManager.instance.weaponManager.bulletID = bulletID+100;
        GameManager.instance.weaponManager.bulletCnt = bulletCnt;
    }
}
