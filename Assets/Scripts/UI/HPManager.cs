using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPManager : MonoBehaviour
{
    public static HPManager m_instance;
    public Image m_healImage;           //HPバーのUI

    [SerializeField]
    private int m_maxHP = 100;          //最大HP
    [SerializeField]
    private int m_minHP = 0;            //最小HP
    private int m_hp;                   //現在のHP
    public bool m_isHpZero = false;     //HPが0になったか


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
        m_hp = m_maxHP; // 初期HPを最大HPに設定
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

    //被ダメージ処理
    public void Damage(int damage)
    {
        m_hp -= damage;
        UpdateHPUI();

        if (m_hp <= m_minHP)    //HPが最小値以下になったら
        {
            
            Debug.Log("ゲームオーバー");
            m_isHpZero = true;
            //HPが最小値を下回らないように
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
