using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class CutScene : MonoBehaviour
{
    private int storyID;
    private bool canNext;
    public Text text;
    public string[] storyText;
    public Image[] storyImage;
    public Image fadeImage;
    float time = 0f;
    float fadeTime = 1f;

    public Image textBox;
    public GameObject textBoxObj;
    void Awake()
    {
        storyID = 0;
        canNext = false;
        storyImage[storyID].gameObject.SetActive(true);
        textBoxObj.SetActive(false);
        StartCoroutine("TypingEffect");
    }

    // Update is called once per frame
    void Update()
    {
        if(canNext && Input.GetKeyDown(KeyCode.Space))
        {
            canNext = false;
            StartCoroutine("FadeFlow");
        }
        if (Input.GetKeyDown(KeyCode.Alpha0)) 
        {
            SceneManager.LoadScene("Tutorial");
        }
    }
    IEnumerator TypingEffect()
    {
        for(int i=0; i<=storyText[storyID].Length; i++)
        {
            text.text = storyText[storyID].Substring(0, i);
            Debug.Log(i);
            yield return new WaitForSeconds(0.05f);
        }
        canNext = true;
    }

    IEnumerator FadeFlow()
    {
        time = 0f;
        Color alpha = fadeImage.color;
        Color textAlpha = textBox.color;
        while (alpha.a < 1f)
        {
            time += Time.deltaTime / fadeTime;
            alpha.a = Mathf.Lerp(0, 1, time);
            textAlpha.a= Mathf.Lerp(1, 0, time);
            fadeImage.color=alpha;
            textBox.color = textAlpha;
            yield return null;
        }


        time = 0f;
        storyImage[storyID].gameObject.SetActive(false);
        storyID += 1;
        if (storyID == storyImage.Length-1 )
            textBoxObj.SetActive(false);
        else
            text.text = " ";
        storyImage[storyID].gameObject.SetActive(true);
        yield return new WaitForSeconds(1f);


        while (alpha.a > 0f)
        {
            time += Time.deltaTime / fadeTime;
            alpha.a = Mathf.Lerp(1, 0, time);
            textAlpha.a = Mathf.Lerp(0, 1, time);
            fadeImage.color = alpha;
            textBox.color = textAlpha;
            yield return null;
        }
        text.text = storyText[storyID];

        if(storyID!= storyImage.Length - 1)
            StartCoroutine("TypingEffect");
        yield return null;
    }
}
