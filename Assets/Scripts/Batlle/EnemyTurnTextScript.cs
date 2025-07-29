using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemyTurnTextScript : MonoBehaviour
{
    public TextMeshProUGUI EnemyTurnText;

    [SerializeField] private BattleManager battleManager; // BattleManagerを参照するための変数

    public float enemyTurnTimer = 2.0f; // 敵のターン表示時間

    // Start is called before the first frame update
    void Start()
    {
        EnemyTurnText.gameObject.SetActive(false); // 敵のターンテキストを非表示にする
    }

    void EnemyTurnFlag()
    {
        if (battleManager.IsEnemyActive == true)
        {
            EnemyTurnText.gameObject.SetActive(true); // 敵のターンテキストを表示する
            enemyTurnTimer -= Time.deltaTime; // タイマーを減少させる
            if (enemyTurnTimer <= 0.0f) // タイマーが0以下になったら
            {
                EnemyTurnText.gameObject.SetActive(false); // 敵のターンテキストを非表示にする
                enemyTurnTimer = 2.0f; // タイマーをリセット
            }
        }
        else
        {
            EnemyTurnText.gameObject.SetActive(false); // 敵のターンテキストを非表示にする
        }

    }

    // Update is called once per frame
    void Update()
    {
        EnemyTurnFlag(); // 敵のターンフラグを更新
    }
}
