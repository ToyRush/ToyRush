using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    public float InitAngle;
    public float CurrentAngle;
    public float EndAngle;
    public float speed;
    protected float CurrentTime;
    public SpriteRenderer sprite;

    public bool bActive;
    public float multi;
    private void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
    }
    private void Start()
    {
        transform.localScale = new Vector2(1000 * multi, transform.localScale.y);
        bActive = false;
    }
    public Vector2 PlayLaser()
    {
        Vector2 point = Vector2.zero;
        if (bActive == false)
            return point;

        CurrentAngle += Time.fixedDeltaTime * speed;
        transform.Rotate(Vector3.forward, Time.fixedDeltaTime * speed);

        if (CurrentAngle > EndAngle)
        {
            bActive = false;
            CurrentAngle = InitAngle;
            transform.rotation = new Quaternion(0,0,0,0);
            transform.localScale = new Vector2(1000 * multi, transform.localScale.y);
            return point;
        }

        RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, transform.right, 50);
        RaycastHit2D? hitPoint = null;
        float size = 100;
        foreach (RaycastHit2D hit in hits)
        {
            if (hit && hit.transform.name != "Boss")
            {
                if (hit.distance != 0 && hit.distance < size)
                {
                    size = hit.distance;
                    hitPoint = hit;
                }
            }
        }

        if (hitPoint != null)
        {
            point = hitPoint.Value.point;
            //Vector2 temp = transform.position;
            //size = Vector2.Distance(temp , point);
        }
        transform.localScale = new Vector2(size * multi, transform.localScale.y);
        return point;
    }
}
