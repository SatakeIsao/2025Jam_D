using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerStateBase : IPlayerState
{
    protected PlayerBase m_player; // どのステートからもアクセスできる

    public void SetPlayerStateBase(PlayerBase player)
    {
        this.m_player = player;
    }

    public abstract void Enter(PlayerBase player);
    public abstract void Update();
    public abstract void Exit();
}
