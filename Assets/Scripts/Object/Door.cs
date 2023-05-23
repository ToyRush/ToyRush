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
        GameManager.instance.RegisterDoorState(this);
        spr = GetComponent<SpriteRenderer>();
        stageID = GameManager.instance.GetStageID();
    }

    public void Open()
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

    public bool CheckDoor() // 문이 열려있으면 열쇠를 잃어버리지 않는 방식으로 채택
    {
        return isOpen;
    }
}
