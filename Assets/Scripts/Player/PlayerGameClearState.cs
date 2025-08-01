using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGameClearState : PlayerStateBase
{
    public override void Enter(GameObject gameObject)
    {
        SetPlayerStateBase(gameObject);
        SetComponents();
        // 入力を受け付けないようにする
        m_player.SetIsInputRock(true);
        AudioManager.Instance.PlaySE(AudioManager.SEType.enGameClear);
    }

    public override void Update()
    {
        m_playerMoveBase.GameClearMove();
    }

    public override void Exit()
    {

    }
}
