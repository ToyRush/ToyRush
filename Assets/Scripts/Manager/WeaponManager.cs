using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponManager : MonoBehaviour
{
    public int currentKey=1;
    public PlayerShoot playerShoot;
    
    public int bulletCnt = 0;
    public int bulletID = 0;
    public int trapCnt = 0;
    public int trapID = 0;

    [SerializeField] Image weaponImage;
    [SerializeField] ItemData normalBullet;
    Dictionary<int, ItemData> holdWeapon = new Dictionary<int, ItemData>();

    private void Awake()
    {
        SetNormalBullet();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            currentKey = 1;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            currentKey = 2;
        }
    }

    public void AddWeapon(ItemData _itemData)
    {
        switch (_itemData.itemType)
        {
            case ItemData.ItemType.Bullet:
                if (_itemData.itemID == bulletID)
                {
                    // 총알 개수만 채우기
                }
                else
                {
                    // 총알 개수 채우기
                    DeleteWeapon(bulletID);
                    holdWeapon.Add(_itemData.itemID, _itemData);
                }
                UpdateBullet(_itemData);
                break;
            case ItemData.ItemType.Trap:
                if (_itemData.itemID == trapID)
                {
                    // 트랩 개수만 채우기
                }
                else
                {
                    // 트랩 개수 채우기
                    DeleteWeapon(trapID);
                    holdWeapon.Add(_itemData.itemID, _itemData);
                }
                break;
        }
    }

    public void UpdateBullet(ItemData _itemData)
    {
        bulletID = _itemData.itemID;
        playerShoot.SetBullet(_itemData.itemID-100, _itemData.itemCnt);

    }

    public void UpdateTrap(ItemData _itemData)
    {
        trapID = 0;
    }

    public void DeleteWeapon(int _id)
    {
        holdWeapon.Remove(_id);
    }

    public void SetNormalBullet()
    {
        bulletID = 101;
        holdWeapon.Add(normalBullet.itemID, normalBullet);
    }
}
