using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ending : MonoBehaviour
{
    RectTransform rect; 
    Image image;
    Color color;
    [SerializeField]float speed = 20f;
    bool isEnd = false;
    bool fadeEnd = false;
    bool gameExit = false;
    [SerializeField]Sprite endingImg;
    [SerializeField] float timer = 0f;
    [SerializeField] float fadeTimer = 2f;
    private void Awake()
    {
        image = GetComponent<Image>();
        rect = GetComponent<RectTransform>();
        color = image.color;
        timer = fadeTimer;
    }
    void Update()
    {
        if (!isEnd)
            ImageEffect();
        else
            EndEffect();
        if (Input.anyKeyDown && gameExit)
        {
            Application.Quit();
            Debug.Log("Á¾·á");
        }
    }

    void ImageEffect()
    {
        Vector2 position = rect.anchoredPosition;
        position.y += speed * Time.deltaTime;
        rect.anchoredPosition = position;
        if (position.y >= 1280)
            isEnd = true;
    }

    void EndEffect()
    {
        if (!fadeEnd)
        {
            timer -= Time.deltaTime;
            color.a = timer / fadeTimer;
            image.color = color;
        }
        if (timer <= 0)
        {
            fadeEnd = true;
            gameExit = true;
        }
    }
}
