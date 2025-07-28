using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum PlayerState
{
    enLocalPlayerTurn,    // �����̃^�[��
    enOtherPlayerTurn,    // �ʃv���C���[�̃^�[��
    enEnemyTurn,  // �G�̃^�[��
    enGameOver,   // �Q�[���I�[�o�[
    enGameClear,  // �Q�[���N���A
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
    int m_damageTaken = 0; // �󂯂��_���[�W�̍��v
    [SerializeField] private Palamata m_palamata;
    public event Action <PlayerBase> OnDamaged;// �_���[�W���󂯂��Ƃ��̃C�x���g�B


    private void AddDamage(int damage)
    {
        m_damageTaken += damage;
        OnDamaged?.Invoke(this); // �_���[�W���󂯂��Ƃ��̃C�x���g���Ăяo���B
    }

    /// <summary>
    /// �v���C���[�̏�Ԃ�ύX���郁�\�b�h�B
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


        // �Â���Ԃ�Exit�Ă�
        currentState?.Exit();

        // �V������Ԃɐ؂�ւ�
        currentState = newState;

        // �V������Ԃ�Enter�Ă�
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
    /// �C���v�b�g�����b�N���邩�ǂ�����ݒ肷�郁�\�b�h�B
    /// </summary>
    /// <param name="isInputRock"></param>
    /// <returns></returns>
    public void SetIsInputRock(bool isInputRock)
    {
        // �^�b�`���͂��󂯕t����
        if (m_touchInput != null)
        {
            m_touchInput.SetFlickLock(isInputRock);
        }
        // �}�E�X���͂��󂯕t����
        if (m_mauseInput != null)
        {
            m_mauseInput.SetFlickLock(isInputRock);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        ChangeState(PlayerState.enLocalPlayerTurn);
        //currentState = new PlayerPlayerTurnState(); // ������Ԃ�ݒ�
        m_playerMoveBase =GetComponent<PlayerMoveBase>();
        m_touchInput = GetComponent<TouchInput>();
        m_mauseInput = GetComponent<MauseInput>();

        ApplySpead();
    }

    /// <summary>
    /// �X�s�[�h�����ۂ̋����ɔ��f������B
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
        //�X�s�[�h�𓮂��Ƀ{�[�����[�u�N���X�ɔ��f�B
        ApplySpead();
    }

    // Update is called once per frame
    void Update()
    {
        currentState.Update();
    }
}
