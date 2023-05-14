using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightBullet : Bullet
{
    public override void SetBulletDelay()
    {
        shootDelay = new WaitForSeconds(0.3f);
    }
}
