using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerTurnTextScript : MonoBehaviour
{
    public TextMeshProUGUI TurnText;
    [SerializeField] private BattleManager battleManager; // BattleManager�̎Q��
    public float PlayerTurnTimer = 3.0f;   //�`�̃^�[���ł��̕\������

    // Start is called before the first frame update
    void Start()
    {
        TurnText.gameObject.SetActive(false); // �^�[���e�L�X�g��\������
    }

    void PlayerTurnTimerCountDown()
    {
        if (battleManager.IsPlayerActive == true)
        {
            TurnText.gameObject.SetActive(true); // �^�[���e�L�X�g��\������
            PlayerTurnTimer -= Time.deltaTime; // �^�[���^�C�}�[���J�E���g�_�E��
            if (PlayerTurnTimer <= 0)
            {
                TurnText.gameObject.SetActive(false); // �^�[���e�L�X�g���\���ɂ���
                PlayerTurnTimer = 0; // �^�[���^�C�}�[�����Z�b�g
            }
        }
        else
        {
            TurnText.gameObject.SetActive(false); // �^�[���e�L�X�g���\���ɂ���
            PlayerTurnTimer = 3; // �^�[���^�C�}�[�����Z�b�g
        }

    }

    // Update is called once per frame
    void Update()
    {
        PlayerTurnTimerCountDown(); // �^�[���^�C�}�[�̃J�E���g�_�E�������s
    }
}
