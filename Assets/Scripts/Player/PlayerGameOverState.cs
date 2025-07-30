using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGameOverState : PlayerStateBase
{
    public override void Enter(GameObject gameObject)
    {
        SetPlayerStateBase(gameObject);
        SetComponents();
        // “ü—Í‚ðŽó‚¯•t‚¯‚È‚¢‚æ‚¤‚É‚·‚é
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
