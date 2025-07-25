using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState
{
    PlayerTurn, // �v���C���[�̃^�[��
    EnemyTurn,  // �G�̃^�[��
    GameOver,   // �Q�[���I�[�o�[
}

[System.Serializable]
public struct Palamata
{
    [SerializeField] public float speed;
    [SerializeField] public float hp;
    [SerializeField] public int attack;
    [SerializeField] public float defence;
}

public class PlayerBase : MonoBehaviour
{
    IPlayerState currentState;
    public PlayerMoveBase m_playerMoveBase;
    public TouchInput m_touchInput;
    public MauseInput m_mauseInput;





    [SerializeField] private Palamata m_palamata;

    /// <summary>
    /// �v���C���[�̏�Ԃ�ύX���郁�\�b�h�B
    /// </summary>
    /// <param name="newState"></param>
    public void ChangeState(PlayerState playerState)
    {
        IPlayerState newState = null;
        switch (playerState)
        {
            case PlayerState.PlayerTurn:
                newState=new PlayerPlayerTurnState();
                break;
            case PlayerState.EnemyTurn:
                //newState=new PlayerEnemyTurnState();
                break;
            case PlayerState.GameOver:
                //newState=new PlayerGameOverState();
                break;
            default:
                break;
        }


        // �Â���Ԃ�Exit�Ă�
        currentState?.Exit();

        // �V������Ԃɐ؂�ւ�
        currentState = newState;

        // �V������Ԃ�Enter�Ă�
        currentState.Enter(this);
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
        currentState = new PlayerPlayerTurnState(); // ������Ԃ�ݒ�
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

    protected void SetPalamata(float speed, float hp, int attack, float defence)
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
