using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchInput : MonoBehaviour
{
    // タッチ情報を格納する変数
    Touch m_touch;
    //タッチした最初の位置
    Vector2 beganTouchPosition=Vector2.zero;

    //タッチしている位置
    Vector2 movetTouchPosition= Vector2.zero;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //タッチの位置を更新
        UpdateTouchPosition();

    }
    /// <summary>
    /// タッチされているかどうかを判定するメソッド。
    /// </summary>
    /// <returns></returns>
    bool IsTouching()
    {
        return Input.touchCount > 0;
    }

    /// <summary>
    /// タッチが終わったかを判定するメソッド。
    /// </summary>
    /// <returns></returns>
    bool IsTouchEnded()
    {
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

            //スワイプの方向を正規化。
            swipeDirectionVector.Normalize();

            //スワイプの方向を返す。
            return swipeDirectionVector;
        }

        //タッチされていない場合はゼロベクトルを返す。
        return Vector2.zero;
    }

    Vector2 GetSwipeEndedDirection()
    {
        //タッチが終わった瞬間かどうかを判定。
        if (IsTouchEnded())
        {
            //スワイプの方向。
            Vector2 swipeEndedDirectionVector;

            //スワイプの方向を計算。
            swipeEndedDirectionVector = movetTouchPosition - beganTouchPosition;

            //スワイプの方向を正規化。
            swipeEndedDirectionVector.Normalize();

            //スワイプの方向を返す。
            return swipeEndedDirectionVector;
        }

        //タッチが終わっていない場合はゼロベクトルを返す。
        return Vector2.zero;
    }

    /// <summary>
    /// スワイプの距離を取得するメソッド。
    /// </summary>
    /// <returns></returns>
    float GetSwipeDistance()
    {
        //タッチされているかどうかを判定。
        if (IsTouching())
        {
            //スワイプの距離を計算。
            float swipeDistance = Vector2.Distance(movetTouchPosition, beganTouchPosition);

            //スワイプの距離を返す。
            return swipeDistance;
        }

        //タッチされていない場合はゼロを返す。
        return 0.0f;
    }
}
