using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices.WindowsRuntime;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

/// <summary>
/// エネミーの種類
/// </summary>
enum EnEnemyType {
    enSmall,
    enBoss,
    enEmpty,
}

/// <summary>
/// 雑魚敵の縦座標のパターン
/// </summary>
enum EnSmallPosVertical { 
    enTop,
    enMiddleTop,
    enMiddle,
    enMiddleBottom,
    enBottom,
    enEmpty,
}

/// <summary>
/// 雑魚敵の横座標のパターン
/// </summary>
enum EnSmallPosHorizontal {
    enLeft,
    enMiddle,
    enRight,
    enEmpty,
}



public class EnemyStatus : MonoBehaviour
{
    //座標の縦軸のパターン（雑魚敵）
    private Dictionary<EnSmallPosVertical, float> smallVerticalPattern = new Dictionary<EnSmallPosVertical, float>(){
        {EnSmallPosVertical.enTop,          11.0f},
        {EnSmallPosVertical.enMiddleTop,    6.5f},
        {EnSmallPosVertical.enMiddle,       2.0f},
        {EnSmallPosVertical.enMiddleBottom, -2.5f},
        {EnSmallPosVertical.enBottom,       -7.0f},
        {EnSmallPosVertical.enEmpty,        0},
    };

    //座標の横軸のパターン（雑魚敵）
    private Dictionary<EnSmallPosHorizontal, float> smallHorizontalPattern = new Dictionary<EnSmallPosHorizontal, float>() {
        {EnSmallPosHorizontal.enLeft,   -2.5f},
        {EnSmallPosHorizontal.enMiddle, 0.0f},
        {EnSmallPosHorizontal.enRight,  2.5f},
        {EnSmallPosHorizontal.enEmpty,  0.0f},
    };
    
    //HP
    public int m_HP = 0;
    //最大HP
    [SerializeField] public int m_maxHP = 0;
    //初期位置（縦）
    [SerializeField] private EnSmallPosVertical m_newPositionVer = EnSmallPosVertical.enEmpty;
    //初期位置（横）
    [SerializeField] private EnSmallPosHorizontal m_newPositionHor = EnSmallPosHorizontal.enEmpty;
    // エネミーの種類
    //[SerializeField] private EnEnemyType m_enemyType = EnEnemyType.enEmpty;

    /// <summary>
    /// 初期位置を取得
    /// </summary>
    /// <returns>初期位置</returns>
    public Vector2 GetNewPos()
    {
        return new Vector2(smallHorizontalPattern[m_newPositionHor], smallVerticalPattern[m_newPositionVer]);
    }

    /// ダメージを受ける
    /// </summary>
    /// <param name="damage">ダメージ量</param>
    public void ApplyDamage(int damage)
    {
        m_HP -= damage;
        //HPを0未満にしない
        if (m_HP <= 0) {
            m_HP = 0;
        }
    }

    /// <summary>
    /// 体力を回復
    /// </summary>
    /// <param name="recovery">回復量</param>
    public void Heal(int recovery)
    {
        m_HP += recovery;
        //HPを最大値を超えないようにする
        if (m_HP > m_maxHP) {
            m_HP = m_maxHP;
        }
    }

    /// <summary>
    /// HPを取得
    /// </summary>
    public int HP
    {
        get
        {
            return m_HP;
        }
    }

    /// <summary>
    /// パターンを入力して座標を返す
    /// </summary>
    /// <param name="ver">縦のパターン</param>
    /// <param name="hor">横のパターン</param>
    /// <returns>返された座標</returns>
    public Vector2 GetPosition(int ver,int hor)
    {
        return new Vector2(smallHorizontalPattern[(EnSmallPosHorizontal)hor], smallVerticalPattern[(EnSmallPosVertical)ver]);
    }

    /// <summary>
    /// コンストラクタ
    /// </summary>
    /// <param name="hp">初期HP</param>
    /// <param name="enemyType">エネミーの種類</param>
    //EnemyStatus(int hp, EnEnemyType enemyType)
    //{
    //    m_hp = hp;
    //    m_enemyType = enemyType;
    //    SizeSetting(m_enemyType);
    //}

    // Start is called before the first frame update
    void Start()
    {
        m_HP = m_maxHP; // 初期HPを最大HPに設定

        //初期位置をセット
        transform.position = new Vector2(smallHorizontalPattern[m_newPositionHor], smallVerticalPattern[m_newPositionVer]);
    }

    /// Update is called once per frame
    void Update()
    {

    }

    /// <summary>
    /// エネミーのサイズを設定
    /// </summary>
    /// <param name="type">エネミーの種類</param>
    void SizeSetting(EnEnemyType type)
    {
        switch (type) {
            case EnEnemyType.enSmall:
                transform.localScale = new Vector3(1f, 1f, 1f);
                break;
            case EnEnemyType.enBoss:
                transform.localScale = new Vector3(2f, 2f, 2f);
                break;
            default:
                break;
        }
    }
}
