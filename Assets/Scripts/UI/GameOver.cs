using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    private Canvas m_canvas;
    public GameObject GameOverUIObj;
    
    // Start is called before the first frame update
    void Start()
    {
        m_canvas = GameOverUIObj.GetComponent<Canvas>();
        m_canvas.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        m_canvas.enabled = HPManager.m_instance.m_isHpZero;

    }
}
