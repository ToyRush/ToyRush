using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Trap : MonoBehaviour
{
    protected int damage;
    protected float debuffFigure;

    protected abstract void OnCollisionEnter2D(Collision2D collision);
}
