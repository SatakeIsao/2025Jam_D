using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOtherPlayerTurnState : PlayerStateBase
{
    public override void Enter(GameObject gameObject)
    {
        SetPlayerStateBase(gameObject);
        SetComponents();
        // ���͂��󂯕t����悤�ɂ���
        m_player.SetIsInputRock(false);
    }

    public override void Update()
    {

    }

    public override void Exit()
    {

    }
}
