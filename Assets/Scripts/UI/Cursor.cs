using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cursor : MonoBehaviour
{
    public static Cursor cursorInstance;
    
    public Sprite[] cursorMode; // 커서 이미지, 첫번째는 총알, 두번째는 함정

    SpriteRenderer spr;
    Color defaultColor;
    Vector3 mousePos; // 마우스 위치
    List<Vector3> trapPos = new List<Vector3>();

    int currentKey=0;
    bool setTrap;
   
   
    private void Start()
    {
        spr = GetComponent<SpriteRenderer>();
        currentKey = GameManager.instance.weaponManager.currentKey;
        spr.sprite = cursorMode[currentKey - 1];
        cursorInstance = this;
        defaultColor = spr.color;
    }

    public void ChangeCursorState(int _currentKey)
    {
        currentKey = _currentKey;
        spr.sprite = cursorMode[_currentKey - 1];
    }

    void Update()
    {
        CheckMousePos();
        SetColorCursor();
    }
    
    void CheckMousePos()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (currentKey == 1)
            mousePos.z = 0;
        else if (currentKey == 2)
            mousePos = new Vector3(Mathf.Round(mousePos.x), Mathf.Round(mousePos.y), 0);
        transform.position = mousePos;
    }

    void SetColorCursor()
    {
        if (currentKey == 2)
        {
            if ((Mathf.Abs(transform.localPosition.x) > 3f || Mathf.Abs(transform.localPosition.y) > 3f)||trapPos.Contains(mousePos))
            {
                spr.color = Color.red;
                setTrap = false;
            }
            else
            {
                spr.color = Color.green;
                setTrap = true;
            }
        }
        else
        {
            spr.color = defaultColor;
        }
    }

    public bool CanSetTrap()
    {
        return setTrap;
    }

    public Vector3 GetMousePos()
    {
        return mousePos;
    }

    public void AddTrapPos(Vector3 _trapPos)
    {
        trapPos.Add(_trapPos);
    }

    public void DeleteTrapPos(Vector3 _trapPos)
    {
        trapPos.Remove(_trapPos);
    }
}
