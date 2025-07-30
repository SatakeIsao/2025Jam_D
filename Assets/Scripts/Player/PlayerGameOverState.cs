using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGameOverState : PlayerStateBase
{
    public override void Enter(GameObject gameObject)
    {
        SetPlayerStateBase(gameObject);
        SetComponents();
        // ���͂��󂯕t���Ȃ��悤�ɂ���
        m_player.SetIsInputRock(true);
        AudioManager.Instance.PlaySE(AudioManager.SEType.enGameOver);

    }

    public override void Update()
    {
        m_playerMoveBase.GameOverMove();
    }

    public override void Exit()
    {

    }
}
