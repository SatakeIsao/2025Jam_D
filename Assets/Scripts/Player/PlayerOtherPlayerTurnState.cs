using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOtherPlayerTurnState : PlayerStateBase
{
    public override void Enter(GameObject gameObject)
    {
        SetPlayerStateBase(gameObject);
        SetComponents();
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
