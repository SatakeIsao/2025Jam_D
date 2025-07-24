using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
public class BallMove : MonoBehaviour
{
    [SerializeField]
    protected float m_moveSpeed = 100.0f; //���ˑ��x
    [SerializeField]
    protected float m_adjustmentSpeed = 1.0f; //�����p�̑��x�W��
    protected Rigidbody2D m_rigidBody;  //Rigidbody2D�ւ̎Q��
    //�}�E�X�̃h���b�O�֘A�̕ϐ�
    private Vector2 m_dragStartPos;     //�h���b�O�J�n�ʒu
    private Vector2 m_dragEndPos;       //�h���b�O�I���ʒu
    private bool m_isDragging = false;  //�h���b�O�����ǂ���
    protected virtual void Awake()
    {
        m_rigidBody = GetComponent<Rigidbody2D>();
        m_rigidBody.drag = 0.2f;        //��C��R
        m_rigidBody.angularDrag = 1.0f;//��]�ɑ΂����R
    }
    //�}�E�X�{�^���������ꂽ���̏���
    private void OnMouseDown()
    {
        if (Input.GetMouseButtonDown(0)) //���{�^��
        {
            m_isDragging = true;
            //�}�E�X�̃X�N���[�����W�����[���h��Ԃɕϊ����ĕۑ�
            m_dragStartPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            //�h���b�O�J�n���Ɍ��݂̑��x�����Z�b�g
            if (m_rigidBody != null)
            {
                m_rigidBody.velocity = Vector2.zero;
                m_rigidBody.angularVelocity = 0.0f; //��]�����Z�b�g
            }
        }
    }
    //�}�E�X�{�^����������Ă���Ԃ̏���
    void OnMouseDrag()
    {
        if (m_isDragging
&& Input.GetMouseButton(0)) //�h���b�O���ō��{�^��������Ă����
        {
            //���݂̃}�E�X�̃��[���h���W���X�V
            m_dragEndPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            //���������Ă�������̃x�N�g��
            Vector2 dragVector = m_dragStartPos - m_dragEndPos;
        }
    }
    //�}�E�X�{�^���������ꂽ���̏���
    private void OnMouseUp()
    {
        if (m_isDragging
&& Input.GetMouseButtonUp(0))//�h���b�O���ō��{�^���������ꂽ�u��
        {
            m_isDragging = false;
            m_dragEndPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            //�}�E�X�̃h���b�O�J�n�n�_����I���n�_�܂ł̃x�N�g�����v�Z
            Vector2 launchDirection = (m_dragStartPos - m_dragEndPos).normalized;   //�����𐳋K��
            //�������鋗���Ɋւ�炸�ɁA�Œ�̗͂Ŕ���
            m_rigidBody.AddForce(launchDirection * m_moveSpeed * m_adjustmentSpeed, ForceMode2D.Impulse);
        }
    }
}