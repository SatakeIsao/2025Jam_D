using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyReaction : MonoBehaviour
{
    EnemyStatus enemyStatus; // 敵のステータス

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        JudgeUnderAttack();
    }

    /// <summary>
    /// プレイヤーの攻撃にヒットしたか
    /// </summary>
    void JudgeUnderAttack()
    {

    }

    /// <summary>
    /// 死亡
    /// </summary>
    void JudgeDeath()
    {
        if (enemyStatus)
        {

        }
    }
}
