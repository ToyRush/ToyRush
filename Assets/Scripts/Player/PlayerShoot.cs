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

    void Update() // 마우스의 입력과 총알을 쏜다.
    {
        if (canShoot)
        {
            CheckMousePos();
            ShootBullet();
        }
    }

    void FixedUpdate() // 총의 방향과 각도 조절한다.
    {
        if (canShoot)
        {
            WeaponRotate();
            Flip();
        }
    }

    void CheckMousePos() // 현재 마우스의 위치를 받아온다.
    {
        mousePos = Input.mousePosition;
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);
    }

    void WeaponRotate() // 플레이어가 총구를 겨누는 방향으로 회전한다.
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

    void ShootBullet() // 총알의 ID를 받아와서 쏜더. 
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


    void SetBulletCntText() // 현재 총알의 개수를 불러온다.
    {
        if (bulletID == 0 || bulletCnt==0)
            currentBulletCnt.text = "∞";
        else
            currentBulletCnt.text = bulletCnt.ToString();
    }

    public void SetBullet(int _bulletID, int _bulletCnt) // WeaponManager에서 현재 들고 있는 총알과 총알의 개수를 받아온다.
    {
        bulletID = _bulletID;
        bulletCnt = _bulletCnt;
        SetBulletCntText();
    }

    public void ControlGun(bool _canShoot) // WeaponManager에서 1번,2번키를 누를때마다 쏠 수 있는지 불러온다.
    {
        canShoot = _canShoot;
        if (!canShoot)
        {
            rightGun.SetActive(canShoot);
            leftGun.SetActive(canShoot);
        }
    }

    void CheckHoldWeapon() // 처음에 플레이어가 어떤 무기를 들고있는지 확인한다.
    {
        if (GameManager.instance.weaponManager.currentKey == 1)
            canShoot = true;
        else
            canShoot = false;
    }

    public void LoadBullet() // 총알의 ID와 개수를 저장한다.
    {
        GameManager.instance.weaponManager.bulletID = bulletID+100;
        GameManager.instance.weaponManager.bulletCnt = bulletCnt;
    }
}
