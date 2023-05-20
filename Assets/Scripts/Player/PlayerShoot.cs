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
            GunRotate();
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
    }

    void GunRotate() // 플레이어가 총구를 겨누는 방향으로 회전한다.
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

    void ShootBullet() // 총알의 ID를 받아와서 쏜더. 
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

    void Flip() // 플레이어의 좌,우 반전
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

    public void LoadBullet(int _bulletID)  // WeaponManager에서 현재 들고 있는 총알의 ID를 받아온다.
    {
        bulletID = _bulletID - 100;
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

    void CoolDown()
    {
        isCoolDown = false;
    }
}
