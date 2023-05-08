using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponManager : MonoBehaviour
{
    // ��ũ��Ʈ 
    public PlayerShoot playerShoot;
    public PlayerTrap playerTrap;

    // ���� ���� ������ ����
    public int currentKey = 0; 
    public int bulletID = 100;
    public int bulletCnt = 0;
    public int trapID = 200;
    public int trapCnt = 0;
    bool canSelectTrap;
    // ���� UI
    [SerializeField] GameObject bulletUI;
    [SerializeField] GameObject trapUI;
    [SerializeField] Image bulletImage;
    [SerializeField] Image trapImage;
    [SerializeField] Text bulletCntText;
    [SerializeField] Text trapCntText;

    // ������ ������
    [SerializeField] ItemData normalBullet;
    Dictionary<int, ItemData> holdWeapon = new Dictionary<int, ItemData>();

    private void Awake()
    {
        SetDefaultBullet(); // �ʱ⿣ ���� �Ѿ˷� �ʱ�ȭ
        SetDefaultTrap();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) && currentKey!=1)
        {
            currentKey = 1;
            Cursor.instance.ChangeCursorState(currentKey);
            SetUILayer(2,1);
            playerShoot.ControlGun(true);
            playerTrap.ControlTrap(false);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2) && currentKey !=2 && canSelectTrap)
        {
            currentKey = 2;
            Cursor.instance.ChangeCursorState(currentKey);
            SetUILayer(1,2);
            playerShoot.ControlGun(false);
            playerTrap.ControlTrap(true);
        }
    }

    void SetUILayer(int bulletLayer, int trapLayer) // UI ������ ���� ����
    {
        bulletUI.transform.SetSiblingIndex(bulletLayer);
        trapUI.transform.SetSiblingIndex(trapLayer);
    }

    public void AddWeapon(ItemData _itemData) // ���� ���� ��, ȣ��
    {
        switch (_itemData.itemType)
        {
            case ItemData.ItemType.Bullet:
                if (_itemData.itemID != bulletID)
                {
                    DeleteWeapon(bulletID);
                    holdWeapon.Add(_itemData.itemID, _itemData);
                }
                UpdateBullet(_itemData);
                break;
            case ItemData.ItemType.Trap:
                if (_itemData.itemID != trapID)
                {
                    DeleteWeapon(trapID);
                    holdWeapon.Add(_itemData.itemID, _itemData);
                }
                UpdateTrap(_itemData);
                break;
        }
    }

    public void DeleteWeapon(int _id) // �ش� ���̵��� ���⸦ ��ųʸ����� ����
    {
        holdWeapon.Remove(_id);
    }


    public void ShootGun() // �� �߻� ��, �Ѿ� ���� ī��Ʈ�Ѵ�.
    {
        bulletCnt -= 1;
        if (bulletCnt <= 0 && bulletID!=normalBullet.itemID)
            SetDefaultBullet();
        SetBulletCnt();
    }
    public void UpdateBullet(ItemData _itemData) // �� �Ѿ� ���� ��, �ʱ�ȭ
    {
        bulletID = _itemData.itemID;
        bulletCnt = _itemData.itemCnt;
        bulletImage.sprite = holdWeapon[bulletID].itemIcon; // �̹��� ����
        SetBulletCnt();
        playerShoot.LoadBullet(_itemData.itemID);
    }


    public void SetDefaultBullet() // ���� �Ѿ˷� �ʱ�ȭ
    {
        DeleteWeapon(bulletID);
        bulletID = normalBullet.itemID;
        playerShoot.LoadBullet(bulletID);
        bulletImage.sprite = normalBullet.itemIcon;
        SetBulletCnt();
        holdWeapon.Add(normalBullet.itemID, normalBullet);   
    }

    public void SetBulletCnt() // �Ѿ��� ����
    {
        if (bulletID == normalBullet.itemID) 
            bulletCntText.text = " ";
        else
            bulletCntText.text = bulletCnt.ToString();
    }

    public void UpdateTrap(ItemData _itemData) // �� Ʈ�� ���� ��, �ʱ�ȭ
    {
        trapID = _itemData.itemID;
        trapCnt = _itemData.itemCnt;
        trapImage.sprite = _itemData.itemIcon;
        playerTrap.LoadTrap(_itemData.itemID);
        SetDefaultTrap();
    }

    void SetDefaultTrap() // ID�� 200�϶� �ҷ��´�.
    {
        if (trapID == 200)
            canSelectTrap = false;
        else
            canSelectTrap = true;
    }

}
