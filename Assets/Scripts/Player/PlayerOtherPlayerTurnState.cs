using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOtherPlayerTurnState : PlayerStateBase
{
    public override void Enter(PlayerBase player)
    {
        SetPlayerStateBase(player);
        // 入力を受け付けるようにする
        m_player.SetIsInputRock(false);
    }

    public override void Update()
    {

    }

    public override void Exit()
    {

    }
}
