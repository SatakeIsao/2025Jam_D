using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;

enum EnChargeTarget
{
    enToWayPoint, //ウェイポイントへ突進
    enToNewPos, //初期位置へ突進
    enEmpty 
}


public class EnemyCharge : MonoBehaviour
{
    EnemyStatus enemyStatus;
    CircleCollider2D m_chargeAttackCollider;

    //ダメージ（毎フレーム）
    [SerializeField] private float m_chargeDamage = 0.0f;
    //攻撃ターン数
    [SerializeField] private int m_turnNum = 0;
    //現在の残りターン数
    private int m_restTurnNum = 0;
    //ウェイポイントの位置（縦）
    [SerializeField] private EnSmallPosVertical wayVer = EnSmallPosVertical.enEmpty;
    //ウェイポイントの位置（横）
    [SerializeField] private EnSmallPosHorizontal wayHor = EnSmallPosHorizontal.enEmpty;
    //一度の突進での移動距離
    private const float m_chargeDistance = 2.0f;
    //突進の目標位置の状態
    private EnChargeTarget m_chargeTarget = EnChargeTarget.enEmpty;
    //突進の目標位置
    private Vector2 m_targetPos = Vector2.zero;
    //1フレームの移動量（倍率）
    private const float m_moveMagnification = 0.02f;
    //一度の movement での移動量
    private Vector2 m_moveAmount = Vector2.zero;
    //現在の突進攻撃で動いた回数
    private int m_restMoveNum = 0;
    //現在動いたベクトルを記録
    private Vector2 m_moveVecMemory = Vector2.zero;
    //停止時間
    private const float STOP_TIME = 0.3f;
    //現在の停止時間
    private float m_currentStopTime = 0.0f;
    //行動中か
    private bool m_isInAction = false;

    /// <summary>
    /// ターンのカウントダウン（ターン数が0になったら突進を行う）
    /// </summary>
    public void TurnCount()
    {
        m_restTurnNum--;

        if(m_restTurnNum <= 0)
        {
            m_isInAction = true; //行動終了フラグをリセット
            m_currentStopTime = 0.0f; //停止時間をリセット
            m_moveVecMemory = Vector2.zero; //移動したベクトルをリセット
            CalcMoveAmount(); //一度の突進で動く距離を計算
        }
    }

    /// <summary>
    /// 行動中かチェック
    /// </summary>
    /// <returns>行動中フラグ</returns>
    public bool GetIsInAction()
    {
        return m_isInAction; //行動中かどうかを返す
    }


    // Start is called before the first frame update
    void Start()
    {
        enemyStatus = GetComponent<EnemyStatus>();
        //突進攻撃用のコライダーを追加
        gameObject.AddComponent<CircleCollider2D>();
        m_chargeAttackCollider = GetComponent<CircleCollider2D>();
        m_chargeAttackCollider.radius = 1.2f;

        m_chargeTarget = EnChargeTarget.enToWayPoint; //突進の目標位置をウェイポイントへ設定
    }

    // Update is called once per frame
    void Update()
    {
        //debug
        if (!m_isInAction)
        {
            m_isInAction = true; //行動終了フラグをリセット
            m_currentStopTime = 0.0f; //停止時間をリセット
            m_moveVecMemory = Vector2.zero; //移動したベクトルをリセット
            CalcMoveAmount(); //一度の突進で動く距離を計算
        }

        m_currentStopTime += Time.deltaTime; //現在の停止時間を更新
        if (m_currentStopTime < STOP_TIME)
        {
            m_chargeAttackCollider.enabled = true;

            return; //停止時間が経過していないので、突進しない
        }

        m_chargeAttackCollider.enabled = false;

        if (m_restMoveNum > 0)
        {
            ChargeExecution();
        }
        else
        {
            TurnEnd();
        }
    }

    /// <summary>
    /// 突進実行
    /// </summary>
    private void ChargeExecution()
    {
        transform.Translate(m_moveAmount * m_moveMagnification);
        m_moveVecMemory += m_moveAmount * m_moveMagnification; //移動したベクトルを記録

        if (m_moveVecMemory.magnitude >= m_moveAmount.magnitude)
        {
            m_moveVecMemory = Vector2.zero; //移動したベクトルをリセット
            m_currentStopTime = 0.0f; //停止時間をリセット
            m_restMoveNum--;
        }        
    }

    /// <summary>
    /// 一度の move で動く距離を計算
    /// </summary>
    private void CalcMoveAmount()
    {
        //突進の目標位置を設定
        if (m_chargeTarget == EnChargeTarget.enToWayPoint)
        {
            m_targetPos = enemyStatus.GetPosition((int)wayVer, (int)wayHor);
        }
        else if(m_chargeTarget == EnChargeTarget.enToNewPos)
        {
            m_targetPos = enemyStatus.GetNewPos();
        }

        Vector2 chargeVec = m_targetPos - (Vector2)transform.position;
        int moveNum = (int)Math.Ceiling(chargeVec.magnitude / m_chargeDistance); //移動する回数を計算（端数切り上げで int に代入）
        Vector2 moveAmount = chargeVec / moveNum;
        m_moveAmount = moveAmount;
        m_restMoveNum = moveNum; //残りの移動回数をセット
    }

    /// <summary>
    /// 突進終了時の処理
    /// </summary>
    private void TurnEnd()
    {
        m_isInAction = false; //突進が終了
        m_restTurnNum = m_turnNum; //残りのターン数をリセット

        //突進目標位置を変える
        if (m_chargeTarget == EnChargeTarget.enToWayPoint)
        {
            m_chargeTarget = EnChargeTarget.enToNewPos; //ウェイポイントから初期位置へ
        }
        else if (m_chargeTarget == EnChargeTarget.enToNewPos)
        {
            m_chargeTarget = EnChargeTarget.enToWayPoint; //初期位置からウェイポイントへ
        }
    }

}
