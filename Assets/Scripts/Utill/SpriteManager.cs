using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteManager : MonoBehaviour
{
    private Animator anim;
    private SpriteRenderer spr;
    private int state;

    // Start is called before the first frame update
    public void Init(Sprite sprite, RuntimeAnimatorController con)
    {
        spr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        if (spr == null)
            spr = gameObject.AddComponent<SpriteRenderer>();
        if (anim == null)
            anim = gameObject.AddComponent<Animator>();
        state = anim.GetInteger("State");

        spr.sprite = sprite;
        anim.runtimeAnimatorController = con;
    }
    public void Init()
    {
        spr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        if (spr == null)
            spr = gameObject.AddComponent<SpriteRenderer>();
        if (anim == null)
            anim = gameObject.AddComponent<Animator>();
        state = anim.GetInteger("State");
    }

    public void SetState(UnitState state)
    {
        if (anim != null)
        {
            anim.SetInteger("State", ((int)state));
            this.state = ((int)state);
        }
    }
    public void SetFlips() // 오류 ! state 에 따라 flip 해야할듯
    {
        if (state != 0)
        {
            bool flips;
            Vector2 currentV = this.transform.position;
            Vector2 targetV = this.GetComponent<Moving>().GetTargetV();
            Vector2 direction = (targetV - currentV).normalized;

            if (direction.x < 0.001f)
                flips = true;
            else
                flips = false;
            spr.flipX = flips;
        }
    }

    public void SetVisible(bool visiible)
    {
        spr.enabled = visiible;
    }

}
