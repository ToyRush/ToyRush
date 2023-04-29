using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : MonoBehaviour // Æó±â ÇÒµí
{
    protected Rigidbody2D rb;
    private List<Vector2> fixedPos;
    private Vector2 targetV;

    private float speed;
    private int curindex;
    private bool isplus;
    public Vector2 getNextFixedPos()
    {
        return fixedPos[curindex];
    }

    public void SetNextFixedPos()
    {
        if (isplus)
            curindex++;
        else
            curindex--;

        if (curindex >= fixedPos.Count)
        {
            isplus = false;
            curindex = fixedPos.Count - 1;
        }
        if (curindex < 0)
        {
            isplus = true;
            curindex = 0;
        }
    }

    public void pushFixedPos(Vector2 pos)
    {
        if (fixedPos == null)
            fixedPos = new List<Vector2>();
        fixedPos.Add(pos);
    }

    ///  
    public void getReversePos(Vector2 dir)
    {
        dir *= -1;


    }

}
