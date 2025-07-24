using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// エネミーの種類
/// </summary>
enum EnEnemyType {
    enSmall,
    enBoss,
    enEmpty,
}


public class EnemyStatus : MonoBehaviour
{
    //HP
    private int m_hp = 200;
    //最大HP
    private int m_maxHp = 200;
    // エネミーの種類
    private EnEnemyType m_enemyType = EnEnemyType.enEmpty;

    /// <summary>
    /// ダメージを受ける
    /// </summary>
    /// <param name="damage">ダメージ量</param>
    public void ApplyDamage(int damage)
    {
        m_hp -= damage;
        //HPを0未満にしない
        if (m_hp <= 0) {
            m_hp = 0;
        }
    }

    /// <summary>
    /// 体力を回復
    /// </summary>
    /// <param name="recovery">回復量</param>
    public void Heal(int recovery)
    {
        m_hp += recovery;
        //HPを最大値を超えないようにする
        if (m_hp > m_maxHp) {
            m_hp = m_maxHp;
        }
    }

    /// <summary>
    /// HPを取得
    /// </summary>
    public int HP
    {
        get
        {
            return m_hp;
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
