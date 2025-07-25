using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackControl : MonoBehaviour
{
    EnemyCharge m_enemyCharge;

    //�G���s�����Ă��悢��
    private bool m_isCanAction = false;


    /// <summary>
    /// �G�̍s�����J�n����
    /// </summary>
    public void StartEnemyAction()
    {
        m_isCanAction = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        m_enemyCharge = GetComponent<EnemyCharge>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!m_isCanAction)
        {
            return;
        }

        //�ːi�U��
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
