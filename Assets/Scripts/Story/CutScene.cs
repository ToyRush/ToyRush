using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class CutScene : MonoBehaviour
{
    Image image;
    Color color;

    float timer = 0f;
    float typingTimer = 0f;
    int storyFlow=0;
    bool canSkip = false;

    [SerializeField] Text storyText;
    [SerializeField] Sprite[] storyImages;
    [SerializeField] string[] storyTexts;

    [SerializeField] float fadeTimer = 2f;
    [SerializeField] float typeEffectTimer = 2f;
    WaitForSeconds changeImageTime = new WaitForSeconds(1f);

    [SerializeField] GameObject textUI;
    [SerializeField] GameObject startPage;


    void Awake()
    {
        image = GetComponent<Image>();
        color = image.color;
        StartCoroutine("TypingText");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q) && canSkip)
            StartCoroutine("CameraEffect");
    }

    IEnumerator TypingText()
    {
        storyText.text = "";
        typingTimer = typeEffectTimer / storyTexts[storyFlow].Length;
        foreach (char word in storyTexts[storyFlow])
        {
            storyText.text += word;
            yield return new WaitForSeconds(typingTimer);
        }
        canSkip = true;
        yield return null;
    }

    IEnumerator CameraEffect()
    {
        canSkip = false;
        timer = fadeTimer;
        while (timer > 0)
        {
            timer -= Time.deltaTime;
            color.a = timer / fadeTimer;
            image.color = color;
            yield return null;
        }
        TextUIOff();
        yield return changeImageTime;
        TextUIOn();
        ChangeImage();
        timer = 0f;
        while (timer < fadeTimer)
        {
            timer += Time.deltaTime;
            color.a = timer / fadeTimer;
            image.color = color;
            yield return null;
        }
        StartCoroutine("TypingText");
    }

    void TextUIOff()
    {
        storyText.text = "";
        textUI.SetActive(false);
    }

    void TextUIOn()
    {
        textUI.SetActive(true);
    }

    void ChangeImage()
    {
        storyFlow += 1;
        if (storyFlow >= storyImages.Length)
        {
            StopAllCoroutines();
            startPage.SetActive(true);
            canSkip = false;
            return;
        }
        image.sprite = storyImages[storyFlow];
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1); // 1¹ø¾ÀÀº Æ©Åä¸®¾ó¾À
    }

    public void EndGame()
    {
        Application.Quit();
        Debug.Log("°ÔÀÓ Á¾·á");
    }
}
