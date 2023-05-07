using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cursor : MonoBehaviour
{
    SpriteRenderer spr;
    int currentKey = 0;

    public static Cursor instance;
    public Vector3 mousePos;
    public Sprite[] cursorMode;
    private void Awake()
    {
        spr = GetComponent<SpriteRenderer>();
        currentKey = GameManager.instance.weaponManager.currentKey;
        spr.sprite = cursorMode[currentKey - 1];
        instance = this;
    }

    public void ChangeCursorState(int _currentKey)
    {
        currentKey = _currentKey;
        spr.sprite = cursorMode[_currentKey - 1];
    }

    void Update()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (currentKey == 2)
            mousePos = new Vector3(Mathf.Round(mousePos.x), Mathf.Round(mousePos.y), 0);
        else if (currentKey == 1)
            mousePos.z = 0;
        transform.position = mousePos;
    }
}
