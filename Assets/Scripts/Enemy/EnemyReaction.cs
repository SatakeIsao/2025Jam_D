using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyReaction : MonoBehaviour
{
    EnemyStatus enemyStatus; // 敵のステータス
    public Image m_healImage; // HPバーのUI
    public Canvas m_hpCanvas;

    // Start is called before the first frame update
    void Start()
    {
        enemyStatus = GetComponent<EnemyStatus>();
    }

    // Update is called once per frame
    void Update()
    {
        JudgeDeath();
        UpdateHPUI();
    }

    /// <summary>
    /// プレイヤーの攻撃にヒットしたか
    /// </summary>
    void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject other = collision.gameObject;
        if (other.tag == "Player") {
            enemyStatus.ApplyDamage(200); // プレイヤーの攻撃が当たったらダメージを与える
            
        }
    }

    //HPバーの更新
    private void UpdateHPUI()
    {
        m_healImage.fillAmount = (float)enemyStatus.HP / enemyStatus.m_maxHP;
    }

    /// <summary>
    /// 死亡判定
    /// </summary>
    void JudgeDeath()
    {
        if (enemyStatus.HP <= 0) {
            Destroy(gameObject);
            Destroy(m_hpCanvas.gameObject); // HPバーのUIも削除
        }
    }
}
