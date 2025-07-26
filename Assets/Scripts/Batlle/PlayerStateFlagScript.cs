using UnityEngine;

public class PlayerStateFlagScript : MonoBehaviour
{
    [SerializeField]
    private int playerNumber = 1; // プレイヤー番号

    //void Start()
    //{
    //    playerBase1.ChangeState(PlayerState.enLocalPlayerTurn); // プレイヤー1の状態をローカルプレイヤーターンに設定
    //    playerBase2.ChangeState(PlayerState.enOtherPlayerTurn); // プレイヤー2の状態を他のプレイヤーターンに設定
    //    playerBase3.ChangeState(PlayerState.enOtherPlayerTurn); // プレイヤー3の状態を他のプレイヤーターンに設定
    //    playerBase4.ChangeState(PlayerState.enOtherPlayerTurn); // プレイヤー4の状態を他のプレイヤーターンに設定
    //}

    //void CheckStatus()
    //{
    //    // TODO:hayasi 何かしらフラグを追加しないとゲーム開始早々呼ばれる。理想はキャラクターを操作し止まった時を取得しないといけない。
    //    if (playerMove1.GetIsStop())
    //    {
    //        playerBase2.ChangeState(PlayerState.enLocalPlayerTurn);
    //        // プレイヤー1の状態をチェック
    //        if (enemyTurnCount.EnemyTurnCount <= 0)
    //        {
    //            playerBase1.ChangeState(PlayerState.enEnemyTurn);
    //            Debug.Log("プレイヤー1の状態を敵のターンに変更しました。");
    //        }
    //        else
    //        {
    //            playerBase1.ChangeState(PlayerState.enOtherPlayerTurn);
    //            Debug.Log("プレイヤー1の状態を他のプレイヤーターンに変更しました。");
    //        }
    //    }
    //    else
    //    {
    //        playerBase1.ChangeState(PlayerState.enLocalPlayerTurn);
    //    }


    //    if (playerMove2.GetIsStop())
    //    {
    //        if (enemyTurnCount.EnemyTurnCount <= 0)
    //        {
    //            playerBase2.ChangeState(PlayerState.enEnemyTurn);
    //        }
    //        else
    //        {
    //            playerBase2.ChangeState(PlayerState.enOtherPlayerTurn);
    //        }
    //    }

    //    else
    //    {
    //        playerBase2.ChangeState(PlayerState.enLocalPlayerTurn);
    //    }

    //    if (playerMove3.GetIsStop())
    //    {
    //        if (enemyTurnCount.EnemyTurnCount <= 0)
    //        {
    //            playerBase3.ChangeState(PlayerState.enEnemyTurn);
    //        }
    //        else
    //        {
    //            playerBase3.ChangeState(PlayerState.enOtherPlayerTurn);
    //        }
    //    }

    //    else
    //    {
    //        playerBase3.ChangeState(PlayerState.enLocalPlayerTurn);
    //    }

    //    if (playerMove4.GetIsStop())
    //    {
    //        if (enemyTurnCount.EnemyTurnCount <= 0)
    //        {
    //            playerBase4.ChangeState(PlayerState.enEnemyTurn);
    //        }
    //        else
    //        {
    //            playerBase4.ChangeState(PlayerState.enOtherPlayerTurn);
    //        }
    //    }

    //    else
    //    {
    //        playerBase4.ChangeState(PlayerState.enLocalPlayerTurn);
    //    }

    //}

    //// Update is called once per frame
    //void Update()
    //{
    //    CheckStatus(); // プレイヤーの状態をチェック
    //}
}
