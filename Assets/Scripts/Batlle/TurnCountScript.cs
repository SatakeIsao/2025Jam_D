using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnCountScript : MonoBehaviour
{
    [SerializeField] private PlayerMoveBase playerMoveBase; // プレイヤーの移動スクリプトを参照するための変数

    public int EnemyTurnCount = 1; // 敵のターン数を管理する変数
    public int EnemyTurn = 1; // 敵のターン数を表示する変数
    public float enemyAttackTimer = 2.0f; //仮置き
                                          //
    // Start is called before the first frame update
    void Start()
    {

    }

    // ターン数を増やすメソッド
    void Turn()
    {
        if (playerMoveBase.HasStoppedAfterPull())
        {
            // プレイヤーが移動を停止した場合、敵のターン数を減らす
            EnemyTurn = Mathf.Max(EnemyTurn, 0); // 敵のターン数が0未満にならないようにする
            EnemyTurn =EnemyTurnCount - 1; // 敵のターン数を減らす
            EnemyTurn = EnemyTurnCount; // 敵のターン数を更新
            if (EnemyTurn <= 0)
            {
                Debug.Log("敵のターンです。");
                EnemyTurnCount = 1; // 敵のターン数をリセット
            }

            else
            {
                Debug.Log("敵の攻撃まで後:" + EnemyTurn);
            }

        }
    }

    // Update is called once per frame
    void Update()
    {
        Turn();
    }
}
