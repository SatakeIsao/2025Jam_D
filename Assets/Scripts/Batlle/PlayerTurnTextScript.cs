using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerTurnTextScript : MonoBehaviour
{
    public TextMeshProUGUI TurnText;
    public float PlayerTurnTimer = 3;   //～のターンですの表示時間
    public bool playerTurnFlag = true; // プレイヤーのターンかどうかのフラグ

    // Start is called before the first frame update
    void Start()
    {
        TurnText.gameObject.SetActive(true); // ターンテキストを表示する
    }

    void PlayerTurnTimerCountDown()
    {
        if (playerTurnFlag == true)
        {
            PlayerTurnTimer -= Time.deltaTime; // タイマーを減少させる
            if (PlayerTurnTimer <= 0.0f) // タイマーが0以下になったら
            {
                playerTurnFlag = false; // プレイヤーのターンフラグをオフにする
                TurnText.gameObject.SetActive(false); // ターンテキストを非表示にする
                PlayerTurnTimer = 3.0f; // タイマーをリセット
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        PlayerTurnTimerCountDown(); // ターンタイマーのカウントダウンを実行
    }
}
