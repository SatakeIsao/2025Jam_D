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
    bool m_hasPulled = false; //引っ張ったかどうかのフラグ
    float m_jumpTime = 0.0f; //ジャンプの時間を管理する変数
    float m_gameOverMoveTime = 0.0f; //ゲームオーバー時のジャンプ時間を管理する変数。
    bool IsStartJump = true;
    bool IsStartGameOverMove = true; //ゲームオーバー時の動きが開始されたかどうかのフラグ。
    Vector3 m_startJumpPos;
    Vector3 m_startGameOverPos; //ゲームオーバー時の開始位置。

    public void SetDrag(float dorag)
    {
        m_rigidBody.drag = dorag; //Rigidbody2Dのドラッグを更新。
    }

    public void GameClearMove()
    {
        if (!IsStartJump) {
            m_startJumpPos = transform.position;
            IsStartJump = false;
        }
       

       m_jumpTime+= 1.0f; //ジャンプの時間を更新。
        transform.position = new Vector3(transform.position.x, m_startJumpPos.y+ Mathf.Abs(Mathf.Sin(m_jumpTime*0.02f)), transform.position.z);
    }

    public void GameOverMove()
    {
        //ゲームオーバー時の動き。
        if (!IsStartGameOverMove)
        {
            m_startGameOverPos = transform.position;
            IsStartGameOverMove = false;
        }

        m_gameOverMoveTime += 1.0f; //ジャンプの時間を更新。
        if(m_gameOverMoveTime * 0.02f <= 3.141592f)
        {
            transform.position = new Vector3(transform.position.x, m_startJumpPos.y + (Mathf.Sin(m_gameOverMoveTime * 0.02f) * 1.5f), transform.position.z);
        }
    }

    public float GedMenbaDrag()
    {
        return m_drag; //Rigidbody2Dのドラッグを取得。
    }
    /// <summary>
    /// 引っ張った後に止まったかどうか？
    /// </summary>
    /// <returns></returns>
    public bool HasStoppedAfterPull()
    {
        //引っ張った後に止まったかどうかをチェックする。
        if (m_hasPulled)
        {
            m_hasPulled = false; //フラグをリセット。
            if (GetIsStop()) return true;
        }
            return false;
    }

    /// <summary>
    /// 今止まっているかどうか？
    /// </summary>
    /// <returns></returns>
    public bool GetIsStop()
    {
        if (m_rigidBody.velocity.magnitude<= 0)
        {
            return true; //ボールの速度が0以下ならtrueを返す。
        }
        else
        {
            return false; //ボールの速度が0より大きいならfalseを返す。
        }
    }

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

    void Start()
    {
        //引っ張って放したら、呼ばれるイベントに関数を追加。
        m_mouseInput.OnDragEnded += () => {
            //ロックがかかっていないとき、マウスのドラッグが終わったら、ロックをかける。
            if (!m_mouseInput.IsFlickLock()|| !m_touchInput.IsFlickLock()) m_hasPulled = true;
        };
        m_touchInput.OnDragEnded += () =>
        {
            //ロックがかかっていないとき、タッチのドラッグが終わったら、ロックをかける。
            if (!m_mouseInput.IsFlickLock() || !m_touchInput.IsFlickLock()) m_hasPulled = true;
        };
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

    //void FlicLockManager()
    //{
    //    //ロックがかかっていないとき。
    //    if (!m_mouseInput.IsFlickLock())
    //    {
    //        if(m_mouseInput.IsDragEnded())
    //        {
    //            //TODO: 今はインプット側のisFlickLockがロックの主導権を持っているが、時間があれば
    //            //ムーブ側が主導権を持つようにする。
    //            m_mouseInput.SetFlickLock(true);

    //        }
    //    }

    //    if (!m_touchInput.IsFlickLock())
    //    {
    //        if (m_touchInput.IsTouchEnded())
    //        {
    //            //マウスのドラッグが終わったら、ロックをかける。
    //            m_touchInput.SetFlickLock(true);
    //        }
    //    }


    //}

    void Update()
    {
        //タッチが終わった瞬間だったら。
        if (m_touchInput.HasJustReleased())
        {
         //タッチの終了位置から方向を取得して、力を加える。
         AddForce(m_touchInput.GetSwipeEndedDirection());
        }

        //マウスのドラッグが終わった瞬間だったら。
        if (m_mouseInput.HasJustReleased())
        {
          //マウスのドラッグ終了位置から方向を取得して、力を加える。
          AddForce(m_mouseInput.GetSwipeEndedDirection());
        }

        //ボールの移動速度がある程度低くなったらゼロにする。
        if (m_rigidBody.velocity.magnitude <= 1.0f)
        {
            m_rigidBody.velocity = Vector2.zero;
        }


        //TODO：ターンマネージャーでロックの管理を行うようにする。
        ////ロックの管理を行う。
        //FlicLockManager();

    }
}