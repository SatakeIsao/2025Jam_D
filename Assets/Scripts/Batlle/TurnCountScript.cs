using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnCountScript : MonoBehaviour
{
    [SerializeField] private BattleManager battleManager; // �v���C���[�̈ړ��X�N���v�g���Q�Ƃ��邽�߂̕ϐ�

    public int EnemyTurnCount = 1; // �G�̃^�[�������Ǘ�����ϐ�

    // Start is called before the first frame update
    void Start()
    {

    }

    // �^�[�����𑝂₷���\�b�h
    void Turn()
    {
        if(battleManager.IsPlayerActive==false)
        {
            EnemyTurnCount -= 1; // �G�̃^�[���������炷
            if (EnemyTurnCount <= 0)
            {
                battleManager.IsEnemyActive = true; // �G�̃^�[�����I��
                EnemyTurnCount = 1; // �^�[���������Z�b�g
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        Turn();
    }
}
