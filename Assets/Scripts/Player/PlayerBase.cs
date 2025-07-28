using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum PlayerState
{
    enLocalPlayerTurn,    // 自分のターン
    enOtherPlayerTurn,    // 別プレイヤーのターン
    enEnemyTurn,  // 敵のターン
    enGameOver,   // ゲームオーバー
    enGameClear,  // ゲームクリア
}

[System.Serializable]
public struct Palamata
{
    [SerializeField] public float speed;
    [SerializeField] public int hp;
    [SerializeField] public int attack;
    [SerializeField] public float defence;
}

public class PlayerBase : MonoBehaviour
{
    IPlayerState currentState;
    public PlayerMoveBase m_playerMoveBase;
    public TouchInput m_touchInput;
    public MauseInput m_mauseInput;
    int m_damageTaken = 0; // 受けたダメージの合計
    [SerializeField] private Palamata m_palamata;
    public event Action <PlayerBase> OnDamaged;// ダメージを受けたときのイベント。


    private void AddDamage(int damage)
    {
        m_damageTaken += damage;
        OnDamaged?.Invoke(this); // ダメージを受けたときのイベントを呼び出す。
    }

    /// <summary>
    /// プレイヤーの状態を変更するメソッド。
    /// </summary>
    /// <param name="newState"></param>
    public void ChangeState(PlayerState playerState)
    {
        IPlayerState newState = null;
        switch (playerState)
        {
            case PlayerState.enLocalPlayerTurn:
                newState=new PlayerPlayerTurnState();
                break;
            case PlayerState.enOtherPlayerTurn:
                newState = new PlayerOtherPlayerTurnState();
                break;
            case PlayerState.enGameOver:
                newState = new PlayerGameOverState();
                break;
            case PlayerState.enEnemyTurn:
                newState = new PlayerEnemyTurnState();
                break;
            case PlayerState.enGameClear:
                newState = new PlayerGameClearState();
                break;
            default:
                break;
        }


        // 古い状態のExit呼ぶ
        currentState?.Exit();

        // 新しい状態に切り替え
        currentState = newState;

        // 新しい状態のEnter呼ぶ
        currentState.Enter(this.gameObject);
    }

    public int GetDamagae()
    {
        return m_damageTaken;
    }

    public Palamata GetPalamata()
    {
        return m_palamata;
    }

    public PlayerMoveBase GetPlayerMoveBase()
    {
        return m_playerMoveBase;
    }

    /// <summary>
    /// インプットをロックするかどうかを設定するメソッド。
    /// </summary>
    /// <param name="isInputRock"></param>
    /// <returns></returns>
    public void SetIsInputRock(bool isInputRock)
    {
        // タッチ入力を受け付ける
        if (m_touchInput != null)
        {
            m_touchInput.SetFlickLock(isInputRock);
        }
        // マウス入力を受け付ける
        if (m_mauseInput != null)
        {
            m_mauseInput.SetFlickLock(isInputRock);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        ChangeState(PlayerState.enLocalPlayerTurn);
        //currentState = new PlayerPlayerTurnState(); // 初期状態を設定
        m_playerMoveBase =GetComponent<PlayerMoveBase>();
        m_touchInput = GetComponent<TouchInput>();
        m_mauseInput = GetComponent<MauseInput>();

        ApplySpead();
    }

    /// <summary>
    /// スピードを実際の挙動に反映させる。
    /// </summary>
    void ApplySpead()
    {
        m_playerMoveBase.SetMoveSpeed(m_palamata.speed);
    }

    protected void SetPalamata(float speed, int hp, int attack, float defence)
    {
        m_palamata.speed = speed;
        m_palamata.hp = hp;
        m_palamata.attack = attack;
        m_palamata.defence = defence;
        //スピードを動きにボールムーブクラスに反映。
        ApplySpead();
    }

    // Update is called once per frame
    void Update()
    {
        currentState.Update();
    }
}
