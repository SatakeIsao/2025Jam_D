using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnCountScript : MonoBehaviour
{
    public int EnemyTurnCount = 3; // �G�̃^�[�������Ǘ�����ϐ�

    public float enemyAttackTimer = 2.0f; //���u�� 
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // �^�[�����𑝂₷���\�b�h
    void Turn()
    {
        // A�L�[�������ꂽ��^�[�����𑝂₷
        if (Input.GetKeyDown(KeyCode.A))
        {
            EnemyTurnCount--;
        }

        // �X�y�[�X�L�[�������ꂽ�猻�݃^�[�����̊m�F
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log(" �G���U�����Ă���܂Ŏc��^�[����:" + EnemyTurnCount);
        }

        if(EnemyTurnCount==0)
        {
            enemyAttackTimer -= Time.deltaTime; // �^�C�}�[������������
            if (enemyAttackTimer <= 0.0f) // �^�C�}�[��0�ȉ��ɂȂ�����
            {
                enemyAttackTimer = 2.0f; // �^�C�}�[�����Z�b�g
                EnemyTurnCount = 3; // �^�[���������Z�b�g
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
       Turn();
    }
}
