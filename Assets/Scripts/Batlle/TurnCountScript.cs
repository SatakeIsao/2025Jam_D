using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnCountScript : MonoBehaviour
{
    [SerializeField] private PlayerMoveBase playerMoveBase; // �v���C���[�̈ړ��X�N���v�g���Q�Ƃ��邽�߂̕ϐ�

    public int EnemyTurnCount = 1; // �G�̃^�[�������Ǘ�����ϐ�
    public int EnemyTurn = 1; // �G�̃^�[������\������ϐ�
    public float enemyAttackTimer = 2.0f; //���u��
                                          //
    // Start is called before the first frame update
    void Start()
    {

    }

    // �^�[�����𑝂₷���\�b�h
    void Turn()
    {
        if (playerMoveBase.HasStoppedAfterPull())
        {
            // �v���C���[���ړ����~�����ꍇ�A�G�̃^�[���������炷
            EnemyTurn = Mathf.Max(EnemyTurn, 0); // �G�̃^�[������0�����ɂȂ�Ȃ��悤�ɂ���
            EnemyTurn =EnemyTurnCount - 1; // �G�̃^�[���������炷
            EnemyTurn = EnemyTurnCount; // �G�̃^�[�������X�V
            if (EnemyTurn <= 0)
            {
                Debug.Log("�G�̃^�[���ł��B");
                EnemyTurnCount = 1; // �G�̃^�[���������Z�b�g
            }

            else
            {
                Debug.Log("�G�̍U���܂Ō�:" + EnemyTurn);
            }

        }
    }

    // Update is called once per frame
    void Update()
    {
        Turn();
    }
}
