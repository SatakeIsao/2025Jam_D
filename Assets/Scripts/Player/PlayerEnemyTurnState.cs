using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEnemyTurnState : PlayerStateBase
{
    public override void Enter(PlayerBase player)
    {
        SetPlayerStateBase(player);
        // ���͂��󂯕t����悤�ɂ���
        m_player.SetIsInputRock(true);
    }

    public override void Update()
    {

    }

    public override void Exit()
    {

    }
}
