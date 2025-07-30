using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLaser : MonoBehaviour
{
    EnemyStatus m_enemyStatus;

    //レーザーのオブジェクト
    [SerializeField] private GameObject m_laserObject = null;
    //ダメージ量
    [SerializeField] private int m_damageAmount = 0;
    //攻撃までのターン数
    [SerializeField] private int m_attackTurn = 0;
    //現在の残りターン
    private int m_currentTurn = 0;
    //レーザーの長さ
    [SerializeField] private float m_laserLength = 0.0f;
    //レーザーの太さ
    [SerializeField] private float m_laserWidth = 0.0f;
    //レーザーの持続時間
    [SerializeField] private float m_laserDuration = 0.0f;
    //レーザーの速さ
    private float m_laserSpeed = 1.0f;
    //プレイヤーオブジェクトを取得
    private GameObject[] m_players = new GameObject[4];
    //現在の最寄りのプレイヤーまでの距離
    private float m_toNearestPlayerDis = 0.0f;
    //最寄りのプレイヤー
    private GameObject m_nearestPlayer = null;
    //レーザーの射出方向ベクトル
    private Vector2 m_toNearPlayerDir = Vector2.zero;

    public float laserLength = 10f;     // レーザーの長さ
    public float damage = 20f;          // ダメージ量
    public float duration = 0.2f;       // レーザー表示時間
    public LayerMask hitLayer;          // 当たり判定するレイヤー

    private LineRenderer line;

    void Start()
    {
        m_enemyStatus = GetComponent<EnemyStatus>();

        m_players = GameObject.FindGameObjectsWithTag("Player");

        // LineRendererを用意
        line = gameObject.AddComponent<LineRenderer>();
        line.startWidth = 0.1f;
        line.endWidth = 0.1f;
        line.material = new Material(Shader.Find("Sprites/Default"));
        line.startColor = Color.red;
        line.endColor = Color.red;
        line.enabled = false;
    }

    public void Fire(Vector2 direction)
    {
        // レーザーの始点
        Vector2 startPos = transform.position;
        Vector2 endPos = startPos + direction.normalized * laserLength;

        // Raycast でヒットチェック
        RaycastHit2D hit = Physics2D.Raycast(startPos, direction, laserLength, hitLayer);
        if (hit.collider != null)
        {
            endPos = hit.point;

            // 敵にダメージを与える処理
            GameObject hitObject = hit.collider.gameObject;
            if (hitObject.tag =="Player")
            {
                //TODO プレイヤーにダメージ

            }           
        }

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
    /// ターンのカウントダウン（ターン数が0になったらレーザーを発射）
    /// </summary>
    public void TurnCount()
    {
        m_currentTurn--;

        if (m_currentTurn <= 0)
        {
            CalcLaserVec();
            m_laserObject = Instantiate(m_laserObject, transform.position, Quaternion.LookRotation(m_toNearPlayerDir));
            m_currentTurn = m_attackTurn; // ターン数をリセット
        }
    }

    // Update is called once per frame
    void Update()
    {
        m_laserObject.transform.localScale += new Vector3(0.0f, m_laserSpeed, 0.0f); //レーザーを上方向に伸ばす
    }



    /// <summary>
    /// レーザーの射出方向を計算
    /// </summary>
    private void CalcLaserVec()
    {
        for (int i = 0; i < m_players.Length; i++)
        {
            if (m_players[i] == null) continue; // プレイヤーが存在しない場合はスキップ
            float playerDis = Vector2.Distance(m_enemyStatus.GetNewPos(), m_players[i].transform.position);
            if (playerDis < m_toNearestPlayerDis)
            {
                m_toNearestPlayerDis = playerDis;
            }
        }

        //m_toNearPlayerDir = m_toNearestPlayerDis.normalized;
    }

    /// <summary>
    /// ベクトルを再計算
    /// </summary>
    private void VecRecalculation()
    {

    }
}
