using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    private bool animationTriggered = false; 

    [SerializeField] private Canvas m_canvas;
    [SerializeField] private Animator m_anim;
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
        if (HPManager.m_instance != null) // null�`�F�b�N��ǉ�
        {
            m_canvas.enabled = HPManager.m_instance.m_isHpZero;

            if (HPManager.m_instance.m_isHpZero && !animationTriggered)
            {
                m_anim.SetBool("isGameOver", true);
                animationTriggered = true; 
            }

        }
    }

}