using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponManager : MonoBehaviour
{
    // 스크립트 
    public PlayerShoot playerShoot;
    public PlayerTrap playerTrap;

    // 현재 가진 무기의 정보
    public int currentKey = 0; 
    public int bulletID = 100;
    public int bulletCnt = 0;
    public int trapID = 200;
    public int trapCnt = 0;
    bool canSelectTrap;
    // 무기 UI
    [SerializeField] GameObject bulletUI;
    [SerializeField] GameObject trapUI;
    [SerializeField] Image bulletImage;
    [SerializeField] Image trapImage;
    [SerializeField] Text bulletCntText;
    [SerializeField] Text trapCntText;

    // 아이템 데이터
    [SerializeField] ItemData normalBullet;
    Dictionary<int, ItemData> holdWeapon = new Dictionary<int, ItemData>();

    private void Awake()
    {
        SetDefaultBullet(); // 초기엔 보통 총알로 초기화
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

    void SetUILayer(int bulletLayer, int trapLayer) // UI 렌더링 순서 변경
    {
        bulletUI.transform.SetSiblingIndex(bulletLayer);
        trapUI.transform.SetSiblingIndex(trapLayer);
    }

    public void AddWeapon(ItemData _itemData) // 무기 습득 시, 호출
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

    public void DeleteWeapon(int _id) // 해당 아이디의 무기를 딕셔너리에서 제거
    {
        holdWeapon.Remove(_id);
    }


    public void ShootGun() // 총 발사 시, 총알 개수 카운트한다.
    {
        bulletCnt -= 1;
        if (bulletCnt <= 0 && bulletID!=normalBullet.itemID)
            SetDefaultBullet();
        SetBulletCnt();
    }
    public void UpdateBullet(ItemData _itemData) // 새 총알 습득 시, 초기화
    {
        bulletID = _itemData.itemID;
        bulletCnt = _itemData.itemCnt;
        bulletImage.sprite = holdWeapon[bulletID].itemIcon; // 이미지 설정
        SetBulletCnt();
        playerShoot.LoadBullet(_itemData.itemID);
    }


    public void SetDefaultBullet() // 보통 총알로 초기화
    {
        DeleteWeapon(bulletID);
        bulletID = normalBullet.itemID;
        playerShoot.LoadBullet(bulletID);
        bulletImage.sprite = normalBullet.itemIcon;
        SetBulletCnt();
        holdWeapon.Add(normalBullet.itemID, normalBullet);   
    }

    public void SetBulletCnt() // 총알의 개수
    {
        if (bulletID == normalBullet.itemID) 
            bulletCntText.text = " ";
        else
            bulletCntText.text = bulletCnt.ToString();
    }

    public void UpdateTrap(ItemData _itemData) // 새 트랩 습득 시, 초기화
    {
        trapID = _itemData.itemID;
        trapCnt = _itemData.itemCnt;
        trapImage.sprite = _itemData.itemIcon;
        playerTrap.LoadTrap(_itemData.itemID);
        SetDefaultTrap();
    }

    void SetDefaultTrap() // ID가 200일때 불러온다.
    {
        if (trapID == 200)
            canSelectTrap = false;
        else
            canSelectTrap = true;
    }

}
