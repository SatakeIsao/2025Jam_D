using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// エネミーの種類
/// </summary>
enum EnEnemyType {
    enSmall,
    enBoss,
}


public class EnemyStatus : MonoBehaviour
{
    //HP
    int m_hp = 0;
    // エネミーの種類
    EnEnemyType m_enemyType = 0;

    /// <summary>
    /// ダメージを受ける
    /// </summary>
    /// <param name="damage">ダメージ量</param>
    void ApplyDamage(int damage)
    {
        m_hp -= damage;
        if (m_hp <= 0)
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// 体力を回復
    /// </summary>
    /// <param name="recovery">回復量</param>
    void Heal(int recovery)
    {
        m_hp += recovery;
        // HPの上限を設定する場合はここでチェック
    }

    /// <summary>
    /// コンストラクタ
    /// </summary>
    /// <param name="hp">初期HP</param>
    /// <param name="enemyType">エネミーの種類</param>
    EnemyStatus(int hp, EnEnemyType enemyType)
    {
        m_hp = hp;
        m_enemyType = enemyType;
        SizeSetting(m_enemyType);
    }

    // Start is called before the first frame update
    void Start()
    {
        
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
