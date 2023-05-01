using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ItemDataBase : MonoBehaviour
{
    // 새로 생성
    public List<ItemData> AllItems = new List<ItemData>();

    public PlayerMove playerMove;
    public Key key;
    public Image bulletImage;
    //public Sprite defaultBullet;
    //public Sprite defaultKey;
    private void Start()
    {
        AllItems.Add(new BulletData(101, 30));
        AllItems.Add(new KeyData(301));
    }

    public void FindItem(int _itemID) // 아이템 타입도 받아오면 더 좋을 듯
    {
        foreach (ItemData itemData in AllItems)
        {
            if (itemData is BulletData bulletData && bulletData.itemID == _itemID)
            {
                if (playerMove.bulletNum == _itemID - 100)
                {
                    playerMove.bulletMaxCnt = bulletData.maxBulletCnt;
                    playerMove.ChargeBullet();
                    return;
                }
                else
                {
                    bulletImage.sprite = bulletData.itemIcon;
                    playerMove.bulletMaxCnt = bulletData.maxBulletCnt;
                    playerMove.bulletNum = bulletData.itemID - 100;
                    playerMove.ChargeBullet();
                    return;
                }
            }
            if(itemData is ConsumerData consumerType && consumerType.itemID == _itemID)
            {
                if(consumerType.consumableType== ConsumerData.ConsumableType.Heal)
                {
                    // 플레이어 체력 회복
                }
                else if (consumerType.consumableType == ConsumerData.ConsumableType.SpeedUp)
                {
                    // 플레이어 스피드 업
                }
            }

            if(itemData is KeyData keyType)
            {
                if (keyType.itemID == _itemID)
                {
                    key.ChargeGauge(keyType.itemID);
                    return;
                }
            }
        }
    }
}
