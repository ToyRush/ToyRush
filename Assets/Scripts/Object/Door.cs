using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
    bool isOpen=false;
    public int stageID;

    private void Start()
    {
        stageID = GameManager.instance.GetStageID();
        OpenDoor();
    }
    public void OpenDoor()
    {
        isOpen = true;
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
}
