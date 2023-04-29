using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ObjectState
{
    Stop = 0,
    Selected = 1,

    Dstroy = 5
}

public class IOject : MonoBehaviour // 상자 등의 obj
{
    //protected
    protected SpriteManager sprManager;
    protected Rigidbody2D rigid;
    protected BoxCollider2D boxCollider;
    public ObjectState state;
    // --

    // sprite
    public RuntimeAnimatorController controller;
    public Sprite sprite;
    protected virtual void Awake()
    {
        sprManager = GetComponent<SpriteManager>();
        rigid = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();

        if (sprManager == null)
            sprManager = gameObject.AddComponent<SpriteManager>();
        if (rigid == null)
            rigid = gameObject.AddComponent<Rigidbody2D>();
        if (boxCollider == null)
            boxCollider = gameObject.AddComponent<BoxCollider2D>();

        sprManager.Init(sprite, controller);
    }
    protected virtual void Start()
    {
        boxCollider.isTrigger = true;
    }

    protected virtual void ResetObject()
    {
        sprManager.SetVisible(true);
        boxCollider.isTrigger = true;
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Debug.Log(collision.name + " Obj Enter");
            sprManager.SetVisible(false);
            boxCollider.isTrigger = false;
        }
    }

    protected virtual void OnCollisionStay2D(Collision2D collision) 
    {
    }
    protected virtual void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Debug.Log(collision.name + " Obj Exit");
        }
    }


}