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
    public float damage; // ������
    public int maxBulletCnt; // �ִ� �Ѿ� ����
   
    public BulletData(int _itemID, int _maxBulletCnt)
    {
        itemID = _itemID;
        maxBulletCnt = _maxBulletCnt;
        itemIcon= Resources.Load("Sprites/Item/" + itemID.ToString(), typeof(Sprite)) as Sprite;
    }
}

public class ConsumerData : ItemData
{
    public float figure; // ��ġ
    public ConsumableType consumableType;
    public enum ConsumableType
    {
        Heal, // ü�� ȸ��
        SpeedUp // �ӵ� ���
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