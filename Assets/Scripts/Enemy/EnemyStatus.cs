using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices.WindowsRuntime;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

/// <summary>
/// �G�l�~�[�̎��
/// </summary>
enum EnEnemyType {
    enSmall,
    enBoss,
    enEmpty,
}

/// <summary>
/// �G���G�̏c���W�̃p�^�[��
/// </summary>
enum EnSmallPosVertical { 
    enTop,
    enMiddleTop,
    enMiddle,
    enMiddleBottom,
    enBottom,
    enEmpty,
}

/// <summary>
/// �G���G�̉����W�̃p�^�[��
/// </summary>
enum EnSmallPosHorizontal {
    enLeft,
    enMiddle,
    enRight,
    enEmpty,
}



public class EnemyStatus : MonoBehaviour
{    
    //HP
    public int m_HP = 0;
    //�ő�HP
    [SerializeField] public int m_maxHP = 0;
    //�����ʒu�i�c�j
    [SerializeField] private EnSmallPosVertical m_newPositionVer = EnSmallPosVertical.enEmpty;
    //�����ʒu
    Vector2 m_newPosition = Vector2.zero;

    /// <summary>
    /// �����ʒu���擾
    /// </summary>
    /// <returns>�����ʒu</returns>
    public Vector2 GetNewPos()
    {
        return m_newPosition;
    }

    /// �_���[�W���󂯂�
    /// </summary>
    /// <param name="damage">�_���[�W��</param>
    public void ApplyDamage(int damage)
    {
        m_HP -= damage;
        //HP��0�����ɂ��Ȃ�
        if (m_HP <= 0) {
            m_HP = 0;
        }
    }

    /// <summary>
    /// �̗͂���
    /// </summary>
    /// <param name="recovery">�񕜗�</param>
    public void Heal(int recovery)
    {
        m_HP += recovery;
        //HP���ő�l�𒴂��Ȃ��悤�ɂ���
        if (m_HP > m_maxHP) {
            m_HP = m_maxHP;
        }
    }

    /// <summary>
    /// HP���擾
    /// </summary>
    public int HP
    {
        get
        {
            return m_HP;
        }
    }

    /// <summary>
    /// �R���X�g���N�^
    /// </summary>
    /// <param name="hp">����HP</param>
    /// <param name="enemyType">�G�l�~�[�̎��</param>
    //EnemyStatus(int hp, EnEnemyType enemyType)
    //{
    //    m_hp = hp;
    //    m_enemyType = enemyType;
    //    SizeSetting(m_enemyType);
    //}

    // Start is called before the first frame update
    void Start()
    {
        m_HP = m_maxHP; // ����HP���ő�HP�ɐݒ�

        //�����ʒu���Z�b�g
        m_newPosition = (Vector2)transform.position;
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
