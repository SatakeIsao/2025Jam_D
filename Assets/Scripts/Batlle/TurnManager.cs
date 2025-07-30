///
/// ターンマネージャー
///
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    // @todo for test
    [Header("プレイヤーたち"), SerializeField]
    private GameObject[] playerObjects_;
    private List<PlayerBase> players_ = new List<PlayerBase>();
    private List<PlayerMoveBase> playerMoves_ = new List<PlayerMoveBase>();


    //[Header("プレイヤーたち"), SerializeField]
    //private PlayerBase[] players_;

    //[Header("プレイヤーの移動"), SerializeField]
    //private PlayerMoveBase[] playerMoves_;

    [Header("プレイヤーHP"), SerializeField]
    private HPManager playerHP_;

    [Header("敵たち"), SerializeField]
    private EnemyAttackControl[] enemies_;

    [Header("ターンテキスト"), SerializeField]
    private TurnText turnText_;

    /// <summary>
    ///  プレイヤーの経過ターン数
    /// </summary>
    private int playerTurnCount_ = 0;

    /// <summary>
    /// 敵の経過ターン数
    /// </summary>
    private int eneyTurnCount_ = MaxEnemyTurn;

    /// <summary>
    /// 敵の最大ターン数
    /// </summary>
    public static readonly int MaxEnemyTurn = 3;

    /// <summary>
    /// Start
    /// </summary>
    void Start()
    {
        for (var i = 0; i < playerObjects_.Length; i++)
        {
            players_.Add(playerObjects_[i].GetComponent<PlayerBase>());
            playerMoves_.Add(playerObjects_[i].GetComponent<PlayerMoveBase>());

            var state = PlayerState.enLocalPlayerTurn;
            if (i > 0)
            {
                state = PlayerState.enOtherPlayerTurn;
            }
            players_[i].ChangeState(state);
        }

        // Battleコルーチンを開始
        StartCoroutine(Battle());
    }

    /// <summary>
    /// バトルフロー
    /// </summary>
    /// <returns></returns>
    IEnumerator Battle()
    {
        while (true)
        {
            // プレイヤーのターン開始
            StartCoroutine(turnText_.DisplayText("PlayerTurn"));
            players_[playerTurnCount_].ChangeState(PlayerState.enLocalPlayerTurn);
            Debug.Log("プレイヤーのターン: " + playerTurnCount_);
            yield return new WaitUntil(() => 
            {
                bool isPull = false;
                foreach(var playerMove in playerMoves_)
                {
                    if (playerMove.HasStoppedAfterPull())
                    {
                        isPull = true;
                        break;
                    }
                }
                return isPull;
            });

            // プレイーのターンが終了したら、次のプレイヤーに移行
            Debug.Log("プレイヤーのターン終了: " + playerTurnCount_);
            players_[playerTurnCount_].ChangeState(PlayerState.enOtherPlayerTurn);
            playerTurnCount_++;
            if (playerTurnCount_ >= players_.Count)
            {
                playerTurnCount_ = 0;
            }

            // ゲームクリアチェック
            if (EnemyCheckScript.m_instance.gameClearFlag)
            {
                yield break;
            }

            // 敵のターン開始
            eneyTurnCount_ -= 1;
            if (eneyTurnCount_ == 0)
            {
                foreach (var player in players_)
                {
                    player.ChangeState(PlayerState.enEnemyTurn);
                }
                Debug.Log("敵のターン");

                // 敵の攻撃を開始
                StartCoroutine(turnText_.DisplayText("EnemyTurn"));

                foreach (var enemy in enemies_)
                {
                    //enemy.ResetEnemyAction(); // 敵の行動をリセット
                }

                // 敵の行動を待つ
                // yield return new WaitUntil(() => );

                eneyTurnCount_ = MaxEnemyTurn;
            }

            // プレイヤーのHPチェック
            if (playerHP_.m_isHpZero)
            {
                yield break;
            }

        }
    }
}
