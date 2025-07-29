using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnCountScript : MonoBehaviour
{
    [SerializeField] private BattleManager battleManager; // プレイヤーの移動スクリプトを参照するための変数

    public int EnemyTurnCount = 1; // 敵のターン数を管理する変数

    // Start is called before the first frame update
    void Start()
    {

    }

    // ターン数を増やすメソッド
    void Turn()
    {
        if(battleManager.IsPlayerActive==false)
        {
            EnemyTurnCount -= 1; // 敵のターン数を減らす
            if (EnemyTurnCount <= 0)
            {
                battleManager.IsEnemyActive = true; // 敵のターンを終了
                EnemyTurnCount = 1; // ターン数をリセット
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        Turn();
    }
}
