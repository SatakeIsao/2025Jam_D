using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MauseInput : MonoBehaviour
{

    //�}�E�X�̃h���b�O�֘A�̕ϐ�
    private Vector2 m_dragStartPos = Vector2.zero;     //�h���b�O�J�n�ʒu
    private Vector2 m_dragCurrentPos = Vector2.zero;   //�h���b�O���̌��݈ʒu
    private bool m_isDragging = false;  //�h���b�O�����ǂ���
    bool m_isFlickLock = false; //�{�[���̈ړ������b�N���邩�ǂ����̃t���O
    public event Action OnDragStarted; //�h���b�O�J�n���̃C�x���g
    public event Action OnDragEnded;
    public event Action<float> OnArrowLengthUpdated; //�h���b�O���̃C�x���g
    public event Action<float> OnArrowRotationUpdated; //�h���b�O���̃C�x���g


    public bool IsFlickLock()
    {         //�{�[���̈ړ������b�N����Ă��邩�ǂ�����Ԃ��B
        return m_isFlickLock;
    }

    /// <summary>
    /// �{�[���̃��b�N��ݒ肷�郁�\�b�h�B
    /// </summary>
    /// <param name="isLock"></param>
    public void SetFlickLock(bool isLock)
    {
        m_isFlickLock = isLock; //�{�[���̈ړ������b�N���邩�ǂ�����ݒ�B
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //���b�N���������Ă��Ȃ��ꍇ�̓h���b�O�ʒu���X�V����B
        if (!m_isFlickLock)
        {
            //�}�E�X�̃h���b�O�ʒu���X�V
            UpdateDragPosition();
        }

        if (HasJustReleased())
        {
            OnDragEnded?.Invoke();
        }
        if (IsDragStarted())
        {
            OnDragStarted?.Invoke();
        }
        if (IsDragging())
        {
            OnArrowRotationUpdated?.Invoke(CalculateDragAngle());
            OnArrowLengthUpdated?.Invoke(GetSwipeDistance());
        }
    }

    public bool IsDragging()
    {
        if (m_isFlickLock)
        {
            //�{�[���̈ړ������b�N����Ă���ꍇ�̓h���b�O���ł͂Ȃ��B
            return false;
        }
        //�}�E�X�̍��{�^����������Ă��邩�ǂ����𔻒�
        return Input.GetMouseButton(0);
    }

    /// <summary>
    /// ���������Ď�𗣂����u�Ԃ��ǂ����H
    /// </summary>
    /// <returns></returns>
    public bool HasJustReleased()
    {
        if (m_isFlickLock)
        {
            //�{�[���̈ړ������b�N����Ă���ꍇ�̓h���b�O���ł͂Ȃ��B
            return false;
        }
        //�}�E�X�̍��{�^���������ꂽ���ǂ����𔻒�
        return Input.GetMouseButtonUp(0);
    }

    bool IsDragStarted()
    {
        if (m_isFlickLock)
        {
            //�{�[���̈ړ������b�N����Ă���ꍇ�̓h���b�O���ł͂Ȃ��B
            return false;
        }
        //�}�E�X�̍��{�^���������ꂽ�u�Ԃ��ǂ����𔻒�
        return Input.GetMouseButtonDown(0);
    }

    void UpdateDragPosition()
    {
        if (Input.GetMouseButton(0))
        {
            if (IsDragStarted())
            {
                //���݂̃}�E�X�̃��[���h���W���X�V
                m_dragStartPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            }
            else if (IsDragging())
            {
                //���݂̃}�E�X�̃��[���h���W���X�V
                m_dragCurrentPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            }
        }
    }

    /// <summary>
    /// �^�b�`�ŃX���C�v�̕������擾���郁�\�b�h�B
    /// </summary>
    /// <returns></returns>
   public Vector2 GetSwipeDirection()
    {
        //�^�b�`����Ă��邩�ǂ����𔻒�B
        if (IsDragging())
        {
            //�X���C�v�̕����B
            Vector2 swipeDirectionVector=Vector2.zero;

            //�X���C�v�̕������v�Z�B
            swipeDirectionVector = m_dragCurrentPos - m_dragStartPos;

            //���K�����ꂽ�X���C�v�x�N�g�����������B�B
            return swipeDirectionVector.normalized;
        }
        else
        {
            //�^�b�`����Ă��Ȃ��ꍇ�̓[���x�N�g����Ԃ��B
            return Vector2.zero;

        }
    }


    /// <summary>
    /// �^�b�`�ŃX���C�v���I������p�x���擾���郁�\�b�h�B
    /// </summary>
    /// <returns></returns>
    public Vector2 GetSwipeEndedDirection()
    {
        //�^�b�`���I������u�Ԃ��ǂ����𔻒�B
        if (HasJustReleased())
        {
            //�X���C�v�̕����B
            Vector2 swipeEndedDirectionVector;

            //�X���C�v�̕������v�Z�B
            swipeEndedDirectionVector = m_dragCurrentPos - m_dragStartPos;

            //���K�����ꂽ�X���C�v�x�N�g�����������B�B
            return swipeEndedDirectionVector.normalized;
        }
        else
        {
            //�^�b�`���I����Ă��Ȃ��ꍇ�̓[���x�N�g����Ԃ��B
            return Vector2.zero;
        }
    }


    /// <summary>
    /// �X���C�v�̋������擾���郁�\�b�h�B
    /// </summary>
    /// <returns></returns>
    public float GetSwipeDistance()
    {
        //�^�b�`����Ă��邩�ǂ����𔻒�B
        if (IsDragging())
        {
            //�X���C�v�̋������v�Z�B
            float swipeDistance = Vector2.Distance(m_dragCurrentPos, m_dragStartPos);

            //�X���C�v�̋�����Ԃ��B
            return swipeDistance;
        }
        else
        {
            //�^�b�`����Ă��Ȃ��ꍇ�̓[����Ԃ��B
            return 0.0f;

        }
    }

    /// <summary>
    /// �h���b�O�̊p�x���v�Z���郁�\�b�h�B
    /// </summary>
    /// <returns></returns>
    public float CalculateDragAngle()
    {
        float angle = Mathf.Atan2(GetSwipeDirection().y, GetSwipeDirection().x) * Mathf.Rad2Deg;
        return angle;
    }
}
