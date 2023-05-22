using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashUI : MonoBehaviour
{
    PlayerMove playerMove;
    Animator anim;

    void Awake()
    {
        playerMove = GetComponentInParent<PlayerMove>();
        anim = GetComponent<Animator>();
        anim.SetBool("isCharge", false);
    }

    public void UseDash()
    {
        anim.SetBool("isCharge", true);
        playerMove.CanDash(false);
    }

    public void FullCharge()
    {
        anim.SetBool("isCharge", false);
        playerMove.CanDash(true);
    }
}
