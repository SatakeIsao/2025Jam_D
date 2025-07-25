using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;

enum EnChargeTarget
{
    enToWayPoint, //�E�F�C�|�C���g�֓ːi
    enToNewPos, //�����ʒu�֓ːi
    enEmpty 
}


public class EnemyCharge : MonoBehaviour
{
    EnemyStatus enemyStatus;
    CircleCollider2D m_chargeAttackCollider;

    //�_���[�W�i���t���[���j
    [SerializeField] private float m_chargeDamage = 0.0f;
    //�U���^�[����
    [SerializeField] private int m_turnNum = 0;
    //���݂̎c��^�[����
    private int m_restTurnNum = 0;
    //�E�F�C�|�C���g�̈ʒu�i�c�j
    [SerializeField] private EnSmallPosVertical wayVer = EnSmallPosVertical.enEmpty;
    //�E�F�C�|�C���g�̈ʒu�i���j
    [SerializeField] private EnSmallPosHorizontal wayHor = EnSmallPosHorizontal.enEmpty;
    //��x�̓ːi�ł̈ړ�����
    private const float m_chargeDistance = 2.0f;
    //�ːi�̖ڕW�ʒu�̏��
    private EnChargeTarget m_chargeTarget = EnChargeTarget.enEmpty;
    //�ːi�̖ڕW�ʒu
    private Vector2 m_targetPos = Vector2.zero;
    //1�t���[���̈ړ��ʁi�{���j
    private const float m_moveMagnification = 0.02f;
    //��x�� movement �ł̈ړ���
    private Vector2 m_moveAmount = Vector2.zero;
    //���݂̓ːi�U���œ�������
    private int m_restMoveNum = 0;
    //���ݓ������x�N�g�����L�^
    private Vector2 m_moveVecMemory = Vector2.zero;
    //��~����
    private const float STOP_TIME = 0.3f;
    //���݂̒�~����
    private float m_currentStopTime = 0.0f;
    //�s������
    private bool m_isInAction = false;

    /// <summary>
    /// �^�[���̃J�E���g�_�E���i�^�[������0�ɂȂ�����ːi���s���j
    /// </summary>
    public void TurnCount()
    {
        m_restTurnNum--;

        if(m_restTurnNum <= 0)
        {
            m_isInAction = true; //�s���I���t���O�����Z�b�g
            m_currentStopTime = 0.0f; //��~���Ԃ����Z�b�g
            m_moveVecMemory = Vector2.zero; //�ړ������x�N�g�������Z�b�g
            CalcMoveAmount(); //��x�̓ːi�œ����������v�Z
        }
    }

    /// <summary>
    /// �s�������`�F�b�N
    /// </summary>
    /// <returns>�s�����t���O</returns>
    public bool GetIsInAction()
    {
        return m_isInAction; //�s�������ǂ�����Ԃ�
    }


    // Start is called before the first frame update
    void Start()
    {
        enemyStatus = GetComponent<EnemyStatus>();
        //�ːi�U���p�̃R���C�_�[��ǉ�
        gameObject.AddComponent<CircleCollider2D>();
        m_chargeAttackCollider = GetComponent<CircleCollider2D>();
        m_chargeAttackCollider.radius = 1.2f;

        m_chargeTarget = EnChargeTarget.enToWayPoint; //�ːi�̖ڕW�ʒu���E�F�C�|�C���g�֐ݒ�
    }

    // Update is called once per frame
    void Update()
    {
        //debug
        if (!m_isInAction)
        {
            m_isInAction = true; //�s���I���t���O�����Z�b�g
            m_currentStopTime = 0.0f; //��~���Ԃ����Z�b�g
            m_moveVecMemory = Vector2.zero; //�ړ������x�N�g�������Z�b�g
            CalcMoveAmount(); //��x�̓ːi�œ����������v�Z
        }

        m_currentStopTime += Time.deltaTime; //���݂̒�~���Ԃ��X�V
        if (m_currentStopTime < STOP_TIME)
        {
            m_chargeAttackCollider.enabled = true;

            return; //��~���Ԃ��o�߂��Ă��Ȃ��̂ŁA�ːi���Ȃ�
        }

        m_chargeAttackCollider.enabled = false;

        if (m_restMoveNum > 0)
        {
            ChargeExecution();
        }
        else
        {
            TurnEnd();
        }
    }

    /// <summary>
    /// �ːi���s
    /// </summary>
    private void ChargeExecution()
    {
        transform.Translate(m_moveAmount * m_moveMagnification);
        m_moveVecMemory += m_moveAmount * m_moveMagnification; //�ړ������x�N�g�����L�^

        if (m_moveVecMemory.magnitude >= m_moveAmount.magnitude)
        {
            m_moveVecMemory = Vector2.zero; //�ړ������x�N�g�������Z�b�g
            m_currentStopTime = 0.0f; //��~���Ԃ����Z�b�g
            m_restMoveNum--;
        }        
    }

    /// <summary>
    /// ��x�� move �œ����������v�Z
    /// </summary>
    private void CalcMoveAmount()
    {
        //�ːi�̖ڕW�ʒu��ݒ�
        if (m_chargeTarget == EnChargeTarget.enToWayPoint)
        {
            m_targetPos = enemyStatus.GetPosition((int)wayVer, (int)wayHor);
        }
        else if(m_chargeTarget == EnChargeTarget.enToNewPos)
        {
            m_targetPos = enemyStatus.GetNewPos();
        }

        Vector2 chargeVec = m_targetPos - (Vector2)transform.position;
        int moveNum = (int)Math.Ceiling(chargeVec.magnitude / m_chargeDistance); //�ړ�����񐔂��v�Z�i�[���؂�グ�� int �ɑ���j
        Vector2 moveAmount = chargeVec / moveNum;
        m_moveAmount = moveAmount;
        m_restMoveNum = moveNum; //�c��̈ړ��񐔂��Z�b�g
    }

    /// <summary>
    /// �ːi�I�����̏���
    /// </summary>
    private void TurnEnd()
    {
        m_isInAction = false; //�ːi���I��
        m_restTurnNum = m_turnNum; //�c��̃^�[���������Z�b�g

        //�ːi�ڕW�ʒu��ς���
        if (m_chargeTarget == EnChargeTarget.enToWayPoint)
        {
            m_chargeTarget = EnChargeTarget.enToNewPos; //�E�F�C�|�C���g���珉���ʒu��
        }
        else if (m_chargeTarget == EnChargeTarget.enToNewPos)
        {
            m_chargeTarget = EnChargeTarget.enToWayPoint; //�����ʒu����E�F�C�|�C���g��
        }
    }

}
