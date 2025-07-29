using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    [SerializeField] private HPManager HPManager; // HPManager�̃C���X�^���X���Q�Ƃ��邽�߂̕ϐ�

    private Canvas m_canvas;
    private Animator m_anim;
    public GameObject GameOverUIObj;
    
    // Start is called before the first frame update
    void Start()
    {
        m_canvas = GameOverUIObj.GetComponent<Canvas>();
        m_canvas.enabled = false;
        m_anim = GameOverUIObj.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        m_canvas.enabled = HPManager.m_instance.m_isHpZero;
        //�Q�[���I�[�o�[�ɂȂ������ɃA�j���[�V�������Đ�
        OnEnable();
    }

    /// <summary>
    /// �Q�[���I�[�o�[�ɂȂ������ɃA�j���[�V�������Đ�
    /// </summary>
    private void OnEnable()
    {
        if (HPManager.m_instance.m_isHpZero)
        {
            m_anim.SetBool("isGameOver", true);
        }
        
    }
}
