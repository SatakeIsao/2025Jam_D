using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPManager : MonoBehaviour
{
    public static HPManager m_instance;
    public Image m_healImage;           //HP�o�[��UI

    [SerializeField]
    private int m_maxHP = 100;          //�ő�HP
    [SerializeField]
    private int m_minHP = 0;            //�ŏ�HP
    private int m_hp;                   //���݂�HP
    public bool m_isHpZero = false;     //HP��0�ɂȂ�����


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
        m_hp = m_maxHP; // ����HP���ő�HP�ɐݒ�
    }

    // Update is called once per frame
    void Update()
    {
        
        if(Input.GetMouseButtonDown(2))
        {
            Damage(10);
        }

        if(Input.GetMouseButtonDown(1))
        {
            Heal(10);
        }
    }

    //��_���[�W����
    public void Damage(int damage)
    {
        m_hp -= damage;
        UpdateHPUI();

        if (m_hp <= m_minHP)    //HP���ŏ��l�ȉ��ɂȂ�����
        {
            
            Debug.Log("�Q�[���I�[�o�[");
            m_isHpZero = true;
            //HP���ŏ��l�������Ȃ��悤��
            m_hp = m_minHP;
        }
    }

    public void Heal(int heal)
    {
        m_hp += heal;
        UpdateHPUI();

        if (m_hp > m_minHP)
        {
            m_isHpZero = false;
        }

        if (m_hp > m_maxHP)
        {
            m_hp = m_maxHP;
        }
    }

    private void UpdateHPUI()
    {
        m_healImage.fillAmount = (float)m_hp / m_maxHP;
    }
}
