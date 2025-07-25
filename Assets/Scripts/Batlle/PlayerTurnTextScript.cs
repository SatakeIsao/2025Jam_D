using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerTurnTextScript : MonoBehaviour
{
    public TextMeshProUGUI TurnText;
    public float PlayerTurnTimer = 3;   //�`�̃^�[���ł��̕\������
    public bool playerTurnFlag = true; // �v���C���[�̃^�[�����ǂ����̃t���O

    // Start is called before the first frame update
    void Start()
    {
        TurnText.gameObject.SetActive(true); // �^�[���e�L�X�g��\������
    }

    void PlayerTurnTimerCountDown()
    {
        if (playerTurnFlag == true)
        {
            PlayerTurnTimer -= Time.deltaTime; // �^�C�}�[������������
            if (PlayerTurnTimer <= 0.0f) // �^�C�}�[��0�ȉ��ɂȂ�����
            {
                playerTurnFlag = false; // �v���C���[�̃^�[���t���O���I�t�ɂ���
                TurnText.gameObject.SetActive(false); // �^�[���e�L�X�g���\���ɂ���
                PlayerTurnTimer = 3.0f; // �^�C�}�[�����Z�b�g
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        PlayerTurnTimerCountDown(); // �^�[���^�C�}�[�̃J�E���g�_�E�������s
    }
}
