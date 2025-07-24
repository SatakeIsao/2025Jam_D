using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MauseInput : MonoBehaviour
{

    //マウスのドラッグ関連の変数
    private Vector2 m_dragStartPos = Vector2.zero;     //ドラッグ開始位置
    private Vector2 m_dragCurrentPos = Vector2.zero;   //ドラッグ中の現在位置
    private bool m_isDragging = false;  //ドラッグ中かどうか

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public void Update()
    {
        UpdateDragPosition();
    }

    public bool IsDragging()
    {
        //マウスの左ボタンが押されているかどうかを判定
        return Input.GetMouseButton(0);
    }

    public bool IsDragEnded()
    {
        //マウスの左ボタンが離されたかどうかを判定
        return Input.GetMouseButtonUp(0);
    }

    bool IsDragStarted()
    {
        //マウスの左ボタンが押された瞬間かどうかを判定
        return Input.GetMouseButtonDown(0);
    }

    void UpdateDragPosition()
    {
        if (Input.GetMouseButton(0))
        {
            if (IsDragStarted())
            {
                //現在のマウスのワールド座標を更新
                m_dragStartPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            }
            else if (IsDragging())
            {
                //現在のマウスのワールド座標を更新
                m_dragCurrentPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            }
        }
    }

    /// <summary>
    /// タッチでスワイプの方向を取得するメソッド。
    /// </summary>
    /// <returns></returns>
   public Vector2 GetSwipeDirection()
    {
        //タッチされているかどうかを判定。
        if (IsDragging())
        {
            //スワイプの方向。
            Vector2 swipeDirectionVector=Vector2.zero;

            //スワイプの方向を計算。
            swipeDirectionVector = m_dragCurrentPos - m_dragStartPos;

            //スワイプの方向を正規化。
            swipeDirectionVector.Normalize();

            //スワイプの方向を返す。
            return swipeDirectionVector;
        }
        else
        {
            //タッチされていない場合はゼロベクトルを返す。
            return Vector2.zero;

        }
    }


    /// <summary>
    /// タッチでスワイプが終わった方向を取得するメソッド。
    /// </summary>
    /// <returns></returns>
    public Vector2 GetSwipeEndedDirection()
    {
        //タッチが終わった瞬間かどうかを判定。
        if (IsDragEnded())
        {
            //スワイプの方向。
            Vector2 swipeEndedDirectionVector;

            //スワイプの方向を計算。
            swipeEndedDirectionVector = m_dragCurrentPos - m_dragStartPos;

            //スワイプの方向を正規化。
            swipeEndedDirectionVector.Normalize();

            //スワイプの方向を返す。
            return swipeEndedDirectionVector;
        }
        else
        {
            //タッチが終わっていない場合はゼロベクトルを返す。
            return Vector2.zero;
        }
    }


    /// <summary>
    /// スワイプの距離を取得するメソッド。
    /// </summary>
    /// <returns></returns>
    public float GetSwipeDistance()
    {
        //タッチされているかどうかを判定。
        if (IsDragging())
        {
            //スワイプの距離を計算。
            float swipeDistance = Vector2.Distance(m_dragCurrentPos, m_dragStartPos);

            //スワイプの距離を返す。
            return swipeDistance;
        }
        else
        {
            //タッチされていない場合はゼロを返す。
            return 0.0f;

        }
    }
}
