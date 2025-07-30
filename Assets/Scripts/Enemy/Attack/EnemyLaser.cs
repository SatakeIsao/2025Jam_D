using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLaser : MonoBehaviour
{
    EnemyStatus m_enemyStatus;
    HPManager m_HPManager;

    //ダメージ量
    [SerializeField] private int m_damage = 0;
    //攻撃までのターン数
    [SerializeField] private int m_attackTurn = 0;
    //現在の残りターン
    private int m_currentTurn = 0;
    //プレイヤーオブジェクトを取得
    private GameObject[] m_players = new GameObject[4];
    //現在の最寄りのプレイヤーまでの距離
    private Vector2 m_toNearestPlayerDis = Vector2.zero;
    //レーザーの射出方向ベクトル
    private Vector2 m_toNearPlayerDir = Vector2.zero;

    private float laserLength = 20.0f;     // レーザーの長さ
    private float duration = 0.7f;       // レーザー表示時間
    private LayerMask wallMask;       // 壁レイヤー
    private LayerMask enemyLayer;          // 当たり判定するレイヤー

    private LineRenderer line;

    void Start()
    {
        m_enemyStatus = GetComponent<EnemyStatus>();
        m_HPManager = GameObject.Find("HPManager").GetComponent<HPManager>();

        m_players = GameObject.FindGameObjectsWithTag("Player");

        // LineRendererを用意
        line = gameObject.AddComponent<LineRenderer>();
        line.startWidth = 1.0f;
        line.endWidth = 1.0f;
        line.material = new Material(Shader.Find("Sprites/Default"));
        line.startColor = Color.red;
        line.endColor = Color.red;
        line.enabled = false;

        //debug
        //TurnCount();
    }

    

    // Update is called once per frame
    void Update()
    {
    }

    /// <summary>
    /// ターンのカウントダウン（ターン数が0になったらレーザーを発射）
    /// </summary>
    public void TurnCount()
    {
        m_currentTurn--;

        if (m_currentTurn <= 0)
        {
            CalcLaserVec();
            Fire(); // レーザーを発射
            m_currentTurn = m_attackTurn; // ターン数をリセット
        }
    }

    public void Fire()
    {
        // レーザーの始点
        Vector2 startPos = transform.position;
        Vector2 endPos = startPos + m_toNearPlayerDir * laserLength;

        //壁とのヒットチェック
        RaycastHit2D wallHit = Physics2D.Raycast(startPos, m_toNearPlayerDir, laserLength, wallMask);

        if (wallHit.collider != null) // 壁に当たらなかった場合
        {
            GameObject wallObj = wallHit.collider.gameObject;
            if (wallObj.tag == "Wall") // 壁に当たった場合
            {
                endPos = wallHit.point; // 壁で止まる
            }
        }

        // プレイヤーとのヒットチェック
        RaycastHit2D hit = Physics2D.Raycast(startPos, m_toNearPlayerDir, laserLength, enemyLayer);
        if (hit.collider != null)
        {
            GameObject hitObject = hit.collider.gameObject;
            if (hitObject.tag == "Player")
            {
                // プレイヤーにダメージを与える
                m_HPManager.Damage(m_damage);
            }
        }

        
       

        // レーザーの終点を設定
        line.SetPosition(1, endPos);

        // レーザーを表示
        StartCoroutine(ShowLaser(startPos, endPos));
    }

    private System.Collections.IEnumerator ShowLaser(Vector2 start, Vector2 end)
    {
        line.enabled = true;
        line.SetPosition(0, start);
        line.SetPosition(1, end);
        yield return new WaitForSeconds(duration);
        line.enabled = false;
    }

    /// <summary>
    /// レーザーの射出方向を計算
    /// </summary>
    private void CalcLaserVec()
    {
        for (int i = 0; i < m_players.Length; i++)
        {
            if (m_players[i] == null) continue; // プレイヤーが存在しない場合はスキップ
            Vector2 playerDis = m_players[i].transform.position - transform.position;
            if (playerDis.magnitude > m_toNearestPlayerDis.magnitude)
            {
                m_toNearestPlayerDis = playerDis;
            }
        }

        m_toNearPlayerDir = m_toNearestPlayerDis.normalized;
    }
}
