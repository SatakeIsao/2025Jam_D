using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackControl : MonoBehaviour
{
    EnemyCharge m_enemyCharge;
    EnemyLaser m_enemyLaser;

    //敵が行動してもよいか
    private bool m_isCanAction = false;


    /// <summary>
    /// 敵の行動を開始する
    /// </summary>
    public void StartEnemyAction()
    {
        m_isCanAction = true;
    }
    

    public bool GetIsInAction()
    {
        return m_isCanAction;
    }

    // Start is called before the first frame update
    void Start()
    {
        m_enemyCharge = GetComponent<EnemyCharge>();
        m_enemyLaser = GetComponent<EnemyLaser>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!m_isCanAction)
        {
            return;
        }

        //レーザー
        //突進攻撃
        if (m_enemyLaser != null)
        {
            m_enemyLaser.TurnCount();

            if (m_enemyLaser.GetIsInAction() == true)
            {
                return;
            }
        }

        //突進攻撃
        if (m_enemyCharge != null)
        {
            m_enemyCharge.TurnCount();

            if (m_enemyCharge.GetIsInAction() == true)
            {
                return;
            }
        }

        m_isCanAction = false;
    }
}
