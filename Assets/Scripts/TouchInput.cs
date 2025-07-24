using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchInput : MonoBehaviour
{
    // �^�b�`�����i�[����ϐ�
    Touch m_touch;
    //�^�b�`�����ŏ��̈ʒu
    Vector2 beganTouchPosition=Vector2.zero;

    //�^�b�`���Ă���ʒu
    Vector2 movetTouchPosition= Vector2.zero;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //�^�b�`�̈ʒu���X�V
        UpdateTouchPosition();

    }
    /// <summary>
    /// �^�b�`����Ă��邩�ǂ����𔻒肷�郁�\�b�h�B
    /// </summary>
    /// <returns></returns>
    bool IsTouching()
    {
        return Input.touchCount > 0;
    }

    /// <summary>
    /// �^�b�`���I��������𔻒肷�郁�\�b�h�B
    /// </summary>
    /// <returns></returns>
    bool IsTouchEnded()
    {
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

            //�X���C�v�̕����𐳋K���B
            swipeDirectionVector.Normalize();

            //�X���C�v�̕�����Ԃ��B
            return swipeDirectionVector;
        }

        //�^�b�`����Ă��Ȃ��ꍇ�̓[���x�N�g����Ԃ��B
        return Vector2.zero;
    }

    Vector2 GetSwipeEndedDirection()
    {
        //�^�b�`���I������u�Ԃ��ǂ����𔻒�B
        if (IsTouchEnded())
        {
            //�X���C�v�̕����B
            Vector2 swipeEndedDirectionVector;

            //�X���C�v�̕������v�Z�B
            swipeEndedDirectionVector = movetTouchPosition - beganTouchPosition;

            //�X���C�v�̕����𐳋K���B
            swipeEndedDirectionVector.Normalize();

            //�X���C�v�̕�����Ԃ��B
            return swipeEndedDirectionVector;
        }

        //�^�b�`���I����Ă��Ȃ��ꍇ�̓[���x�N�g����Ԃ��B
        return Vector2.zero;
    }

    /// <summary>
    /// �X���C�v�̋������擾���郁�\�b�h�B
    /// </summary>
    /// <returns></returns>
    float GetSwipeDistance()
    {
        //�^�b�`����Ă��邩�ǂ����𔻒�B
        if (IsTouching())
        {
            //�X���C�v�̋������v�Z�B
            float swipeDistance = Vector2.Distance(movetTouchPosition, beganTouchPosition);

            //�X���C�v�̋�����Ԃ��B
            return swipeDistance;
        }

        //�^�b�`����Ă��Ȃ��ꍇ�̓[����Ԃ��B
        return 0.0f;
    }
}
