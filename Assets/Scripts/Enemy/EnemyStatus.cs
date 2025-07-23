using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �G�l�~�[�̎��
/// </summary>
enum EnEnemyType {
    enSmall,
    enBoss,
}


public class EnemyStatus : MonoBehaviour
{
    //HP
    int m_hp = 0;
    // �G�l�~�[�̎��
    EnEnemyType m_enemyType = 0;

    /// <summary>
    /// �_���[�W���󂯂�
    /// </summary>
    /// <param name="damage">�_���[�W��</param>
    void ApplyDamage(int damage)
    {
        m_hp -= damage;
        if (m_hp <= 0)
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// �̗͂���
    /// </summary>
    /// <param name="recovery">�񕜗�</param>
    void Heal(int recovery)
    {
        m_hp += recovery;
        // HP�̏����ݒ肷��ꍇ�͂����Ń`�F�b�N
    }

    /// <summary>
    /// �R���X�g���N�^
    /// </summary>
    /// <param name="hp">����HP</param>
    /// <param name="enemyType">�G�l�~�[�̎��</param>
    EnemyStatus(int hp, EnEnemyType enemyType)
    {
        m_hp = hp;
        m_enemyType = enemyType;
        SizeSetting(m_enemyType);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    /// Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// �G�l�~�[�̃T�C�Y��ݒ�
    /// </summary>
    /// <param name="type">�G�l�~�[�̎��</param>
    void SizeSetting(EnEnemyType type)
    {
        switch (type) {
            case EnEnemyType.enSmall:
                transform.localScale = new Vector3(1f, 1f, 1f);
                break;
            case EnEnemyType.enBoss:
                transform.localScale = new Vector3(2f, 2f, 2f);
                break;
            default:
                break;
        }
    }
}
