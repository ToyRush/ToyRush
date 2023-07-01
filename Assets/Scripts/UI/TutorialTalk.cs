using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialTalk : MonoBehaviour
{
    bool canSkip = false;
    bool talkBoss = true;
    bool isStart = true;
    int startFlow = 0;
    float typingSpeed = 0f;
    [SerializeField] PlayerMove playerMove;
    [SerializeField] PlayerShoot playerShoot;
    [SerializeField] GameObject bossTextUI;
    [SerializeField] GameObject menuUI;
    [SerializeField] Text storyText;
    [SerializeField] float typingTimer;
    [SerializeField] string[] bossStartTexts;

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
        }
    }

    public void BossStartText()
    {
        talkBoss = true;
        playerMove.canMove = false;
        playerShoot.enabled = false;
        menuUI.SetActive(false);
        StartCoroutine("StartTypingText");
    }
    IEnumerator StartTypingText()
    {
        canSkip = false;
        if (startFlow >= bossStartTexts.Length)
        {
            playerMove.canMove = true;
            playerShoot.enabled = true;
            menuUI.SetActive(true);
            GameManager.instance.canClick = true;
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
}
