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
    bool isInvincible=false; // 무적인가?
    float invincibleTime=2f; // 무적 시간
    void Awake()
    {
        hpUIObject = GameObject.FindGameObjectWithTag("Hp");
        hpUI = hpUIObject.GetComponent<HpUI>();
        playerMove = GetComponent<PlayerMove>();
        currentHealth = GameManager.instance.GetHealth();
        hpUI.ShowHp(currentHealth);
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.K))
            Damaged(1);
    }
    public void Damaged(int damage)
    {
        if (!isInvincible) // 무적이 아닐때만 호출
        {
            if (currentHealth > 0)
            {
                currentHealth -= damage;
                GameManager.instance.SetHealth(currentHealth);
            }
            else
                playerMove.Dead();
            StartCoroutine("Invincible");
            hpUI.ShowHp(currentHealth);
        }
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

    IEnumerator Invincible()
    {
        isInvincible = true;
        playerMove.OnOffDamaged(true);
        yield return new WaitForSeconds(invincibleTime);
        isInvincible = false;
        playerMove.OnOffDamaged(false);
    }
}
