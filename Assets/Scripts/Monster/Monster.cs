using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : Unit
{
    
    // Start is called before the first frame update
    protected override void Awake()
    {
        base.Awake();
    }
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }

    public override void Damaged(float attack) 
    {
        if (unitState != UnitState.Dead)
        {
            Hp -= attack;
            if (Hp < 0.01f)
                unitState = UnitState.Dead;
            else
                unitState = UnitState.Hitted;
        }
    }
}
