using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuUI : MonoBehaviour
{
    [SerializeField] GameObject menuUI;

    public void OnMenuUI()
    {
        menuUI.SetActive(true);
        Time.timeScale = 0f;
    }

    public void OffMenuUI() 
    {
        menuUI.SetActive(false);
        Time.timeScale = 1f;
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(menuUI.activeSelf)
                menuUI.SetActive(false);
            else
                menuUI.SetActive(true);
        }
    }
}
