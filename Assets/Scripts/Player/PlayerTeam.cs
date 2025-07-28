using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PlayerTeam : MonoBehaviour
{
    [SerializeField] private List<GameObject> playerObjects;
    public List<GameObject> PlayerObjects => playerObjects;  // �O����ǂݎ���p
    int m_totalDamage = 0;
    int m_teamHP = 0;

    void Awake()
    {
    }
    // Start is called before the first frame update
    void Start()
    {
        //�`�[���̏���HP��ݒ�B
        InitializeTeamHP();

        // �e�v���C���[�̃_���[�W�C�x���g�Ƀ��\�b�h��o�^
        foreach (var obj in playerObjects)
        {
            var player = obj.GetComponent<PlayerBase>();
            player.OnDamaged += TakeDamage;
        }

    }

    void InitializeTeamHP()
    {
        //�`�[��HP���v���C���[��HP�̍��v�ɐݒ�
        m_teamHP = playerObjects.Sum(obj => obj.GetComponent<PlayerBase>().GetPalamata().hp);
    }


    /// <summary>
    /// �`�[���̌��݂�HP���擾���郁�\�b�h�B
    /// </summary>
    /// <returns></returns>
    public int GetCurrentHP()
    {
        return m_teamHP;
    }


    /// <summary>
    /// �`�[��HP��0�ȉ����ǂ������`�F�b�N���郁�\�b�h�B
    /// �[���ȉ��Ȃ�true��Ԃ��B
    /// </summary>
    /// <returns></returns>
    public bool IsDead() {
        if (m_teamHP <= 0)
        {
            return true;
        }
        return false;
    }


    // Update is called once per frame
    void Update()
    {

    }

    /// <summary>
    /// �_���[�W���󂯂��Ƃ��ɌĂяo����郁�\�b�h�B
    /// </summary>
    /// <param name="playerBase"></param>
    public void TakeDamage(PlayerBase playerBase)
    {
        m_totalDamage = playerObjects.Sum(obj => obj.GetComponent<PlayerBase>().GetDamagae());
        m_teamHP -= m_totalDamage;
    }

}