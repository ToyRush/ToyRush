using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Bullets;

public enum Direction
{
    Left,
    Right
}

public class PlayerShoot : MonoBehaviour
{
    [SerializeField] GameObject rightGun;
    [SerializeField] GameObject leftGun;

    float gunAngle;
    Vector3 mousePos;
    Direction direction;
    [SerializeField] int bulletID = 0;
    int bulletCnt = 0;

    [SerializeField] Text currentBulletCnt;
    SpriteRenderer spr;

    void Awake()
    {
        spr = GetComponent<SpriteRenderer>();
        SetBullet(GameManager.instance.bulletID, GameManager.instance.bulletCnt);
    }

    void Update()
    {
        CheckMousePos();
        ShootBullet();
    }

    void FixedUpdate()
    {
        WeaponRotate();
        Flip();
    }

    void CheckMousePos()
    {
        mousePos = Input.mousePosition;
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);
    }

    void WeaponRotate()
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

    void ShootBullet()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 dir = mousePos - transform.position;
            Transform bullet = GameManager.instance.poolManager.Get(bulletID).transform;
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

    void Flip()
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


    void SetBulletCntText()
    {
        if (bulletID == 0 || bulletCnt==0)
            currentBulletCnt.text = "¡Ä";
        else
            currentBulletCnt.text = bulletCnt.ToString();
    }

    public int  CheckBulletID()
    {
        return bulletID;
    }

    public void SetBullet(int _bulletID, int _bulletCnt)
    {
        bulletID = _bulletID;
        bulletCnt = _bulletCnt;
        SetBulletCntText();
    }
}
