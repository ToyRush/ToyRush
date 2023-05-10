using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public ItemData itemData;
    SpriteRenderer spr;
    private void Awake()
    {
        spr = GetComponent<SpriteRenderer>();
        spr.sprite = itemData.itemIcon;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
       
        if (collision.CompareTag("Player"))
        {
            switch (itemData.itemType)
            {
                case ItemData.ItemType.Bullet:
                    GameManager.instance.weaponManager.AddWeapon(itemData);
                    break;
                case ItemData.ItemType.Trap:
                    GameManager.instance.weaponManager.AddWeapon(itemData);
                    break;
                case ItemData.ItemType.Consumer:
                    break;
                case ItemData.ItemType.Key:
                    break;
            }
        }
    }
}
