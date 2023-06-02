using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossLaser : MonoBehaviour
{
    Laser laser;
    LaserEnd laserEnd;
    LaserStart laserStart;
    LaserCasting laserCasting;

    public float speed;
    public Vector3 initPos;
    private bool bPlaying;
    public bool bActive;
    private bool bDelay;
    public bool bRight;
    private void Awake()
    {
        laser = GetComponentInChildren<Laser>();
        laserEnd = GetComponentInChildren<LaserEnd>();
        laserStart = GetComponentInChildren<LaserStart>();
        laserCasting = GetComponentInChildren<LaserCasting>();

    }
    private void Start()
    {
        laser.sprite.enabled = false;
        initPos = transform.position;
        bActive = false;
        bPlaying = false;
        bRight = true;
        bDelay = false;
    }


    private void FixedUpdate()
    {
        if (bActive == true)
        {
            if (bPlaying == false )
            {
                bDelay = true;
                laser.sprite.enabled = true;
                Quaternion rotate = new Quaternion(0,0,0,0);
                rotate.eulerAngles = new Vector3(0, 0, 180);
                laser.sprite.flipY = false;
                initPos.x = -Mathf.Abs(initPos.x);
                if (bRight == true)
                {
                    initPos.x *= -1;
                    rotate.eulerAngles = new Vector3(0, 180, 180);
                    laser.sprite.flipY = true;
                }
                transform.position = initPos;
                transform.rotation = rotate;
                bPlaying = true;
                laser.speed = 0;
                laser.bActive = true;
                laserCasting.PlayPartical();
                laserEnd.PlayPartical();
                laserStart.PlayPartical();
            }
            if (laserCasting.bPlay == false)
                laser.speed = speed;
            Vector2 point = laser.PlayLaser();
            bActive = laser.bActive;
            laserEnd.gameObject.transform.position = point;
        }

        if (bActive == false && bPlaying == true)
        {
            bDelay = false;
            bPlaying = false;
            laser.sprite.enabled = false;
            laserCasting.StopPartical();
            laserStart.StopPartical();
            laserEnd.StopPartical();
        }
    }



}
