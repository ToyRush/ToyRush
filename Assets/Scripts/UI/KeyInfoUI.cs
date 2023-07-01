using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyInfoUI : MonoBehaviour
{
    [SerializeField] GameObject keyInfoText;
    [SerializeField] RectTransform rect;

    public float targetWidth = 600f;
    public float duration = 1f;
    private float currentWidth;
    private float timer;
    private bool increaseWidth = true;
    private bool decreaseWidth = false;

    WaitForSeconds showTime = new WaitForSeconds(3f);

    private void Update()
    {
        if (increaseWidth)
        {
            timer += Time.deltaTime;
            currentWidth = Mathf.Lerp(0f, targetWidth, timer / duration);
            rect.sizeDelta = new Vector2(currentWidth, rect.sizeDelta.y);
            if (currentWidth >= targetWidth)
            {
                increaseWidth = false;
                StartCoroutine("ShowKeyInfoUI");
            }
        }

        if (decreaseWidth)
        {
            timer += Time.deltaTime;
            currentWidth = Mathf.Lerp(currentWidth, 0, timer / duration);
            rect.sizeDelta = new Vector2(currentWidth, rect.sizeDelta.y);
            if (currentWidth <= 0)
            {
                decreaseWidth = false;
                gameObject.SetActive(false);
            }
        }
    }

    private void OnEnable()
    {
        currentWidth = 0f;
        timer = 0f;
        increaseWidth = true;
    }

    IEnumerator ShowKeyInfoUI()
    {
        keyInfoText.SetActive(true);
        yield return showTime;
        timer = 0f;
        decreaseWidth = true;
        keyInfoText.SetActive(false);
    }
}
