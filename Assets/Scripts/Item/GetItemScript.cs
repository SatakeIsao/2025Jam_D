using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;

public class GetItemScript : MonoBehaviour
{
    private const int HEAL_AMOUNT = 50; // �q�[���A�C�e���̉񕜗�
    //private const float ATK_BOOST_AMOUNT = 10.0f; // �U���̓A�b�v�A�C�e���̌��ʗ�
    private const float SPEED_BOOST_AMOUNT = 10.0f; // �X�s�[�h�A�b�v�A�C�e���̌��ʗ�
    private float playerSpeed;

    [SerializeField] private HPManager hpManager; // HPManager�̃C���X�^���X��ێ�����ϐ�
    [SerializeField] private PlayerBase playerBase; // �v���C���[�̊�{�����Ǘ�����X�N���v�g���Q�Ƃ��邽�߂̕ϐ�
    [SerializeField] private PlayerMoveBase playerMoveBase; // �v���C���[�̈ړ����Ǘ�����X�N���v�g���Q�Ƃ��邽�߂̕ϐ�

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // �Փ˂����I�u�W�F�N�g���A�C�e�����ǂ������`�F�b�N
        if (collision.gameObject.CompareTag("Item"))
        {
            // �A�C�e���̎�ނɉ����ď����𕪊�
            if (collision.gameObject.name == "HealItem")
            {
                // �q�[���A�C�e�����擾�����ꍇ�̏���
                Debug.Log("�񕜃A�C�e������ɓ��ꂽ�I");
                // �����Ƀq�[��������ǉ�
                hpManager.Heal(HEAL_AMOUNT); // ��Ƃ���50�񕜂��鏈����ǉ�
                AudioManager.Instance.PlaySE(AudioManager.SEType.enGetItem);

            }
            if (collision.gameObject.name == "ATKItem")
            {
                // �U���̓A�b�v�A�C�e�����擾�����ꍇ�̏���
                Debug.Log("�U���̓A�b�v�A�C�e������ɓ��ꂽ�I");
                // �����ɍU���̓A�b�v������ǉ�
                AudioManager.Instance.PlaySE(AudioManager.SEType.enGetItem);
            }
            if (collision.gameObject.name == "SpeedItem")
            {
                // �X�s�[�h�A�b�v�A�C�e�����擾�����ꍇ�̏���
                Debug.Log("�ړ����x�A�b�v�A�C�e������ɓ��ꂽ�I");
                // �����ɃX�s�[�h�A�b�v������ǉ�
                Debug.Log("���݂̈ړ����x: " + playerMoveBase.m_moveSpeed);
                playerSpeed = playerMoveBase.m_moveSpeed + SPEED_BOOST_AMOUNT; // ��Ƃ��Ĉړ����x��10.0f�A�b�v���鏈����ǉ�
                playerMoveBase.m_moveSpeed = playerSpeed; // �v���C���[�̈ړ����x���X�V
                Debug.Log("�V�����ړ����x: " + playerMoveBase.m_moveSpeed);
                AudioManager.Instance.PlaySE(AudioManager.SEType.enGetItem);

            }
            // �A�C�e�����폜
            Destroy(collision.gameObject);
        }
    }
}
