using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStat : MonoBehaviour
{
    public Image[] healthImage;
    public int maxHealth;
    public int health;
    public int damage;
    public int heal;

    Color color;
    Color deafultColor;
    void Start()
    {
        color = healthImage[0].color;
        deafultColor = color;
        color.a = 0.3f;
    }

    void Update()
    {
        for (int i = 0; i < health; i++)
        {
            healthImage[i].color = deafultColor;
        }
        for(int i=health; i<maxHealth; i++)
        {
            healthImage[i].color = color;
        }
        if (Input.GetKeyDown(KeyCode.Q))
            Damaged(damage);
        else if (Input.GetKeyDown(KeyCode.E))
            Heal(heal);
    }

    public void Damaged(int damage)
    {
        if (health > 0)
            health-=damage;
    }

    public void Heal(int heal)
    {
        if(health<maxHealth)
            health+=heal;
    }
}
