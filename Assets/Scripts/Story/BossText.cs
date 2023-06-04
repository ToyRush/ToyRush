using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossText : MonoBehaviour
{
    bool canSkip=false;
    bool talkBoss = true;
    bool isStart = true;
    int startFlow = 0;
    int endFlow = 0;
    float typingSpeed=0f;
    [SerializeField] GameObject allObj; 
    [SerializeField] GameObject bossTextUI;
    [SerializeField] Text storyText;
    [SerializeField] float typingTimer;
    [SerializeField] string[] bossStartTexts;
    [SerializeField] string[] bossEndTexts;
 
    private void Awake()
    {
        BossStartText();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && canSkip && talkBoss)
        {
            if (isStart)
                StartCoroutine("StartTypingText");
            else
                StartCoroutine("EndTypingText");
        }
    }

    public void BossStartText()
    {
        talkBoss = true;
        allObj.SetActive(false);
        StartCoroutine("StartTypingText");
    }
    IEnumerator StartTypingText()
    {
        canSkip = false;
        if (startFlow >= bossStartTexts.Length)
        {
            allObj.SetActive(true);
            talkBoss = false;
            isStart = false;
            bossTextUI.SetActive(false);
            yield break;
        }
        storyText.text = "";
        typingSpeed = typingTimer / bossStartTexts[startFlow].Length;
        foreach (char word in bossStartTexts[startFlow])
        {
            storyText.text += word;
            yield return new WaitForSeconds(typingSpeed);
        }
        startFlow += 1;
        canSkip = true;
    }

    public void BossEndText()
    {
        bossTextUI.SetActive(true);
        talkBoss = true;
        StartCoroutine("EndTypingText");
    }
    IEnumerator EndTypingText()
    {
        canSkip = false;
        if (endFlow >= bossEndTexts.Length)
        {
            talkBoss = false;
            bossTextUI.SetActive(false);
            GameManager.instance.ClearBoss();
            yield break;
        }
        storyText.text = "";
        typingSpeed = typingTimer / bossEndTexts[endFlow].Length;
        foreach (char word in bossEndTexts[endFlow])
        {
            storyText.text += word;
            yield return new WaitForSeconds(typingSpeed);
        }
        endFlow += 1;
        canSkip = true;
    }
}
