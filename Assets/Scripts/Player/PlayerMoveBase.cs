using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
public class PlayerMoveBase : MonoBehaviour
{
    [SerializeField] private float m_moveSpeed = 100.0f;        //���ˑ��x
    [SerializeField] private float m_adjustmentSpeed = 1.0f;    //�����p�̑��x�W��
    [SerializeField] private float m_drag;                      //��C��R
    [SerializeField] private float m_angularDrag;               //��]�ɑ΂����R

    private Rigidbody2D m_rigidBody;
    private TouchInput m_touchInput;
    private MauseInput m_mouseInput;

   





    public void SetMoveSpeed(float speed)
    {
        m_moveSpeed = speed; //���ˑ��x��ݒ�B
    }

    void Awake()
    {
        m_rigidBody = GetComponent<Rigidbody2D>(); //Rigidbody2D�̎Q�Ƃ��擾�B
        m_touchInput = GetComponent<TouchInput>(); //�^�b�`���͂̎Q�ƁB
        m_mouseInput = GetComponent<MauseInput>(); //�}�E�X���͂̎Q�ƁB

        m_rigidBody.drag=m_drag; //Rigidbody2D�̃h���b�O���擾�B
        m_rigidBody.angularDrag= m_angularDrag; //Rigidbody2D�̊p�x�h���b�O���擾�B
    }

    /// <summary>
    /// �X���C�v�̕����ɗ͂������郁�\�b�h�B
    /// </summary>
    /// <param name="swipeDirection">�X���C�v����</param>
    void AddForce(Vector2 swipeDirection)
    {
        //�X���C�v�����Ƌt�̕����ɔ��ł����悤�ɂ���B
        swipeDirection *= -1;

        //�X���C�v�����ɗ͂�������B
        m_rigidBody.AddForce(
            swipeDirection * m_moveSpeed * m_adjustmentSpeed, 
            ForceMode2D.Impulse
            );

    }

    void FlicLockManager()
    {
        //���b�N���������Ă��Ȃ��Ƃ��B
        if (!m_mouseInput.IsFlickLock())
        {
            if(m_mouseInput.IsDragEnded())
            {
                m_mouseInput.SetFlickLock(true);
            }
        }

        if (!m_touchInput.IsFlickLock())
        {
            if (m_touchInput.IsTouchEnded())
            {
                //�}�E�X�̃h���b�O���I�������A���b�N��������B
                m_touchInput.SetFlickLock(true);
            }
        }


    }

    void Update()
    {

        //�^�b�`���I������u�Ԃ�������B
        if (m_touchInput.IsTouchEnded())
        {
         //�^�b�`�̏I���ʒu����������擾���āA�͂�������B
         AddForce(m_touchInput.GetSwipeEndedDirection());
        }

        //�}�E�X�̃h���b�O���I������u�Ԃ�������B
        if (m_mouseInput.IsDragEnded())
        {
          //�}�E�X�̃h���b�O�I���ʒu����������擾���āA�͂�������B
          AddForce(m_mouseInput.GetSwipeEndedDirection());
        }

        //�{�[���̈ړ����x��������x�Ⴍ�Ȃ�����[���ɂ���B
        if (m_rigidBody.velocity.magnitude <= 1.0f)
        {
            m_rigidBody.velocity = Vector2.zero;
        }

        //���b�N�̊Ǘ����s���B
        FlicLockManager();

    }
}