using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpUI : MonoBehaviour
{
    Image[] hpCompartments;
    Color[] hpColors;
    Color defaultColor;
    Color transparentColor;
    float transparent = 0.5f;
    int maxHp = 5;
    private void Awake()
    {
        hpCompartments = gameObject.GetComponentsInChildren<Image>();
        hpColors = new Color[hpCompartments.Length];
        defaultColor = Color.white;
        transparentColor = defaultColor;
        transparentColor.a = transparent;
    }
    
    public void ShowHp(int _currentHp)
    {
        for(int i=maxHp; i>maxHp-_currentHp; i--)
        {
            hpCompartments[i].color= defaultColor;
        }
        for (int i = maxHp - _currentHp; i > 0; i--)
        {
            hpCompartments[i].color = transparentColor;
        }
    }
}
