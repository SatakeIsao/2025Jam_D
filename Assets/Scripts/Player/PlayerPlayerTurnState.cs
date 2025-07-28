using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPlayerTurnState : PlayerStateBase
{
    public override void Enter(GameObject gameObject)
    {
        Debug.Log("“n‚³‚ê‚½ gameObject = " + gameObject.name);
        SetPlayerStateBase(gameObject);
        SetComponents();
        // “ü—Í‚ğó‚¯•t‚¯‚é‚æ‚¤‚É‚·‚é
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
        //TODO:“G‚É“–‚½‚Á‚½‚Æ‚«‚É“G‚ÉUŒ‚‚·‚éˆ—‚ğ‘‚­B
        EnemyStatus enemyStatus = gameObject.GetComponent<EnemyStatus>();
        if (enemyStatus != null)
        {
            enemyStatus.ApplyDamage(attack); // ƒvƒŒƒCƒ„[‚ÌUŒ‚‚ª“–‚½‚Á‚½‚çƒ_ƒ[ƒW‚ğ—^‚¦‚é
        }

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject other = collision.gameObject;
        if (other.tag == "Enemy")
        {
            // “G‚Æ‚Ô‚Â‚©‚Á‚½‚çƒ_ƒ[ƒW‚ğ—^‚¦‚é
            Attack(other,m_player.GetPalamata().attack);
        }
    }
}
