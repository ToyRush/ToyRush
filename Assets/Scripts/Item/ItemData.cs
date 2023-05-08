using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu (fileName ="Item", menuName ="ScriptableObject/ItemData")]
public class ItemData : ScriptableObject
{
    public enum ItemType { Bullet, Trap, StatUp }
    [Header("아이템 기본 정보")]
    public ItemType itemType;
    public Sprite itemIcon;
    public int itemID;
    public string ItemName;

    [Header("총알")]
    public int itemCnt;

    [Header("스탯업")]
    public float statUP;

}
