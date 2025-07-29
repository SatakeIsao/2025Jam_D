using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [SerializeField] private AudioSource bgmSource;
    [SerializeField] private AudioSource seSource;

    [SerializeField] private AudioClip[] seClips;
    [SerializeField] private AudioClip[] bgmClips;

    public enum SEType
    {
        enButtonClick,       // �{�^���N���b�N
        enEnemyDamage,       // �G�_���[�W
        enGameClear,         // �Q�[���N���A
        enGameOver,          // �Q�[���I�[�o�[
        enGetItem,           // �A�C�e���擾
        enPlayerDamage,      // �v���C���[�_���[�W
        enReflection,        // ���ˉ�
        enRetry,             // ���g���C
    }
    public enum BGMType
    {
        enInGame,          // �Q�[����BGM
        enTitle,           // �^�C�g��BGM
    }



    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // �V�[�����܂����Ŏg����
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlaySE(SEType se)
    {
        if (seClips != null && (int)se < seClips.Length)
        {
            seSource.PlayOneShot(seClips[(int)se]);
        }
    }

    public void PlayBGM(BGMType bgm)
    {
        if (bgmClips != null && (int)bgm < bgmClips.Length)
        {
            bgmSource.clip = bgmClips[(int)bgm];
            bgmSource.Play();
        }
    }

    public void StopBGM()
    {
        bgmSource.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
