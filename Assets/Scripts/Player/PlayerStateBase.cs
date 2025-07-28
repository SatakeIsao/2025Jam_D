using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerStateBase : IPlayerState
{
    protected GameObject m_playerGameObject; // どのステートからもアクセスできる

    public void SetPlayerStateBase(GameObject gameObject)
    {
        this.m_playerGameObject = gameObject;
    }
    protected PlayerBase m_player;
    protected PlayerMoveBase m_playerMoveBase;
    protected MauseInput m_mauseInput;
    protected TouchInput m_touchInput;
    public void SetComponents()
    {
        m_player = m_playerGameObject.GetComponent<PlayerBase>();
        m_playerMoveBase = m_playerGameObject.GetComponent<PlayerMoveBase>();
        m_mauseInput = m_playerGameObject.GetComponent<MauseInput>();
        m_touchInput = m_playerGameObject.GetComponent<TouchInput>();
    }


    public abstract void Enter(GameObject gameObject);
    public abstract void Update();
    public abstract void Exit();
}
