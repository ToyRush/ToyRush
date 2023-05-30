using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class CutScene : MonoBehaviour
{
    Color color;
    Image image;
    AudioSource audioSource;

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
    [SerializeField] AudioClip nextSound;


    void Awake()
    {
        image = GetComponent<Image>();
        audioSource = GetComponent<AudioSource>();
        color = image.color;
        StartCoroutine("TypingText");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && canSkip)
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
        audioSource.PlayOneShot(nextSound, 0.3f);
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
        audioSource.PlayOneShot(nextSound, 0.3f);
        SceneManager.LoadScene(1); // 1������ Ʃ�丮���
    }

    public void EndGame()
    {
        audioSource.PlayOneShot(nextSound, 0.3f);
        Application.Quit();
        Debug.Log("���� ����");
    }

    public void SkiptBtn()
    {
        storyFlow = storyImages.Length - 1;
        StartCoroutine("CameraEffect");
    }
}
