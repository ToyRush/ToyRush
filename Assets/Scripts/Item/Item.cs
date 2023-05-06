using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public ItemData itemData;
    SpriteRenderer spr;
    int cnt = 0;
    private void Awake()
    {
        spr = GetComponent<SpriteRenderer>();
        spr.sprite = itemData.itemIcon;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
       
        if (collision.CompareTag("Player"))
        {
            cnt += 1;
            switch (itemData.itemType)
            {
                case ItemData.ItemType.Bullet:
                    GameManager.instance.UpdateBullet(itemData.itemID, itemData.bulletMaxCnt);
                    Debug.Log(cnt);
                    break;
                case ItemData.ItemType.Trap:
                    break;
                case ItemData.ItemType.StatUp:
                    break;
            }
        }
    }
}
