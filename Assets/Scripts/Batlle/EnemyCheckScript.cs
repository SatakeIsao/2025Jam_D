using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCheckScript : MonoBehaviour
{
    public static EnemyCheckScript m_instance; 

    GameObject[] enemyObjects;
    int EnemyNum;   //残りの敵の数

    public bool gameClearFlag = false; //ゲームクリアフラグ

    void Awake()
    {
        if(m_instance == null)
        {
            m_instance = this;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
       
    }

    void CheckEnemyCount()
    {
        enemyObjects = GameObject.FindGameObjectsWithTag("Enemy"); // "Enemy"タグを持つオブジェクトを全て取得
        EnemyNum = enemyObjects.Length; // 残りの敵の数を取得

        if (EnemyNum <= 0) // 残りの敵が0以下なら
        {
            gameClearFlag = true; // ゲームクリアフラグを立てる
            Debug.Log("ゲームクリア");
        }
    }

    // Update is called once per frame
    void Update()
    {
       CheckEnemyCount(); // 毎フレーム敵の数をチェック
    }
}