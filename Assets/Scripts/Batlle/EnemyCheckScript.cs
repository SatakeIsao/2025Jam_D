using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCheckScript : MonoBehaviour
{
    public static EnemyCheckScript m_instance; 

    GameObject[] enemyObjects;
    int EnemyNum;   //�c��̓G�̐�

    public bool gameClearFlag = false; //�Q�[���N���A�t���O

    void Awake()
    {
        if(m_instance == null)
        {
            m_instance = this;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
       
    }

    void CheckEnemyCount()
    {
        enemyObjects = GameObject.FindGameObjectsWithTag("Enemy"); // "Enemy"�^�O�����I�u�W�F�N�g��S�Ď擾
        EnemyNum = enemyObjects.Length; // �c��̓G�̐����擾

        if (EnemyNum <= 0) // �c��̓G��0�ȉ��Ȃ�
        {
            gameClearFlag = true; // �Q�[���N���A�t���O�𗧂Ă�
            Debug.Log("�Q�[���N���A");
        }
    }

    // Update is called once per frame
    void Update()
    {
       CheckEnemyCount(); // ���t���[���G�̐����`�F�b�N
    }
}