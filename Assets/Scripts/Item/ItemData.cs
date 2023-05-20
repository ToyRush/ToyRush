using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu (fileName ="Item", menuName ="ScriptableObject/ItemData")]
public class ItemData : ScriptableObject
{
    public enum ItemType { Bullet, Trap, Consumer, Key }
    [Header("������ �⺻ ����")]
    public ItemType itemType;
    public Sprite itemIcon;
    public int itemID;
    public string ItemName;

    [Header("�Ѿ�,����")]
    public int itemCnt;

    [Header("�Һ���")]
    public int figure;
    public ConsumerType consumerType;
}
