using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPlayerTurnState : PlayerStateBase
{
    public override void Enter(PlayerBase player)
    {
        SetPlayerStateBase(player);
        // ���͂��󂯕t����悤�ɂ���
        m_player.SetIsInputRock(false);
    }

    public override void Update()
    {

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
