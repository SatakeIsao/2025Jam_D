using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOtherPlayerTurnState : PlayerStateBase
{
    public override void Enter(GameObject gameObject)
    {
        Debug.Log("別のプレイヤーのターンです");
        SetPlayerStateBase(gameObject);
        SetComponents();
        // 入力を受け付け無いようにする
        m_player.SetIsInputRock(true);
        m_playerMoveBase.SetDrag(800.0f);

    }

    public override void Update()
    {
       
    }

    public override void Exit()
    {
        m_playerMoveBase.SetDrag(m_playerMoveBase.GedMenbaDrag());
    }
}
