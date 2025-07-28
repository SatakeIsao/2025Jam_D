using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPlayerTurnState : PlayerStateBase
{
    public override void Enter(GameObject gameObject)
    {
        Debug.Log("�n���ꂽ gameObject = " + gameObject.name);
        SetPlayerStateBase(gameObject);
        SetComponents();
        // ���͂��󂯕t����悤�ɂ���
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
        //TODO:�G�ɓ��������Ƃ��ɓG�ɍU�����鏈���������B
        EnemyStatus enemyStatus = gameObject.GetComponent<EnemyStatus>();
        if (enemyStatus != null)
        {
            enemyStatus.ApplyDamage(attack); // �v���C���[�̍U��������������_���[�W��^����
        }

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject other = collision.gameObject;
        if (other.tag == "Enemy")
        {
            // �G�ƂԂ�������_���[�W��^����
            Attack(other,m_player.GetPalamata().attack);
        }
    }
}
