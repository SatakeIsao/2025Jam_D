using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemyTurnTextScript : MonoBehaviour
{
    public TextMeshProUGUI EnemyTurnText;

    [SerializeField] private BattleManager battleManager; // BattleManager���Q�Ƃ��邽�߂̕ϐ�

    public float enemyTurnTimer = 2.0f; // �G�̃^�[���\������

    // Start is called before the first frame update
    void Start()
    {
        EnemyTurnText.gameObject.SetActive(false); // �G�̃^�[���e�L�X�g���\���ɂ���
    }

    void EnemyTurnFlag()
    {
        if (battleManager.IsEnemyActive == true)
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

    // Update is called once per frame
    void Update()
    {
        EnemyTurnFlag(); // �G�̃^�[���t���O���X�V
    }
}
