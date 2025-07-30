///
/// �^�[���}�l�[�W���[
///
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    // @todo for test
    [Header("�v���C���[����"), SerializeField]
    private GameObject[] playerObjects_;
    private List<PlayerBase> players_ = new List<PlayerBase>();
    private List<PlayerMoveBase> playerMoves_ = new List<PlayerMoveBase>();


    //[Header("�v���C���[����"), SerializeField]
    //private PlayerBase[] players_;

    //[Header("�v���C���[�̈ړ�"), SerializeField]
    //private PlayerMoveBase[] playerMoves_;

    [Header("�v���C���[HP"), SerializeField]
    private HPManager playerHP_;

    [Header("�G����"), SerializeField]
    private EnemyAttackControl[] enemies_;

    [Header("�^�[���e�L�X�g"), SerializeField]
    private TurnText turnText_;

    /// <summary>
    ///  �v���C���[�̌o�߃^�[����
    /// </summary>
    private int playerTurnCount_ = 0;

    /// <summary>
    /// �G�̌o�߃^�[����
    /// </summary>
    private int eneyTurnCount_ = MaxEnemyTurn;

    /// <summary>
    /// �G�̍ő�^�[����
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

        // Battle�R���[�`�����J�n
        StartCoroutine(Battle());
    }

    /// <summary>
    /// �o�g���t���[
    /// </summary>
    /// <returns></returns>
    IEnumerator Battle()
    {
        while (true)
        {
            // �v���C���[�̃^�[���J�n
            StartCoroutine(turnText_.DisplayText("PlayerTurn"));
            players_[playerTurnCount_].ChangeState(PlayerState.enLocalPlayerTurn);
            Debug.Log("�v���C���[�̃^�[��: " + playerTurnCount_);
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

            // �v���C�[�̃^�[�����I��������A���̃v���C���[�Ɉڍs
            Debug.Log("�v���C���[�̃^�[���I��: " + playerTurnCount_);
            players_[playerTurnCount_].ChangeState(PlayerState.enOtherPlayerTurn);
            playerTurnCount_++;
            if (playerTurnCount_ >= players_.Count)
            {
                playerTurnCount_ = 0;
            }

            // �Q�[���N���A�`�F�b�N
            if (EnemyCheckScript.m_instance.gameClearFlag)
            {
                yield break;
            }

            // �G�̃^�[���J�n
            eneyTurnCount_ -= 1;
            if (eneyTurnCount_ == 0)
            {
                foreach (var player in players_)
                {
                    player.ChangeState(PlayerState.enEnemyTurn);
                }
                Debug.Log("�G�̃^�[��");

                // �G�̍U�����J�n
                StartCoroutine(turnText_.DisplayText("EnemyTurn"));

                foreach (var enemy in enemies_)
                {
                    //enemy.ResetEnemyAction(); // �G�̍s�������Z�b�g
                }

                // �G�̍s����҂�
                // yield return new WaitUntil(() => );

                eneyTurnCount_ = MaxEnemyTurn;
            }

            // �v���C���[��HP�`�F�b�N
            if (playerHP_.m_isHpZero)
            {
                yield break;
            }

        }
    }
}
