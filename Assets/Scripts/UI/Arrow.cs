using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Arrow : MonoBehaviour
{
    private GameObject arrow;
    private MauseInput m_mauseInput;
    private RectTransform m_arrowRectTransform;

    void Awake()
    {
        if (transform.parent != null && transform.parent.parent != null)
        {
            Debug.Log("�}�E�X�C���v�b�g�̃R���|�[�l���g���擾");
            m_mauseInput = transform.parent.parent.GetComponent<MauseInput>();
        }
        if (m_mauseInput == null)
        {
            Debug.LogError("MouseInput ��������܂���ł���");
        }
        arrow = this.gameObject;
        m_arrowRectTransform = arrow.GetComponent<RectTransform>();
    }

    // Start is called before the first frame update
    void Start()
    {
        toggleArrow(false);
        m_mauseInput.OnDragStarted += () => toggleArrow(true); //�h���b�O�J�n���ɖ���\������B
        m_mauseInput.OnDragEnded += () => toggleArrow(false); //�h���b�O�I�����ɖ����\���ɂ���B
        m_mauseInput.OnArrowLengthUpdated += (scale) => SetLengthUpdated(scale*0.1f); //�h���b�O���̖��̃X�P�[�����X�V����B
        m_mauseInput.OnArrowRotationUpdated += (angle) => RotateArrow(angle); //�h���b�O���̖��̊p�x���X�V����B
        RotateArrow(11.0f);
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void toggleArrow(bool IsActive)
    {
        //���̕\���E��\����؂�ւ���B
        arrow.SetActive(IsActive);
    }

    public void SetLengthUpdated(float scale)
    {
        //���̃X�P�[����ݒ肷��B
        //���͂Ƌt�̕����ɖ��������邽�߁AX���̃X�P�[�����}�C�i�X�ɂ���B
        arrow.transform.localScale = new Vector3(-scale,1.0f , 1.0f);
    }

    public void RotateArrow(float angle)
    {
        //���̊p�x��ݒ肷��B
        m_arrowRectTransform.rotation = Quaternion.Euler(0, 0,angle);
        if (m_arrowRectTransform == null)
        {
            Debug.LogError("RectTransform ��������܂���ł���: " + gameObject.name);
        }
    }

}
