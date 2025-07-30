using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameClear : MonoBehaviour
{
    private Canvas m_canvas;
    private Animator m_anim;
    public GameObject GameClearUIObj;
    float m_longPushDown = 0.5f; // �L�[�������Ă���̎���

    // Start is called before the first frame update
    void Start()
    {
        m_canvas = GameClearUIObj.GetComponent<Canvas>();
        m_canvas.enabled = false;
        m_anim = GameClearUIObj.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //Canvas�̗L������������؂�ւ���t���O�݂̂��`�F�b�N
        m_canvas.enabled = EnemyCheckScript.m_instance.gameClearFlag;
    }

    /// <summary>
    /// �Q�[���N���A�������������ꂽ�Ƃ��ɌĂяo����鏈��
    /// </summary>
    public void TrigggerGameClearUI()
    {
        if(EnemyCheckScript.m_instance.gameClearFlag)
        {
            m_canvas.enabled = true;
            m_anim.SetBool("isGameClear", true);
        }
    }
}
