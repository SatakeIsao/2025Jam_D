using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;

public class GetItemScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 衝突したオブジェクトがアイテムかどうかをチェック
        if(collision.gameObject.CompareTag("Item"))
        {
            // アイテムの種類に応じて処理を分岐
            if (collision.gameObject.name == "HealItem")
            {
                // ヒールアイテムを取得した場合の処理
                Debug.Log("回復アイテムを手に入れた！");
                // ここにヒール処理を追加
            }
            else if (collision.gameObject.name == "ATKItem")
            {
                // 攻撃力アップアイテムを取得した場合の処理
                Debug.Log("攻撃力アップアイテムを手に入れた！");
                // ここに攻撃力アップ処理を追加
            }
            else if (collision.gameObject.name == "SpeedItem")
            {
                // スピードアップアイテムを取得した場合の処理
                Debug.Log("移動速度アップアイテムを手に入れた！");
                // ここにスピードアップ処理を追加
            }
            // アイテムを削除
            Destroy(collision.gameObject);
        }
    }
}
