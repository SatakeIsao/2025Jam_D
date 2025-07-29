using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReflectEffect : MonoBehaviour
{

   [SerializeField] private GameObject reflectEffectPrefab; // �G�t�F�N�g�̃v���n�u���w�肷�邽�߂̕ϐ�
   [SerializeField] private PlayerBase playerBase; // �v���C���[�̊�{�����Ǘ�����X�N���v�g���Q�Ƃ��邽�߂̕ϐ�
   [SerializeField] private PlayerMoveBase playerMoveBase; // �v���C���[�̈ړ����Ǘ�����X�N���v�g���Q�Ƃ��邽�߂̕ϐ�


    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("�v���C���[�ƏՓ˂��܂���: " + collision.gameObject.name);                                            
            Instantiate(reflectEffectPrefab, transform.position, Quaternion.identity); // �v���C���[�̌��݈ʒu�ɃG�t�F�N�g���o��
        }
        Destroy(gameObject); // �G�t�F�N�g���o������A�I�u�W�F�N�g���폜
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
