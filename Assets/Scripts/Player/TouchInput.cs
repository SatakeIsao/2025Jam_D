using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TouchInput : MonoBehaviour
{
    // �^�b�`�����i�[����ϐ�
    Touch m_touch;
    //�^�b�`�����ŏ��̈ʒu
    Vector2 beganTouchPosition=Vector2.zero;

    //�^�b�`���Ă���ʒu
    Vector2 movetTouchPosition= Vector2.zero;

    //�{�[���̈ړ������b�N���邩�ǂ����̃t���O
    bool m_isFlickLock = false;

    public event Action OnTouchiEnded;
    public event Action OnTouchiStarted;
    public event Action<float> OnArrowLengthUpdated; //�h���b�O���̃C�x���g
    public event Action<float> OnArrowRotationUpdated; //�h���b�O���̃C�x���g

    /// <summary>
    /// �{�[���̃��b�N��ݒ肷�郁�\�b�h�B
    /// </summary>
    /// <param name="isLock"></param>
    public void SetFlickLock(bool isLock)
    {
        m_isFlickLock = isLock; //�{�[���̈ړ������b�N���邩�ǂ�����ݒ�B
    }

    public bool IsFlickLock()
    {         //�{�[���̈ړ������b�N����Ă��邩�ǂ�����Ԃ��B
        return m_isFlickLock;
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
            //�^�b�`�̈ʒu���X�V
            UpdateTouchPosition();
        }

        if (HasJustReleased())
        {
            OnTouchiEnded?.Invoke();
        }
        if (IsTouchingBegan())
        {
            OnTouchiStarted?.Invoke();
        }
        if (IsTouching())
        {
            OnArrowRotationUpdated?.Invoke(CalculateSwipeAngle());
            OnArrowLengthUpdated?.Invoke(GetSwipeDistance());
        }
    }
    /// <summary>
    /// �^�b�`����Ă��邩�ǂ����𔻒肷�郁�\�b�h�B
    /// </summary>
    /// <returns></returns>
    bool IsTouching()
    {
        if (m_isFlickLock)
        {
            //�{�[���̈ړ������b�N����Ă���ꍇ�̓^�b�`���ł͂Ȃ��B
            return false;
        }
        return Input.touchCount > 0;
    }

    /// <summary>
    /// �^�b�`���n�܂����u�Ԃ��ǂ����𔻒肷�郁�\�b�h�B
    /// </summary>
    /// <returns></returns>
    bool IsTouchingBegan()
    {
        if (m_isFlickLock)
        {
            //�{�[���̈ړ������b�N����Ă���ꍇ�̓^�b�`���ł͂Ȃ��B
            return false;
        }
        return Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began;
    }

    /// <summary>
    /// �^�b�`���I������u�Ԃ��ǂ����𔻒肷�郁�\�b�h�B
    /// </summary>
    /// <returns></returns>
    public bool HasJustReleased()
    {
        if (m_isFlickLock)
        {
            //�{�[���̈ړ������b�N����Ă���ꍇ�̓^�b�`���ł͂Ȃ��B
            return false;
        }
        return Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended;
    }

    /// <summary>
    ///�@�^�b�`�̈ʒu���擾���A�^�b�`�̏�Ԃɉ����ĊJ�n�ʒu���ړ��ʒu���X�V���܂��B
    /// </summary>
    void UpdateTouchPosition()
    {
        //�^�b�`����Ă��邩�ǂ����𔻒�B
        if (Input.touchCount > 0)
        {
            // �^�b�`�����擾
            m_touch = Input.GetTouch(0);

            // �^�b�`�̃t�F�[�Y�ɉ����Ă��ꂼ��̈ʒu���X�V�B
            switch (m_touch.phase)
            {
                case TouchPhase.Began:
                    beganTouchPosition = m_touch.position;
                    break;
                case TouchPhase.Moved:
                    movetTouchPosition = m_touch.position; ;
                    break;
            }
        }
    }

    /// <summary>
    /// �^�b�`�ŃX���C�v�̕������擾���郁�\�b�h�B
    /// </summary>
    /// <returns></returns>
    Vector2 GetSwipeDirection()
    {
        //�^�b�`����Ă��邩�ǂ����𔻒�B
        if (IsTouching())
        {
            //�X���C�v�̕����B
            Vector2 swipeDirectionVector;

            //�X���C�v�̕������v�Z�B
            swipeDirectionVector = movetTouchPosition - beganTouchPosition;

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
    /// �^�b�`�ŃX���C�v���I������������擾���郁�\�b�h�B
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
            swipeEndedDirectionVector = movetTouchPosition - beganTouchPosition;

            //���K�����ꂽ�X���C�v�x�N�g�����������B
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
        if (IsTouching())
        {
            //�X���C�v�̋������v�Z�B
            float swipeDistance = Vector2.Distance(movetTouchPosition, beganTouchPosition);

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
    /// �X���C�v�̊p�x���v�Z���郁�\�b�h�B
    /// </summary>
    /// <returns></returns>
    public float CalculateSwipeAngle()
    {
        float angle = Mathf.Atan2(GetSwipeDirection().y, GetSwipeDirection().x) * Mathf.Rad2Deg;
        return angle;
    }
}
