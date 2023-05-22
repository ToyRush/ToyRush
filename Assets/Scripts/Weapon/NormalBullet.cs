using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalBullet : Bullet
{
    public override void SetBulletDelay()
    {
        shootDelay = new WaitForSeconds(0.1f);
    }

}
