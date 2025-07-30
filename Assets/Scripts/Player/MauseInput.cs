using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MauseInput : MonoBehaviour
{

    //マウスのドラッグ関連の変数
    private Vector2 m_dragStartPos = Vector2.zero;     //ドラッグ開始位置
    private Vector2 m_dragCurrentPos = Vector2.zero;   //ドラッグ中の現在位置
    private bool m_isDragging = false;  //ドラッグ中かどうか
    bool m_isFlickLock = false; //ボールの移動をロックするかどうかのフラグ
    public event Action OnDragStarted; //ドラッグ開始時のイベント
    public event Action OnDragEnded;
    public event Action<float> OnArrowLengthUpdated; //ドラッグ中のイベント
    public event Action<float> OnArrowRotationUpdated; //ドラッグ中のイベント


    public bool IsFlickLock()
    {         //ボールの移動がロックされているかどうかを返す。
        return m_isFlickLock;
    }

    /// <summary>
    /// ボールのロックを設定するメソッド。
    /// </summary>
    /// <param name="isLock"></param>
    public void SetFlickLock(bool isLock)
    {
        m_isFlickLock = isLock; //ボールの移動をロックするかどうかを設定。
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //ロックがかかっていない場合はドラッグ位置を更新する。
        if (!m_isFlickLock)
        {
            //マウスのドラッグ位置を更新
            UpdateDragPosition();
        }

        if (HasJustReleased())
        {
            OnDragEnded?.Invoke();
        }
        if (IsDragStarted())
        {
            OnDragStarted?.Invoke();
        }
        if (IsDragging())
        {
            OnArrowRotationUpdated?.Invoke(CalculateDragAngle());
            OnArrowLengthUpdated?.Invoke(GetSwipeDistance());
        }
    }

    public bool IsDragging()
    {
        if (m_isFlickLock)
        {
            //ボールの移動がロックされている場合はドラッグ中ではない。
            return false;
        }
        //マウスの左ボタンが押されているかどうかを判定
        return Input.GetMouseButton(0);
    }

    /// <summary>
    /// 引っ張って手を離した瞬間かどうか？
    /// </summary>
    /// <returns></returns>
    public bool HasJustReleased()
    {
        if (m_isFlickLock)
        {
            //ボールの移動がロックされている場合はドラッグ中ではない。
            return false;
        }
        //マウスの左ボタンが離されたかどうかを判定
        return Input.GetMouseButtonUp(0);
    }

    bool IsDragStarted()
    {
        if (m_isFlickLock)
        {
            //ボールの移動がロックされている場合はドラッグ中ではない。
            return false;
        }
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

            //正規化されたスワイプベクトルをかえす。。
            return swipeDirectionVector.normalized;
        }
        else
        {
            //タッチされていない場合はゼロベクトルを返す。
            return Vector2.zero;

        }
    }


    /// <summary>
    /// タッチでスワイプが終わった角度を取得するメソッド。
    /// </summary>
    /// <returns></returns>
    public Vector2 GetSwipeEndedDirection()
    {
        //タッチが終わった瞬間かどうかを判定。
        if (HasJustReleased())
        {
            //スワイプの方向。
            Vector2 swipeEndedDirectionVector;

            //スワイプの方向を計算。
            swipeEndedDirectionVector = m_dragCurrentPos - m_dragStartPos;

            //正規化されたスワイプベクトルをかえす。。
            return swipeEndedDirectionVector.normalized;
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

    /// <summary>
    /// ドラッグの角度を計算するメソッド。
    /// </summary>
    /// <returns></returns>
    public float CalculateDragAngle()
    {
        float angle = Mathf.Atan2(GetSwipeDirection().y, GetSwipeDirection().x) * Mathf.Rad2Deg;
        return angle;
    }
}
