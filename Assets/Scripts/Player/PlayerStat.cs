using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStat : MonoBehaviour
{
    GameObject hpUIObject;
    PlayerMove playerMove;
    HpUI hpUI;
    int maxHealth=5;
    int currentHealth;
    int damage;
    int heal;
    void Awake()
    {
        hpUIObject = GameObject.FindGameObjectWithTag("Hp");
        hpUI = hpUIObject.GetComponent<HpUI>();
        playerMove = GetComponent<PlayerMove>();
        currentHealth = GameManager.instance.GetHealth();
        hpUI.ShowHp(currentHealth);
    }


    public void Damaged(int damage)
    {
        if (currentHealth > 0)
        {
            currentHealth -= damage;
            GameManager.instance.SetHealth(currentHealth);
        }
        else
            playerMove.Dead();
        StartCoroutine("NonTargetState");
        hpUI.ShowHp(currentHealth); 
    }

    public void Heal(int heal)
    {
        if (heal + currentHealth > maxHealth)
            currentHealth = maxHealth;
        else
            currentHealth += heal;
        GameManager.instance.SetHealth(currentHealth);
        hpUI.ShowHp(currentHealth);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
            Damaged(1);
        if (Input.GetKeyDown(KeyCode.L))
            Heal(1);
    }

    IEnumerator NonTargetState()
    {
        gameObject.tag = "NonTarget";
        playerMove.KnockBack();
        yield return new WaitForSeconds(2f);
        gameObject.tag = "Player";
        yield return 0;
    }
}
