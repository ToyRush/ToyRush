using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] AudioClip[] BGMs;
    [SerializeField] AudioClip winBGM;
    [SerializeField] AudioClip loseBGM;
    private float musicVolume=0.5f;
    private AudioSource audioSource;
    private static SoundManager instance = null;
    public static SoundManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<SoundManager>();
                if (instance == null)
                {
                    GameObject soundManagerObject = new GameObject("SoundManager");
                    instance = soundManagerObject.AddComponent<SoundManager>();
                }
            }
            return instance;
        }
    }

    private void Awake()
    {
        if (instance != null && instance != this) // ½Ì±ÛÅæÀº ÇÏ³ª¸¸ Á¸ÀçÇÏµµ·Ï
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
        audioSource = GetComponent<AudioSource>();
    }

    public void CheckStage(int _stageID)
    {
        switch (_stageID)
        {
            case 0: // ½ºÅä¸®, Æ©Åä¸®¾ó
                PlaySound(0);
                break;
            case 2: // 1Ãþ, 2Ãþ
                PlaySound(1);
                break;
            case 4: // 2.5Ãþ, 3Ãþ
                PlaySound(2);
                break;
            case 6: // º¸½º ½ºÅ×ÀÌÁö
                PlaySound(3);
                break;
            case 7:
                PlaySound(4);
                break;
            default: // Æ©Åä¸®¾ó, 2Ãþ, 3ÃþÀº ±×´ë·Î...
                break;
        }
    }

    public void PlaySound(int _idx)
    {
        audioSource.Stop();
        audioSource.PlayOneShot(BGMs[_idx], musicVolume);
        audioSource.loop = true;
    }

    public void SetVolume(int _volume)
    {
        musicVolume = _volume;
    }

    public void LoseSound()
    {
        audioSource.PlayOneShot(loseBGM, musicVolume);
        audioSource.loop = false;
    }

    public void WinSound()
    {
        audioSource.PlayOneShot(winBGM, musicVolume);
        audioSource.loop = false;
    }
}
