using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
public class PlayerMoveBase : MonoBehaviour
{
    [SerializeField] private float m_moveSpeed = 100.0f;        //発射速度
    [SerializeField] private float m_adjustmentSpeed = 1.0f;    //調整用の速度係数
    [SerializeField] private float m_drag;                      //空気抵抗
    [SerializeField] private float m_angularDrag;               //回転に対する抵抗

    private Rigidbody2D m_rigidBody;
    private TouchInput m_touchInput;
    private MauseInput m_mouseInput;

   





    public void SetMoveSpeed(float speed)
    {
        m_moveSpeed = speed; //発射速度を設定。
    }

    void Awake()
    {
        m_rigidBody = GetComponent<Rigidbody2D>(); //Rigidbody2Dの参照を取得。
        m_touchInput = GetComponent<TouchInput>(); //タッチ入力の参照。
        m_mouseInput = GetComponent<MauseInput>(); //マウス入力の参照。

        m_rigidBody.drag=m_drag; //Rigidbody2Dのドラッグを取得。
        m_rigidBody.angularDrag= m_angularDrag; //Rigidbody2Dの角度ドラッグを取得。
    }

    /// <summary>
    /// スワイプの方向に力を加えるメソッド。
    /// </summary>
    /// <param name="swipeDirection">スワイプ方向</param>
    void AddForce(Vector2 swipeDirection)
    {
        //スワイプ方向と逆の方向に飛んでいくようにする。
        swipeDirection *= -1;

        //スワイプ方向に力を加える。
        m_rigidBody.AddForce(
            swipeDirection * m_moveSpeed * m_adjustmentSpeed, 
            ForceMode2D.Impulse
            );

    }

    void FlicLockManager()
    {
        //ロックがかかっていないとき。
        if (!m_mouseInput.IsFlickLock())
        {
            if(m_mouseInput.IsDragEnded())
            {
                m_mouseInput.SetFlickLock(true);
            }
        }

        if (!m_touchInput.IsFlickLock())
        {
            if (m_touchInput.IsTouchEnded())
            {
                //マウスのドラッグが終わったら、ロックをかける。
                m_touchInput.SetFlickLock(true);
            }
        }


    }

    void Update()
    {

        //タッチが終わった瞬間だったら。
        if (m_touchInput.IsTouchEnded())
        {
         //タッチの終了位置から方向を取得して、力を加える。
         AddForce(m_touchInput.GetSwipeEndedDirection());
        }

        //マウスのドラッグが終わった瞬間だったら。
        if (m_mouseInput.IsDragEnded())
        {
          //マウスのドラッグ終了位置から方向を取得して、力を加える。
          AddForce(m_mouseInput.GetSwipeEndedDirection());
        }

        //ボールの移動速度がある程度低くなったらゼロにする。
        if (m_rigidBody.velocity.magnitude <= 1.0f)
        {
            m_rigidBody.velocity = Vector2.zero;
        }

        //ロックの管理を行う。
        FlicLockManager();

    }
}