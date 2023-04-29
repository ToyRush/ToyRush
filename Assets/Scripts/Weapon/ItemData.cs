using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemData
{
    public int itemID;
    public Sprite itemIcon;
}


public class BulletData : ItemData
{
    public float damage; // 데미지
    public int maxBulletCnt; // 최대 총알 개수
   
    public BulletData(int _itemID, int _maxBulletCnt)
    {
        itemID = _itemID;
        maxBulletCnt = _maxBulletCnt;
        itemIcon= Resources.Load("Sprites/Item/" + itemID.ToString(), typeof(Sprite)) as Sprite;
    }
}

public class ConsumerData : ItemData
{
    public float figure; // 수치
    public ConsumableType consumableType;
    public enum ConsumableType
    {
        Heal, // 체력 회복
        SpeedUp // 속도 상승
    }

    public ConsumerData(int _itemID, float _figure, ConsumableType _consumableType)
    {
        itemID = _itemID;
        figure = _figure;
        consumableType = _consumableType;
        itemIcon = Resources.Load("Sprites/Item/" + itemID.ToString(), typeof(Sprite)) as Sprite;
    }
}

public class KeyData : ItemData
{
    public KeyData(int _itemID)
    {
        itemID = _itemID;
        itemIcon = Resources.Load("Sprites/Item/" + itemID.ToString(), typeof(Sprite)) as Sprite;
    }
}