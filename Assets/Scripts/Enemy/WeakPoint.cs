using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeakPoint : MonoBehaviour
{
    private bool m_isHIt = false;//��_�Ƀq�b�g���Ă��邩
    private EnemyStatus m_enemyStatus;
    private EnemyReaction m_enemyReaction;

    public bool IsHit
    {
        get { return m_isHIt; }
        set { m_isHIt = value; }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        GameObject other = collider.gameObject;

        if (other.tag == "Player")
        {
            m_isHIt = true; // �q�b�g�����t���O�𗧂Ă�
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        GameObject other = collider.gameObject;

        if (other.tag == "Player")
        {
            m_isHIt = false; // �q�b�g�����t���O�𗧂Ă�
        }
    }



    // Start is called before the first frame update
    void Start()
    {
        m_enemyStatus=GetComponentInParent<EnemyStatus>();
        m_enemyReaction = GetComponentInParent<EnemyReaction>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
