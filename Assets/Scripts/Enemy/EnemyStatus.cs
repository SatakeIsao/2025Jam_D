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
    //HP
    public int m_HP = 0;
    //最大HP
    [SerializeField] public int m_maxHP = 0;
    //初期位置（縦）
    [SerializeField] private EnSmallPosVertical m_newPositionVer = EnSmallPosVertical.enEmpty;
    //初期位置
    Vector2 m_newPosition = Vector2.zero;

    /// <summary>
    /// 初期位置を取得
    /// </summary>
    /// <returns>初期位置</returns>
    public Vector2 GetNewPos()
    {
        return m_newPosition;
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
        m_newPosition = (Vector2)transform.position;
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
