using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEnemyTurnState : PlayerStateBase
{
    public override void Enter(GameObject gameObject)
    {
        SetPlayerStateBase(gameObject);
        SetComponents();
        // 入力を受け付けないようにする
        m_player.SetIsInputRock(true);
    }

    public override void Update()
    {

    }

    public override void Exit()
    {

    }
}
