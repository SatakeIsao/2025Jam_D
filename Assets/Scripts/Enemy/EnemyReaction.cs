using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyReaction : MonoBehaviour
{
    EnemyStatus enemyStatus; // �G�̃X�e�[�^�X

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        JudgeUnderAttack();
    }

    /// <summary>
    /// �v���C���[�̍U���Ƀq�b�g������
    /// </summary>
    void JudgeUnderAttack()
    {

    }

    /// <summary>
    /// ���S
    /// </summary>
    void JudgeDeath()
    {
        if (enemyStatus)
        {

        }
    }
}
