using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;

public class GetItemScript : MonoBehaviour
{
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
        if(collision.gameObject.CompareTag("Item"))
        {
            // �A�C�e���̎�ނɉ����ď����𕪊�
            if (collision.gameObject.name == "HealItem")
            {
                // �q�[���A�C�e�����擾�����ꍇ�̏���
                Debug.Log("�񕜃A�C�e������ɓ��ꂽ�I");
                // �����Ƀq�[��������ǉ�
            }
            else if (collision.gameObject.name == "ATKItem")
            {
                // �U���̓A�b�v�A�C�e�����擾�����ꍇ�̏���
                Debug.Log("�U���̓A�b�v�A�C�e������ɓ��ꂽ�I");
                // �����ɍU���̓A�b�v������ǉ�
            }
            else if (collision.gameObject.name == "SpeedItem")
            {
                // �X�s�[�h�A�b�v�A�C�e�����擾�����ꍇ�̏���
                Debug.Log("�ړ����x�A�b�v�A�C�e������ɓ��ꂽ�I");
                // �����ɃX�s�[�h�A�b�v������ǉ�
            }
            // �A�C�e�����폜
            Destroy(collision.gameObject);
        }
    }
}
