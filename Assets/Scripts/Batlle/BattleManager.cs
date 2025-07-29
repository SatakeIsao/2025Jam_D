using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    [SerializeField] private PlayerBase player1; // プレイヤーの基本情報を参照するための変数
    [SerializeField] private PlayerBase player2; // プレイヤーの基本情報を参照するための変数    
    [SerializeField] private PlayerBase player3; // プレイヤーの基本情報を参照するための変数
    [SerializeField] private PlayerBase player4; // プレイヤーの基本情報を参照するための変数
    [SerializeField] private PlayerMoveBase playerMove; // プレイヤーの移動スクリプトを参照するための変数

    [SerializeField] private EnemyAttackControl enemy1; // 敵の攻撃スクリプトを参照するための変数
    [SerializeField] private EnemyAttackControl enemy2; // 敵の攻撃スクリプトを参照するための変数
    [SerializeField] private EnemyAttackControl enemy3; // 敵の攻撃スクリプトを参照するための変数
    [SerializeField] private EnemyAttackControl Bossenemy; // 敵の攻撃スクリプトを参照するための変数

    [SerializeField] private HPManager playerHP; // プレイヤーチームのHPを管理するスクリプトを参照するための変数

    [SerializeField] private EnemyCheckScript enemyCheck; // 敵の状態をチェックするスクリプトを参照するための変数

    [SerializeField] private TurnCountScript enemyTurnCount; // ターン数を管理するスクリプトを参照するための変数

    //負けフラグ
    private bool isLose = false;

    // プレイヤーと敵のアクティブ状態を管理するフラグ
    public bool IsEnemyActive = false;
    public bool IsPlayerActive = true;

    // BattleStateとPlayerTurnを定義する列挙型
    public enum BattleState
    {
        enPlayerTurn,
        enChangePlayerTurn,
        enEnemyTurn,
        enGameOver,
        enGameClear,
    }

    public enum PlayerTurn
    {
        player1Turn,
        player2Turn,
        player3Turn,
        player4Turn
    }

    BattleState currentBattleState = BattleState.enPlayerTurn;
    PlayerTurn currentPlayerTurn = PlayerTurn.player1Turn;

    // Start is called before the first frame update
    void Start()
    {
        player1.ChangeState(PlayerState.enLocalPlayerTurn);
        player2.ChangeState(PlayerState.enOtherPlayerTurn);
        player3.ChangeState(PlayerState.enOtherPlayerTurn);
        player4.ChangeState(PlayerState.enOtherPlayerTurn);

        // Battleコルーチンを開始
        StartCoroutine(Battle());
    }

    IEnumerator Battle()
    {
        while (!isLose)
        {
            // プレイヤーのターン
            CheckPlayerTurn();
            Debug.Log("プレイヤーのターン: " + currentPlayerTurn);
            currentBattleState = BattleState.enPlayerTurn;

            // プレイヤーの行動を待つ
            yield return new WaitUntil(() => playerMove.HasStoppedAfterPull());

            //敵がやられたか確認
            if (EnemyCheckScript.m_instance.gameClearFlag == true)
            {
                currentBattleState = BattleState.enGameClear;
                yield break; // ゲームクリア時はコルーチンを終了
            }

            // プレイヤーの状態を変更
            PlayerStateChange();
            Debug.Log("プレイヤーの状態を変更: " + currentPlayerTurn);
            currentBattleState = BattleState.enChangePlayerTurn;

            if (enemyTurnCount.EnemyTurnCount == 0)
            {
                currentBattleState = BattleState.enEnemyTurn;
                // 敵のターン
                ChangeEnemyTurn();
                Debug.Log("敵のターン");

                // 敵の攻撃を開始
                EnemyAction();
                Debug.Log("敵の攻撃を開始");
            }

            // 敵の行動を待つ
            yield return new WaitUntil(() => IsEnemyActive == false);

            // ここで敵の行動結果をチェックして、プレイヤーが倒されたかどうかを確認する
            if (playerHP.m_isHpZero == true)
            {
                currentBattleState = BattleState.enGameOver;
                isLose = true; // プレイヤーが倒されたらゲームオーバー
                yield break; // ゲームオーバー時はコルーチンを終了
            }

        }
    }
    void CheckPlayerTurn()
    {

        IsPlayerActive = true; // プレイヤーの行動が可能な状態にする

        if (currentPlayerTurn == PlayerTurn.player1Turn)
        {
            player1.ChangeState(PlayerState.enLocalPlayerTurn);
        }

        else if (currentPlayerTurn == PlayerTurn.player2Turn)
        {
            player2.ChangeState(PlayerState.enLocalPlayerTurn);
        }

        else if (currentPlayerTurn == PlayerTurn.player3Turn)
        {
            player3.ChangeState(PlayerState.enLocalPlayerTurn);
        }

        else if (currentPlayerTurn == PlayerTurn.player4Turn)
        {
            player4.ChangeState(PlayerState.enLocalPlayerTurn);
        }
    }

    void PlayerStateChange()
    {
        if (playerMove.HasStoppedAfterPull())
        {
            IsPlayerActive = false; // プレイヤーの行動が終了した状態にする
            if (currentPlayerTurn == PlayerTurn.player1Turn)
            {
                player1.ChangeState(PlayerState.enOtherPlayerTurn);

                currentPlayerTurn = PlayerTurn.player2Turn;
            }

            else if (currentPlayerTurn == PlayerTurn.player2Turn)
            {
                player2.ChangeState(PlayerState.enOtherPlayerTurn);

                currentPlayerTurn = PlayerTurn.player3Turn;
            }

            else if (currentPlayerTurn == PlayerTurn.player3Turn)
            {
                player3.ChangeState(PlayerState.enOtherPlayerTurn);

                currentPlayerTurn = PlayerTurn.player4Turn;
            }

            else if (currentPlayerTurn == PlayerTurn.player4Turn)
            {
                player4.ChangeState(PlayerState.enOtherPlayerTurn);

                currentPlayerTurn = PlayerTurn.player1Turn;
            }
        }
    }


    void ChangeEnemyTurn()
    {
        player1.ChangeState(PlayerState.enEnemyTurn);
        player2.ChangeState(PlayerState.enEnemyTurn);
        player3.ChangeState(PlayerState.enEnemyTurn);
        player4.ChangeState(PlayerState.enEnemyTurn);
    }

    void EnemyAction()
    {
        if (IsEnemyActive == true)
        {
            enemy1.StartEnemyAction();
            enemy2.StartEnemyAction();
            enemy3.StartEnemyAction();
            Bossenemy.StartEnemyAction();

            IsEnemyActive = false; // 敵の行動が開始されたらフラグをリセット
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
