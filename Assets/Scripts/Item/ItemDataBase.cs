using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ItemDataBase : MonoBehaviour
{
    // ���� ����
    public List<ItemData> AllItems = new List<ItemData>();

    public PlayerShoot playerShoot;
    public Key key;
    public Image bulletImage;
    [SerializeField] Sprite normalBulletImage;
    //public Sprite defaultBullet;
    //public Sprite defaultKey;
    private void Start()
    {
        AllItems.Add(new BulletData(101, 30));
        AllItems.Add(new KeyData(301));
    }

    public void ChangeNormalBullet()
    {
        bulletImage.sprite = normalBulletImage;
    }

    public void FindItem(int _itemID) // ������ Ÿ�Ե� �޾ƿ��� �� ���� ��
    {
        foreach (ItemData itemData in AllItems)
        {
            if (itemData is BulletData bulletData && bulletData.itemID == _itemID)
            {
                if (playerShoot.CheckBulletID() != _itemID - 100)
                    bulletImage.sprite = bulletData.itemIcon;
                playerShoot.SetBullet(bulletData.itemID - 100, bulletData.maxBulletCnt);
                return;
            }
            if(itemData is ConsumerData consumerType && consumerType.itemID == _itemID)
            {
                if(consumerType.consumableType== ConsumerData.ConsumableType.Heal)
                {
                    // �÷��̾� ü�� ȸ��
                }
                else if (consumerType.consumableType == ConsumerData.ConsumableType.SpeedUp)
                {
                    // �÷��̾� ���ǵ� ��
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
