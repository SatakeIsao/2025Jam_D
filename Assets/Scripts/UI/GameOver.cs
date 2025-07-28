using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour
{
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
        //ゲームオーバーになった時にアニメーションを再生
        OnEnable();
    }

    /// <summary>
    /// ゲームオーバーになった時にアニメーションを再生
    /// </summary>
    private void OnEnable()
    {
        if (HPManager.m_instance.m_isHpZero)
        {
            m_anim.SetBool("isGameOver", true);
        }
        
    }
}
