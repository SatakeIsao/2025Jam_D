using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
public class BallMove : MonoBehaviour
{
    [SerializeField]
    protected float m_moveSpeed = 100.0f; //発射速度
    [SerializeField]
    protected float m_adjustmentSpeed = 1.0f; //調整用の速度係数
    protected Rigidbody2D m_rigidBody;  //Rigidbody2Dへの参照
    //マウスのドラッグ関連の変数
    private Vector2 m_dragStartPos;     //ドラッグ開始位置
    private Vector2 m_dragEndPos;       //ドラッグ終了位置
    private bool m_isDragging = false;  //ドラッグ中かどうか
    protected virtual void Awake()
    {
        m_rigidBody = GetComponent<Rigidbody2D>();
        m_rigidBody.drag = 0.2f;        //空気抵抗
        m_rigidBody.angularDrag = 1.0f;//回転に対する抵抗
    }
    //マウスボタンが押された時の処理
    private void OnMouseDown()
    {
        if (Input.GetMouseButtonDown(0)) //左ボタン
        {
            m_isDragging = true;
            //マウスのスクリーン座標をワールド空間に変換して保存
            m_dragStartPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            //ドラッグ開始時に現在の速度をリセット
            if (m_rigidBody != null)
            {
                m_rigidBody.velocity = Vector2.zero;
                m_rigidBody.angularVelocity = 0.0f; //回転もリセット
            }
        }
    }
    //マウスボタンが押されている間の処理
    void OnMouseDrag()
    {
        if (m_isDragging
&& Input.GetMouseButton(0)) //ドラッグ中で左ボタン押されている間
        {
            //現在のマウスのワールド座標を更新
            m_dragEndPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            //引っ張っている方向のベクトル
            Vector2 dragVector = m_dragStartPos - m_dragEndPos;
        }
    }
    //マウスボタンが離された時の処理
    private void OnMouseUp()
    {
        if (m_isDragging
&& Input.GetMouseButtonUp(0))//ドラッグ中で左ボタンが離された瞬間
        {
            m_isDragging = false;
            m_dragEndPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            //マウスのドラッグ開始地点から終了地点までのベクトルを計算
            Vector2 launchDirection = (m_dragStartPos - m_dragEndPos).normalized;   //向きを正規化
            //引っ張る距離に関わらずに、固定の力で発射
            m_rigidBody.AddForce(launchDirection * m_moveSpeed * m_adjustmentSpeed, ForceMode2D.Impulse);
        }
    }
}