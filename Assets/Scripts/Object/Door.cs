using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
    SpriteRenderer spr;
    [SerializeField] Sprite openDoor;
    bool isOpen=false;
    public int stageID;

    private void Start()
    {
        spr = GetComponent<SpriteRenderer>();
        stageID = GameManager.instance.GetStageID();
        OpenDoor();
    }
    public void OpenDoor()
    {
        isOpen = true;
        spr.sprite = openDoor;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && isOpen)
        {
            stageID += 1;
            GameManager.instance.NextStage(stageID);
            SceneManager.LoadScene(stageID);
        }
    }

    public bool GetDoorState() // ���� ���� ���¶�� ���踦 �Ҿ������ �ʴ´�.
    {
        return isOpen;
    }
}
