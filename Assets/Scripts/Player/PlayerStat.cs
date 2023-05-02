using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStat : MonoBehaviour
{
    PlayerMove playerMove;
    float maxHealth;
    float currentHealth;
    float damage;
    float heal;

    public Image[] healthImage;
    Color color;
    Color deafultColor;
    
    void Awake()
    {
        playerMove = GetComponent<PlayerMove>();    
    }

    void Start()
    {
        color = healthImage[0].color;
        deafultColor = color;
        color.a = 0.3f;
    }

    public void Damaged(float damage)
    {
        if (currentHealth > 0)
        {
            currentHealth -= damage;
            GameManager.instance.SetHealth(currentHealth);
        }
        else
            playerMove.Dead();
;    }

    public void Heal(float heal)
    {
        if (heal + currentHealth > maxHealth)
            currentHealth = maxHealth;
        else
            currentHealth += heal;
        GameManager.instance.SetHealth(currentHealth);
    }
}
