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
    bool m_hasPulled = false; //�������������ǂ����̃t���O
    float m_jumpTime = 0.0f; //�W�����v�̎��Ԃ��Ǘ�����ϐ�
    float m_gameOverMoveTime = 0.0f; //�Q�[���I�[�o�[���̃W�����v���Ԃ��Ǘ�����ϐ��B
    bool IsStartJump = true;
    bool IsStartGameOverMove = true; //�Q�[���I�[�o�[���̓������J�n���ꂽ���ǂ����̃t���O�B
    Vector3 m_startJumpPos;
    Vector3 m_startGameOverPos; //�Q�[���I�[�o�[���̊J�n�ʒu�B

    public void SetDrag(float dorag)
    {
        m_rigidBody.drag = dorag; //Rigidbody2D�̃h���b�O���X�V�B
    }

    public void GameClearMove()
    {
        if (!IsStartJump) {
            m_startJumpPos = transform.position;
            IsStartJump = false;
        }
       

       m_jumpTime+= 1.0f; //�W�����v�̎��Ԃ��X�V�B
        transform.position = new Vector3(transform.position.x, m_startJumpPos.y+ Mathf.Abs(Mathf.Sin(m_jumpTime*0.02f)), transform.position.z);
    }

    public void GameOverMove()
    {
        //�Q�[���I�[�o�[���̓����B
        if (!IsStartGameOverMove)
        {
            m_startGameOverPos = transform.position;
            IsStartGameOverMove = false;
        }

        m_gameOverMoveTime += 1.0f; //�W�����v�̎��Ԃ��X�V�B
        if(m_gameOverMoveTime * 0.02f <= 3.141592f)
        {
            transform.position = new Vector3(transform.position.x, m_startJumpPos.y + (Mathf.Sin(m_gameOverMoveTime * 0.02f) * 1.5f), transform.position.z);
        }
    }

    public float GedMenbaDrag()
    {
        return m_drag; //Rigidbody2D�̃h���b�O���擾�B
    }
    /// <summary>
    /// ������������Ɏ~�܂������ǂ����H
    /// </summary>
    /// <returns></returns>
    public bool HasStoppedAfterPull()
    {
        //������������Ɏ~�܂������ǂ������`�F�b�N����B
        if (m_hasPulled)
        {
            m_hasPulled = false; //�t���O�����Z�b�g�B
            if (GetIsStop()) return true;
        }
            return false;
    }

    /// <summary>
    /// ���~�܂��Ă��邩�ǂ����H
    /// </summary>
    /// <returns></returns>
    public bool GetIsStop()
    {
        if (m_rigidBody.velocity.magnitude<= 0)
        {
            return true; //�{�[���̑��x��0�ȉ��Ȃ�true��Ԃ��B
        }
        else
        {
            return false; //�{�[���̑��x��0���傫���Ȃ�false��Ԃ��B
        }
    }

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

    void Start()
    {
        //���������ĕ�������A�Ă΂��C�x���g�Ɋ֐���ǉ��B
        m_mouseInput.OnDragEnded += () => {
            //���b�N���������Ă��Ȃ��Ƃ��A�}�E�X�̃h���b�O���I�������A���b�N��������B
            if (!m_mouseInput.IsFlickLock()|| !m_touchInput.IsFlickLock()) m_hasPulled = true;
        };
        m_touchInput.OnDragEnded += () =>
        {
            //���b�N���������Ă��Ȃ��Ƃ��A�^�b�`�̃h���b�O���I�������A���b�N��������B
            if (!m_mouseInput.IsFlickLock() || !m_touchInput.IsFlickLock()) m_hasPulled = true;
        };
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

    //void FlicLockManager()
    //{
    //    //���b�N���������Ă��Ȃ��Ƃ��B
    //    if (!m_mouseInput.IsFlickLock())
    //    {
    //        if(m_mouseInput.IsDragEnded())
    //        {
    //            //TODO: ���̓C���v�b�g����isFlickLock�����b�N�̎哱���������Ă��邪�A���Ԃ������
    //            //���[�u�����哱�������悤�ɂ���B
    //            m_mouseInput.SetFlickLock(true);

    //        }
    //    }

    //    if (!m_touchInput.IsFlickLock())
    //    {
    //        if (m_touchInput.IsTouchEnded())
    //        {
    //            //�}�E�X�̃h���b�O���I�������A���b�N��������B
    //            m_touchInput.SetFlickLock(true);
    //        }
    //    }


    //}

    void Update()
    {
        //�^�b�`���I������u�Ԃ�������B
        if (m_touchInput.HasJustReleased())
        {
         //�^�b�`�̏I���ʒu����������擾���āA�͂�������B
         AddForce(m_touchInput.GetSwipeEndedDirection());
        }

        //�}�E�X�̃h���b�O���I������u�Ԃ�������B
        if (m_mouseInput.HasJustReleased())
        {
          //�}�E�X�̃h���b�O�I���ʒu����������擾���āA�͂�������B
          AddForce(m_mouseInput.GetSwipeEndedDirection());
        }

        //�{�[���̈ړ����x��������x�Ⴍ�Ȃ�����[���ɂ���B
        if (m_rigidBody.velocity.magnitude <= 1.0f)
        {
            m_rigidBody.velocity = Vector2.zero;
        }


        //TODO�F�^�[���}�l�[�W���[�Ń��b�N�̊Ǘ����s���悤�ɂ���B
        ////���b�N�̊Ǘ����s���B
        //FlicLockManager();

    }
}