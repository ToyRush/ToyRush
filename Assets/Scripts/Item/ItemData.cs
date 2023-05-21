using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu (fileName ="Item", menuName ="ScriptableObject/ItemData")]
public class ItemData : ScriptableObject
{
    public enum ItemType { Bullet, Trap, Consumer, Key }
    [Header("아이템 기본 정보")]
    public ItemType itemType;
    public Sprite itemIcon;
    public int itemID;
    public string ItemName;

    [Header("총알,함정")]
    public int itemCnt;

    [Header("소비재")]
    public int figure;
    public ConsumerType consumerType;
}
