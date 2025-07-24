using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    HPManager m_hpManager;
    Renderer m_render;
    // Start is called before the first frame update
    void Start()
    {
        //m_render.enabled = false;
        //gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            Destroy(gameObject);
           // m_render.enabled = true;
        }
        //if (m_hpManager.m_isHpZero==true)
        //{
         //   gameObject.SetActive(true);
        //}
        
    }
}
