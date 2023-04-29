using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public enum BtnType
{
    Start,
    Quit
}

public class Button : MonoBehaviour
{
    [SerializeField]
    BtnType btnType;
    public void OnClickBtn()
    {
        switch (btnType)    
        {
            case BtnType.Start:
                SceneManager.LoadScene("Test");
                break;
            case BtnType.Quit:
                Application.Quit();
                break;
        }
    }
    
}
