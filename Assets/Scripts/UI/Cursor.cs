using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cursor : MonoBehaviour
{
    public RectTransform rect;
    public GameObject cursorObject;
    private Vector3 offset;
    Vector3 mousePos;

    private void Awake()
    {
        cursorObject.SetActive(true);
    }
    private void Start()
    {
        rect = GetComponent<RectTransform>();
        offset = new Vector3(rect.rect.width / 2, -rect.rect.height / 2,0);
    }

    private void FixedUpdate()
    {
        mousePos = Input.mousePosition;
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);
        if (Input.mousePosition.x < Screen.width && Input.mousePosition.y < Screen.height)
        {
            rect.position = Input.mousePosition + offset;
        }
        
    }
}
