using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpeechBox : MonoBehaviour
{
    [SerializeField] string[] npcTexts;
    [SerializeField] Text text;
    public Transform characterTransform;
    private int textIdx = 0;
    private RectTransform uiRectTransform;
    Vector3 upVec = new Vector3(0, 1.5f, 0);
    private void Start()
    {
        uiRectTransform = GetComponent<RectTransform>();
        UpdateUIPosition();
        ChangeText();
    }

    private void Update()
    {
        UpdateUIPosition();
    }

    private void UpdateUIPosition()
    {
        Vector3 characterScreenPosition = Camera.main.WorldToScreenPoint(characterTransform.position + upVec);
        uiRectTransform.position = characterScreenPosition;
    }

    void ChangeText()
    {
        if (textIdx == npcTexts.Length)
            textIdx = 0;
        else
            text.text = npcTexts[textIdx];
        textIdx += 1;
        Invoke("ChangeText", 5f);
    }
}
