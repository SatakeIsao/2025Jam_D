using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    [SerializeField] private PlayerBase player1; // �v���C���[�̊�{�����Q�Ƃ��邽�߂̕ϐ�
    [SerializeField] private PlayerBase player2; // �v���C���[�̊�{�����Q�Ƃ��邽�߂̕ϐ�    
    [SerializeField] private PlayerBase player3; // �v���C���[�̊�{�����Q�Ƃ��邽�߂̕ϐ�
    [SerializeField] private PlayerBase player4; // �v���C���[�̊�{�����Q�Ƃ��邽�߂̕ϐ�
    [SerializeField] private PlayerMoveBase playerMove; // �v���C���[�̈ړ��X�N���v�g���Q�Ƃ��邽�߂̕ϐ�

    [SerializeField] private EnemyAttackControl enemy1; // �G�̍U���X�N���v�g���Q�Ƃ��邽�߂̕ϐ�
    [SerializeField] private EnemyAttackControl enemy2; // �G�̍U���X�N���v�g���Q�Ƃ��邽�߂̕ϐ�
    [SerializeField] private EnemyAttackControl enemy3; // �G�̍U���X�N���v�g���Q�Ƃ��邽�߂̕ϐ�
    [SerializeField] private EnemyAttackControl Bossenemy; // �G�̍U���X�N���v�g���Q�Ƃ��邽�߂̕ϐ�

    [SerializeField] private HPManager playerHP; // �v���C���[�`�[����HP���Ǘ�����X�N���v�g���Q�Ƃ��邽�߂̕ϐ�

    [SerializeField] private EnemyCheckScript enemyCheck; // �G�̏�Ԃ��`�F�b�N����X�N���v�g���Q�Ƃ��邽�߂̕ϐ�

    [SerializeField] private TurnCountScript enemyTurnCount; // �^�[�������Ǘ�����X�N���v�g���Q�Ƃ��邽�߂̕ϐ�

    //�����t���O
    private bool isLose = false;

    // �v���C���[�ƓG�̃A�N�e�B�u��Ԃ��Ǘ�����t���O
    public bool IsEnemyActive = false;
    public bool IsPlayerActive = true;

    // BattleState��PlayerTurn���`����񋓌^
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

        // Battle�R���[�`�����J�n
        StartCoroutine(Battle());
    }

    IEnumerator Battle()
    {
        while (!isLose)
        {
            // �v���C���[�̃^�[��
            CheckPlayerTurn();
            Debug.Log("�v���C���[�̃^�[��: " + currentPlayerTurn);
            currentBattleState = BattleState.enPlayerTurn;

            // �v���C���[�̍s����҂�
            yield return new WaitUntil(() => playerMove.HasStoppedAfterPull());

            //�G�����ꂽ���m�F
            if (EnemyCheckScript.m_instance.gameClearFlag == true)
            {
                currentBattleState = BattleState.enGameClear;
                yield break; // �Q�[���N���A���̓R���[�`�����I��
            }

            // �v���C���[�̏�Ԃ�ύX
            PlayerStateChange();
            Debug.Log("�v���C���[�̏�Ԃ�ύX: " + currentPlayerTurn);
            currentBattleState = BattleState.enChangePlayerTurn;

            if (enemyTurnCount.EnemyTurnCount == 0)
            {
                currentBattleState = BattleState.enEnemyTurn;
                // �G�̃^�[��
                ChangeEnemyTurn();
                Debug.Log("�G�̃^�[��");

                // �G�̍U�����J�n
                EnemyAction();
                Debug.Log("�G�̍U�����J�n");
            }

            // �G�̍s����҂�
            yield return new WaitUntil(() => IsEnemyActive == false);

            // �����œG�̍s�����ʂ��`�F�b�N���āA�v���C���[���|���ꂽ���ǂ������m�F����
            if (playerHP.m_isHpZero == true)
            {
                currentBattleState = BattleState.enGameOver;
                isLose = true; // �v���C���[���|���ꂽ��Q�[���I�[�o�[
                yield break; // �Q�[���I�[�o�[���̓R���[�`�����I��
            }

        }
    }
    void CheckPlayerTurn()
    {

        IsPlayerActive = true; // �v���C���[�̍s�����\�ȏ�Ԃɂ���

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
            IsPlayerActive = false; // �v���C���[�̍s�����I��������Ԃɂ���
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

            IsEnemyActive = false; // �G�̍s�����J�n���ꂽ��t���O�����Z�b�g
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
