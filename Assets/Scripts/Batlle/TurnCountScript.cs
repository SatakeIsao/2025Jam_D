using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnCountScript : MonoBehaviour
{
    public int EnemyTurnCount = 3; // 敵のターン数を管理する変数

    public float enemyAttackTimer = 2.0f; //仮置き 
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // ターン数を増やすメソッド
    void Turn()
    {
        // Aキーが押されたらターン数を増やす
        if (Input.GetKeyDown(KeyCode.A))
        {
            EnemyTurnCount--;
        }

        // スペースキーが押されたら現在ターン数の確認
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log(" 敵が攻撃してくるまで残りターン数:" + EnemyTurnCount);
        }

        if(EnemyTurnCount==0)
        {
            enemyAttackTimer -= Time.deltaTime; // タイマーを減少させる
            if (enemyAttackTimer <= 0.0f) // タイマーが0以下になったら
            {
                enemyAttackTimer = 2.0f; // タイマーをリセット
                EnemyTurnCount = 3; // ターン数をリセット
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
       Turn();
    }
}
