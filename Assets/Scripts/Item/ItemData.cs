using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu (fileName ="Item", menuName ="ScriptableObject/ItemData")]
public class ItemData : ScriptableObject
{
    public enum ItemType { Bullet, Trap, StatUp }
    [Header("������ �⺻ ����")]
    public ItemType itemType;
    public Sprite itemIcon;
    public int itemID;
    public string ItemName;

    [Header("�Ѿ�")]
    public int bulletMaxCnt;

    [Header("���Ⱦ�")]
    public float statUP;

}
