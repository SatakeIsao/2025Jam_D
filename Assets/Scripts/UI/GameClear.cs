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
        m_canvas.enabled = EnemyCheckScript.m_instance.gameClearFlag;
        //�Q�[���N���A�ɂȂ������ɃA�j���[�V�������Đ�
        OnEnable();
    }

    void LongPush()
    {
        //�����̓f�o�b�O�p�B
        m_canvas.enabled = true; 
    }

    private void OnEnable()
    {
        if(EnemyCheckScript.m_instance.gameClearFlag)
        {
            m_anim.SetBool("isGameClear", true);
        }
    }
}
