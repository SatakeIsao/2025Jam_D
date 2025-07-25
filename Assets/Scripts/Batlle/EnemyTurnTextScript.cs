using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemyTurnTextScript : MonoBehaviour
{
    public TextMeshProUGUI EnemyTurnText;

    public PlayerMoveBase playerMoveBase; // �v���C���[�̈ړ��X�N���v�g���Q�Ƃ��邽�߂̕ϐ�

    public PlayerTurnTextScript playerTurnTextScript; // �v���C���[�̃^�[���e�L�X�g�X�N���v�g���Q�Ƃ��邽�߂̕ϐ�

    public TurnCountScript turnCountScript; // �^�[�������Ǘ�����X�N���v�g���Q�Ƃ��邽�߂̕ϐ�

    public float enemyTurnTimer = 2.0f; // �G�̃^�[���\������

    // Start is called before the first frame update
    void Start()
    {
        EnemyTurnText.gameObject.SetActive(false); // �G�̃^�[���e�L�X�g���\���ɂ���
    }

    void EnemyTurnFlag()
    {
        if (turnCountScript.EnemyTurnCount == 0)
        {
            if (playerTurnTextScript.playerTurnFlag == false)
            {
                if (playerMoveBase.GetIsStop())
                {
                    EnemyTurnText.gameObject.SetActive(true); // �G�̃^�[���e�L�X�g��\������
                    enemyTurnTimer -= Time.deltaTime; // �^�C�}�[������������
                    if (enemyTurnTimer <= 0.0f) // �^�C�}�[��0�ȉ��ɂȂ�����
                    {
                        EnemyTurnText.gameObject.SetActive(false); // �G�̃^�[���e�L�X�g���\���ɂ���
                        enemyTurnTimer = 2.0f; // �^�C�}�[�����Z�b�g
                    }
                }
                else
                {
                    EnemyTurnText.gameObject.SetActive(false); // �G�̃^�[���e�L�X�g���\���ɂ���
                }
            }
        }

        else
        {
            EnemyTurnText.gameObject.SetActive(false); // �G�̃^�[���e�L�X�g���\���ɂ���
        }
    }

    // Update is called once per frame
    void Update()
    {
        EnemyTurnFlag(); // �G�̃^�[���t���O���X�V
    }
}
