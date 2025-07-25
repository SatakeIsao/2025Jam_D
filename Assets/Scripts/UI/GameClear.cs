using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameClear : MonoBehaviour
{
    private Canvas m_canvas;
    public GameObject GameClearUIObj;
    float m_longPushDown = 0.5f; // キーを押してからの時間

    // Start is called before the first frame update
    void Start()
    {
        m_canvas = GameClearUIObj.GetComponent<Canvas>();
        m_canvas.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Tab))
        {
            Invoke(nameof(LongPush), m_longPushDown);
        }
        else if(Input.GetKeyUp(KeyCode.Tab) && IsInvoking(nameof(LongPush)))
        {
            CancelInvoke(nameof(LongPush));
            m_canvas.enabled = false; // キーを離したらキャンバスを非表示にする
        }
    }

    void LongPush()
    {
        //条件はデバッグ用。
        m_canvas.enabled = true; 
     //       = Input.GetKeyDown(KeyCode.Tab);
    }
}
