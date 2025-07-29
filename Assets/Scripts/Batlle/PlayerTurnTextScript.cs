using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerTurnTextScript : MonoBehaviour
{
    public TextMeshProUGUI TurnText;
    [SerializeField] private BattleManager battleManager; // BattleManagerの参照
    public float PlayerTurnTimer = 3.0f;   //～のターンですの表示時間

    // Start is called before the first frame update
    void Start()
    {
        TurnText.gameObject.SetActive(false); // ターンテキストを表示する
    }

    void PlayerTurnTimerCountDown()
    {
        if (battleManager.IsPlayerActive == true)
        {
            TurnText.gameObject.SetActive(true); // ターンテキストを表示する
            PlayerTurnTimer -= Time.deltaTime; // ターンタイマーをカウントダウン
            if (PlayerTurnTimer <= 0)
            {
                TurnText.gameObject.SetActive(false); // ターンテキストを非表示にする
                PlayerTurnTimer = 0; // ターンタイマーをリセット
            }
        }
        else
        {
            TurnText.gameObject.SetActive(false); // ターンテキストを非表示にする
            PlayerTurnTimer = 3; // ターンタイマーをリセット
        }

    }

    // Update is called once per frame
    void Update()
    {
        PlayerTurnTimerCountDown(); // ターンタイマーのカウントダウンを実行
    }
}
