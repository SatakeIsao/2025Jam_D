using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;

public class GetItemScript : MonoBehaviour
{
    private const int HEAL_AMOUNT = 50; // ヒールアイテムの回復量
    //private const float ATK_BOOST_AMOUNT = 10.0f; // 攻撃力アップアイテムの効果量
    private const float SPEED_BOOST_AMOUNT = 10.0f; // スピードアップアイテムの効果量
    private float playerSpeed;

    [SerializeField] private HPManager hpManager; // HPManagerのインスタンスを保持する変数
    [SerializeField] private PlayerBase playerBase; // プレイヤーの基本情報を管理するスクリプトを参照するための変数
    [SerializeField] private PlayerMoveBase playerMoveBase; // プレイヤーの移動を管理するスクリプトを参照するための変数

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
        if (collision.gameObject.CompareTag("Item"))
        {
            // アイテムの種類に応じて処理を分岐
            if (collision.gameObject.name == "HealItem")
            {
                // ヒールアイテムを取得した場合の処理
                Debug.Log("回復アイテムを手に入れた！");
                // ここにヒール処理を追加
                hpManager.Heal(HEAL_AMOUNT); // 例として50回復する処理を追加
                AudioManager.Instance.PlaySE(AudioManager.SEType.enGetItem);

            }
            if (collision.gameObject.name == "ATKItem")
            {
                // 攻撃力アップアイテムを取得した場合の処理
                Debug.Log("攻撃力アップアイテムを手に入れた！");
                // ここに攻撃力アップ処理を追加
                AudioManager.Instance.PlaySE(AudioManager.SEType.enGetItem);
            }
            if (collision.gameObject.name == "SpeedItem")
            {
                // スピードアップアイテムを取得した場合の処理
                Debug.Log("移動速度アップアイテムを手に入れた！");
                // ここにスピードアップ処理を追加
                Debug.Log("現在の移動速度: " + playerMoveBase.m_moveSpeed);
                playerSpeed = playerMoveBase.m_moveSpeed + SPEED_BOOST_AMOUNT; // 例として移動速度を10.0fアップする処理を追加
                playerMoveBase.m_moveSpeed = playerSpeed; // プレイヤーの移動速度を更新
                Debug.Log("新しい移動速度: " + playerMoveBase.m_moveSpeed);
                AudioManager.Instance.PlaySE(AudioManager.SEType.enGetItem);

            }
            // アイテムを削除
            Destroy(collision.gameObject);
        }
    }
}
