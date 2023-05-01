using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
    public int bulletNum = 0;
    SpriteRenderer spr;

    void Awake()
    {
        spr = GetComponent<SpriteRenderer>();
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
        if (gunAngle <=90 && gunAngle > -90)
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
            Transform bullet = GameManager.instance.poolManager.Get(bulletNum).transform;
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

}
