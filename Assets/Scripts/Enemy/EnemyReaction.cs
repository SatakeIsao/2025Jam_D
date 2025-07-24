using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyReaction : MonoBehaviour
{
    EnemyStatus enemyStatus; // 敵のステータス
    public GameObject m_player;
    Collision collision;


    // Start is called before the first frame update
    void Start()
    {
        enemyStatus = GetComponent<EnemyStatus>();
    }

    // Update is called once per frame
    void Update()
    {
        //OnCollisionEnter(Collision collision);
        JudgeDeath();
    }

    /// <summary>
    /// プレイヤーの攻撃にヒットしたか
    /// </summary>
    void OnCollisionEnter(Collision collision)
    {
        GameObject other = collision.gameObject;
        if(other.name == "Circle") {
            enemyStatus.ApplyDamage(100); // プレイヤーの攻撃が当たったらダメージを与える
        }
    }

    /// <summary>
    /// 死亡判定
    /// </summary>
    void JudgeDeath()
    {
        if (enemyStatus.HP <= 0) {
            Destroy(gameObject);
        }
    }
}
