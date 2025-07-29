using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 弱点の位置
/// </summary>
enum EnWeakPointPos {
    enTop,        // 上
    enButton,     // 下
    enLeft,       // 左
    enRight,      //右
    enEmpty,      // 空
}


public class EnemyReaction : MonoBehaviour
{
    //敵の弱点の位置
    [SerializeField] private EnWeakPointPos weakPointPos = EnWeakPointPos.enEmpty;

    EnemyStatus enemyStatus; // 敵のステータス
    WeakPoint weakPoint; // 弱点のスクリプト
    GameObject m_weakObject = null; // 弱点のオブジェクト    

    private Dictionary<EnWeakPointPos, Vector2> weakPointPattern = new Dictionary<EnWeakPointPos, Vector2>() {
        { EnWeakPointPos.enTop,    new Vector2(0.0f, 10.0f) },
        { EnWeakPointPos.enButton, new Vector2(0.0f, -1.0f) },
        { EnWeakPointPos.enLeft,   new Vector2(-1.0f, 0.0f) },
        { EnWeakPointPos.enRight,  new Vector2(1.0f, 0.0f) },
        { EnWeakPointPos.enEmpty,  new Vector2(0.0f, 0.0f) },
    };


    public Image m_healImage; // HPバーのUI
    public Canvas m_hpCanvas;


    public int WeakPointPos
    {
        get { return (int)weakPointPos; }
    }


    // Start is called before the first frame update
    void Start()
    {
        //弱点生成
        //if (weakPointPos != EnWeakPointPos.enEmpty)
        //{
        //    m_weakObject = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/prefabs/WeakPoint.prefab");
        //    Instantiate(m_weakObject, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity);
        //    m_weakObject.transform.position += (Vector3)weakPointPattern[weakPointPos]; // 弱点の位置を設定
        //}

        enemyStatus = GetComponent<EnemyStatus>();
        m_weakObject = GetComponentInChildren<WeakPoint>().gameObject; // 弱点のオブジェクトを取得
        SpriteRenderer weakRenderer = m_weakObject.GetComponent<SpriteRenderer>();
        weakRenderer.sortingOrder = 10; // 弱点のスプライトの描画順を設定
        //弱点オブジェクトの null チェック
        if (m_weakObject == null) {
            Debug.LogError("弱点オブジェクトがアタッチされていません！！");
            return;
        }
        weakPoint = m_weakObject.GetComponentInChildren<WeakPoint>();
        m_weakObject.transform.position = transform.position + (Vector3)weakPointPattern[weakPointPos]; // 弱点の位置を設定
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

        //null チェック 
        if (weakPoint == null) {
            return;
        }

        if (other.tag == "Player")
        {
            if (weakPoint.IsHit)
            {
                enemyStatus.ApplyDamage(20 * 2); // 弱点にヒットしているなら大ダメージを与える
                Debug.Log("2");
            }
            else
            {
                enemyStatus.ApplyDamage(20); // 弱点にヒットしていないなら通常ダメージを与える
                Debug.Log("1");
            }
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
