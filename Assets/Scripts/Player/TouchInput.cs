using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TouchInput : MonoBehaviour
{
    // タッチ情報を格納する変数
    Touch m_touch;
    //タッチした最初の位置
    Vector2 beganTouchPosition=Vector2.zero;

    //タッチしている位置
    Vector2 movetTouchPosition= Vector2.zero;

    //ボールの移動をロックするかどうかのフラグ
    bool m_isFlickLock = false;

    public event Action OnTouchiEnded;
    public event Action OnTouchiStarted;
    public event Action<float> OnArrowLengthUpdated; //ドラッグ中のイベント
    public event Action<float> OnArrowRotationUpdated; //ドラッグ中のイベント

    /// <summary>
    /// ボールのロックを設定するメソッド。
    /// </summary>
    /// <param name="isLock"></param>
    public void SetFlickLock(bool isLock)
    {
        m_isFlickLock = isLock; //ボールの移動をロックするかどうかを設定。
    }

    public bool IsFlickLock()
    {         //ボールの移動がロックされているかどうかを返す。
        return m_isFlickLock;
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
            //タッチの位置を更新
            UpdateTouchPosition();
        }

        if (HasJustReleased())
        {
            OnTouchiEnded?.Invoke();
        }
        if (IsTouchingBegan())
        {
            OnTouchiStarted?.Invoke();
        }
        if (IsTouching())
        {
            OnArrowRotationUpdated?.Invoke(CalculateSwipeAngle());
            OnArrowLengthUpdated?.Invoke(GetSwipeDistance());
        }
    }
    /// <summary>
    /// タッチされているかどうかを判定するメソッド。
    /// </summary>
    /// <returns></returns>
    bool IsTouching()
    {
        if (m_isFlickLock)
        {
            //ボールの移動がロックされている場合はタッチ中ではない。
            return false;
        }
        return Input.touchCount > 0;
    }

    /// <summary>
    /// タッチが始まった瞬間かどうかを判定するメソッド。
    /// </summary>
    /// <returns></returns>
    bool IsTouchingBegan()
    {
        if (m_isFlickLock)
        {
            //ボールの移動がロックされている場合はタッチ中ではない。
            return false;
        }
        return Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began;
    }

    /// <summary>
    /// タッチが終わった瞬間かどうかを判定するメソッド。
    /// </summary>
    /// <returns></returns>
    public bool HasJustReleased()
    {
        if (m_isFlickLock)
        {
            //ボールの移動がロックされている場合はタッチ中ではない。
            return false;
        }
        return Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended;
    }

    /// <summary>
    ///　タッチの位置を取得し、タッチの状態に応じて開始位置か移動位置を更新します。
    /// </summary>
    void UpdateTouchPosition()
    {
        //タッチされているかどうかを判定。
        if (Input.touchCount > 0)
        {
            // タッチ情報を取得
            m_touch = Input.GetTouch(0);

            // タッチのフェーズに応じてそれぞれの位置を更新。
            switch (m_touch.phase)
            {
                case TouchPhase.Began:
                    beganTouchPosition = m_touch.position;
                    break;
                case TouchPhase.Moved:
                    movetTouchPosition = m_touch.position; ;
                    break;
            }
        }
    }

    /// <summary>
    /// タッチでスワイプの方向を取得するメソッド。
    /// </summary>
    /// <returns></returns>
    Vector2 GetSwipeDirection()
    {
        //タッチされているかどうかを判定。
        if (IsTouching())
        {
            //スワイプの方向。
            Vector2 swipeDirectionVector;

            //スワイプの方向を計算。
            swipeDirectionVector = movetTouchPosition - beganTouchPosition;

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
    /// タッチでスワイプが終わった方向を取得するメソッド。
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
            swipeEndedDirectionVector = movetTouchPosition - beganTouchPosition;

            //正規化されたスワイプベクトルをかえす。
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
        if (IsTouching())
        {
            //スワイプの距離を計算。
            float swipeDistance = Vector2.Distance(movetTouchPosition, beganTouchPosition);

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
    /// スワイプの角度を計算するメソッド。
    /// </summary>
    /// <returns></returns>
    public float CalculateSwipeAngle()
    {
        float angle = Mathf.Atan2(GetSwipeDirection().y, GetSwipeDirection().x) * Mathf.Rad2Deg;
        return angle;
    }
}
