using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPManager : MonoBehaviour
{
    public Image m_healImage;
    [SerializeField]
    private int m_maxHP;
    [SerializeField]
    private int m_minHP;
    private int m_hp;
    public bool m_isHpZero = false;

    // Start is called before the first frame update
    void Start()
    {
        m_hp = m_maxHP;
    }

    // Update is called once per frame
    void Update()
    {
        if(m_hp < m_minHP)
        {
            Debug.Log("ゲームオーバー");
            m_isHpZero = true;

            m_hp = m_minHP;
        }

        if(m_hp> m_maxHP)
        {
            m_hp = m_maxHP;
        }

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
        m_healImage.fillAmount = (float)m_hp / m_maxHP;
    }

    public void Heal(int heal)
    {
        m_hp += heal;
        m_healImage.fillAmount = (float)m_hp / m_maxHP;
    }
}
