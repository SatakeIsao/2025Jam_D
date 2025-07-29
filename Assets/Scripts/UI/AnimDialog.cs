using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AnimDialog : MonoBehaviour
{
    //�A�j���[�^�[
    [SerializeField] private Animator m_animator;
    //�A�j���[�^�[�R���g���[���[�̃��C���[
    [SerializeField] private int m_layer;
    //IsOpen�t���O
    //raadoly->�ǂݍ��ݐ�p�i�萔�j
    private static readonly int m_paramIsOpen = Animator.StringToHash("isOpen");
    //�_�C�A���O�͊J���Ă��邩�ǂ���
    //get�v���p�e�B�ŁA�Q�[���I�u�W�F�N�g���A�N�e�B�u���ǂ�����Ԃ�
    public bool m_isOpen => gameObject.activeSelf;
    //�A�j���[�V���������ǂ���
    //set�����N���X�����ݒ�\�Aget���O������ǂݎ��\
    public bool m_isTransition { get; private set; }

    //�_�C�A���O���J��
    public void Open()
    {
        //�s������̖h�~
        if (m_isOpen || m_isTransition) return;
        //�p�l�����̂��A�N�e�B�u�ɂ���
        gameObject.SetActive(true);
        //m_isOpen�t���O���Z�b�g
        m_animator.SetBool(m_paramIsOpen, true);
        //�A�j���[�V�����ҋ@
        StartCoroutine(WaitAnimation("Shown"));
    }

    //�_�C�A���O�����
    public void Close()
    {
        //�s������̖h�~
        if (!m_isOpen || m_isTransition) return;
        //m_isOpen�t���O���N���A
        m_animator.SetBool(m_paramIsOpen, false);
        //�A�j���[�V�����ҋ@���A�I�������p�l�����̂��A�N�e�B�u�ɂ���
        StartCoroutine(WaitAnimation("Hidden", () => gameObject.SetActive(false)));
    }

    //�J�A�j���[�V�����̑ҋ@�R���[�`��
    private IEnumerator WaitAnimation(string stateName,UnityAction onCompled=null)
    {
        m_isTransition = true;
        yield return new WaitUntil(() =>
        {
            //�X�e�[�g���ω����A�A�j���[�V�������I������܂Ń��[�v
            var state = m_animator.GetCurrentAnimatorStateInfo(m_layer);
            return state.IsName(stateName) && state.normalizedTime >= 1;
        });
        m_isTransition = false;
        onCompled?.Invoke();
    }
}
