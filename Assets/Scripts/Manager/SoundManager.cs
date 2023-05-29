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
        if (instance != null && instance != this) // �̱����� �ϳ��� �����ϵ���
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
            case 0: // ���丮, Ʃ�丮��
                PlaySound(0);
                break;
            case 2: // 1��, 2��
                PlaySound(1);
                break;
            case 4: // 2.5��, 3��
                PlaySound(2);
                break;
            case 6: // ���� ��������
                PlaySound(3);
                break;
            case 7:
                PlaySound(4);
                break;
            default: // Ʃ�丮��, 2��, 3���� �״��...
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
