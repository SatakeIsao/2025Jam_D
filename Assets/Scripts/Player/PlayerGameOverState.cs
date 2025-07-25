using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGameOverState : PlayerStateBase
{
    public override void Enter(PlayerBase player)
    {
        SetPlayerStateBase(player);
        // “ü—Í‚ðŽó‚¯•t‚¯‚é‚æ‚¤‚É‚·‚é
        m_player.SetIsInputRock(false);
    }

    public override void Update()
    {

    }

    public override void Exit()
    {

    }
}
