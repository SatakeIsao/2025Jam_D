using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGameClearState : PlayerStateBase
{
    public override void Enter(GameObject gameObject)
    {
        SetPlayerStateBase(gameObject);
        SetComponents();
        // ���͂��󂯕t���Ȃ��悤�ɂ���
        m_player.SetIsInputRock(true);
    }

    public override void Update()
    {
        m_playerMoveBase.GameClearMove();
    }

    public override void Exit()
    {

    }
}
