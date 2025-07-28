using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PlayerTeam : MonoBehaviour
{
    [SerializeField] private List<GameObject> playerObjects;
    public List<GameObject> PlayerObjects => playerObjects;  // 外から読み取り専用
    int m_totalDamage = 0;
    int m_teamHP = 0;

    void Awake()
    {
    }
    // Start is called before the first frame update
    void Start()
    {
        //チームの初期HPを設定。
        InitializeTeamHP();

        // 各プレイヤーのダメージイベントにメソッドを登録
        foreach (var obj in playerObjects)
        {
            var player = obj.GetComponent<PlayerBase>();
            player.OnDamaged += TakeDamage;
        }

    }

    void InitializeTeamHP()
    {
        //チームHPをプレイヤーのHPの合計に設定
        m_teamHP = playerObjects.Sum(obj => obj.GetComponent<PlayerBase>().GetPalamata().hp);
    }


    /// <summary>
    /// チームの現在のHPを取得するメソッド。
    /// </summary>
    /// <returns></returns>
    public int GetCurrentHP()
    {
        return m_teamHP;
    }


    /// <summary>
    /// チームHPが0以下かどうかをチェックするメソッド。
    /// ゼロ以下ならtrueを返す。
    /// </summary>
    /// <returns></returns>
    public bool IsDead() {
        if (m_teamHP <= 0)
        {
            return true;
        }
        return false;
    }


    // Update is called once per frame
    void Update()
    {

    }

    /// <summary>
    /// ダメージを受けたときに呼び出されるメソッド。
    /// </summary>
    /// <param name="playerBase"></param>
    public void TakeDamage(PlayerBase playerBase)
    {
        m_totalDamage = playerObjects.Sum(obj => obj.GetComponent<PlayerBase>().GetDamagae());
        m_teamHP -= m_totalDamage;
    }

}