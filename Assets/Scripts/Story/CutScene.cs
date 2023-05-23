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
    float effectTimer = 3f;
    int storyFlow=0;
    bool canSkip = false;
    [SerializeField] Text storyText;
    [SerializeField] Sprite[] storyImages;
    [SerializeField] string[] storyTexts;
    WaitForSeconds changeImageTime = new WaitForSeconds(1f);
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
        typingTimer = effectTimer / storyTexts[storyFlow].Length;
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
        timer = effectTimer;
        while (timer > 0)
        {
            timer -= Time.deltaTime;
            color.a = timer / effectTimer;
            image.color = color;
            yield return null;
        }
        yield return changeImageTime;
        ChangeImage();
        timer = 0f;
        while (timer < effectTimer)
        {
            timer += Time.deltaTime;
            color.a = timer / effectTimer;
            image.color = color;
            yield return null;
        }
        StartCoroutine("TypingText");
    }

    void ChangeImage()
    {
        storyFlow += 1;
        if (storyFlow>=storyImages.Length)
            return;
        image.sprite = storyImages[storyFlow];
    }

    public void StartGame()
    {
        Debug.Log("게임을 시작합니다.");
    }

    public void EndGame()
    {
        Application.Quit();
    }
}
