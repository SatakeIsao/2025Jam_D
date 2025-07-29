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
        enButtonClick,       // ボタンクリック
        enEnemyDamage,       // 敵ダメージ
        enGameClear,         // ゲームクリア
        enGameOver,          // ゲームオーバー
        enGetItem,           // アイテム取得
        enPlayerDamage,      // プレイヤーダメージ
        enReflection,        // 反射音
        enRetry,             // リトライ
    }
    public enum BGMType
    {
        enInGame,          // ゲーム内BGM
        enTitle,           // タイトルBGM
    }



    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // シーンをまたいで使える
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
