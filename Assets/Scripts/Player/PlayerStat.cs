using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStat : MonoBehaviour
{
    [SerializeField] GameObject shiledObject;
    [SerializeField] ParticleSystem healEffect;
    [SerializeField] GameObject dashUIObject;
    [SerializeField] AudioClip hitSound;
    [SerializeField] AudioClip healSound;

    CameraManager cameraManager;
    AudioSource audioSource;
    GameObject hpUIObject;
    PlayerMove playerMove;
    ItemData itemData;
    HpUI hpUI;
    int maxHealth=5;
    public int currentHealth;
    int damage;
    int heal;
    bool isInvincible=false; // 무적인가?
    float invincibleTime=2f; // 무적 시간
    int shieldCnt = 0;

    void Start()
    {
        GameManager.instance.RegisterPlayerStat(this);
        GameObject mainCamera;
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        cameraManager = mainCamera.GetComponent<CameraManager>();
        audioSource = GetComponent<AudioSource>();
        hpUIObject = GameObject.FindGameObjectWithTag("Hp");
        hpUI = hpUIObject.GetComponent<HpUI>();
        playerMove = GetComponent<PlayerMove>();
        currentHealth = GameManager.instance.GetHealth();
        shieldCnt = GameManager.instance.GetShield();
        hpUI.ShowHp(currentHealth);
    }

    private void Update()
    {
        if (shieldCnt > 0)
            OnShieldEffect();
        else
            OffShieldEffect();
    }
    public void Damaged(int damage=1)
    {
        audioSource.PlayOneShot(hitSound,1f);
        Debug.Log(currentHealth);
        if (!isInvincible) // 무적이 아닐때만 호출
        {
            audioSource.Play();
            if (shieldCnt > 0)
            {
                shieldCnt -= 1;
                return;
            }
            currentHealth -= damage;
            GameManager.instance.SetHealth(currentHealth);
            cameraManager.OnShakeCamera();
            if (currentHealth <=0)
            {
                playerMove.Dead();
                dashUIObject.SetActive(false);
                isInvincible = true;
                return;
            }
            hpUI.ShowHp(currentHealth);
            StartCoroutine("Invincible");
        }
    }

    public void Heal(int heal)
    {
        audioSource.PlayOneShot(healSound, 1f);
        if (heal + currentHealth > maxHealth)
            currentHealth = maxHealth;
        else
            currentHealth += heal;
        GameManager.instance.SetHealth(currentHealth);
        hpUI.ShowHp(currentHealth);
        healEffect.Play();
    }

    IEnumerator DashInvincible()
    {
        isInvincible = true;
        yield return new WaitForSeconds(0.2f);
        isInvincible = false;
    }

    IEnumerator Invincible()
    {
        isInvincible = true;
        playerMove.OnOffDamaged(true);
        yield return new WaitForSeconds(invincibleTime);
        isInvincible = false;
        playerMove.OnOffDamaged(false);
    }

    public void GetShiled(int _shieldCnt)
    {
        shieldCnt = _shieldCnt;
        GameManager.instance.SetShield(_shieldCnt);
    }
    void OnShieldEffect()
    {
        shiledObject.SetActive(true);
    }
    void OffShieldEffect()
    {
        shiledObject.SetActive(false);
    }

    public void GetConsumerItem(ItemData _itemData)
    {
        itemData = _itemData;
        switch (itemData.consumerType)
        {
            case ConsumerType.Heal:
                Heal(itemData.figure);
                break;
            case ConsumerType.Shield:
                GetShiled(itemData.figure);
                break;
            case ConsumerType.SpeedUp:
                break;
        }
    }
}
