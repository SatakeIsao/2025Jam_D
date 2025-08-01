using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPlayerTurnState : PlayerStateBase
{
    public override void Enter(GameObject gameObject)
    {
        Debug.Log("渡された gameObject = " + gameObject.name);
        Debug.Log("プレイヤーのターンです");
        SetPlayerStateBase(gameObject);
        SetComponents();
        // 入力を受け付けるようにする
        m_player.SetIsInputRock(false);
    }

    public override void Update()
    {
        if (m_mauseInput != null)
        {
            if (m_mauseInput.HasJustReleased())
            {
                m_player.SetIsInputRock(true);
            }
        }
        else if (m_touchInput != null)
        {
            if (m_touchInput.HasJustReleased())
            {
                m_player.SetIsInputRock(true);
            }
        }
    }

    public override void Exit()
    {

    }

    void Attack(GameObject gameObject,int attack)
    {
        //TODO:敵に当たったときに敵に攻撃する処理を書く。
        EnemyStatus enemyStatus = gameObject.GetComponent<EnemyStatus>();
        if (enemyStatus != null)
        {
            enemyStatus.ApplyDamage(attack); // プレイヤーの攻撃が当たったらダメージを与える
        }

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject other = collision.gameObject;
        if (other.tag == "Enemy")
        {
            // 敵とぶつかったらダメージを与える
            Attack(other,m_player.GetPalamata().attack);
        }
    }
}
