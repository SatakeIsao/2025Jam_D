using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeakPoint : MonoBehaviour
{
    private bool m_isHIt = false;//弱点にヒットしているか
    private EnemyReaction m_enemyReaction;
    private BoxCollider2D m_collider;

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
            m_isHIt = true; // ヒットしたフラグを立てる
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        GameObject other = collider.gameObject;

        if (other.tag == "Player")
        {
            m_isHIt = false; // ヒットしたフラグを立てる
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        m_collider = transform.parent.GetComponent<BoxCollider2D>();
        //弱点の位置の初期配置
        m_enemyReaction = transform.parent.GetComponent<EnemyReaction>();

        switch(m_enemyReaction.WeakPointPos)
        {
            case (int)EnWeakPointPos.enTop: // 上
                transform.position = transform.parent.position + new Vector3(0.0f, m_collider.size.y / 2, 0.0f);
                break;
            case (int)EnWeakPointPos.enButton: // 下
                transform.position = transform.parent.position + new Vector3(0.0f, -m_collider.size.y / 2, 0.0f);
                break;
            case (int)EnWeakPointPos.enLeft: // 左
                transform.position = transform.parent.position + new Vector3(-m_collider.size.x / 2, 0.0f, 0.0f);
                break;
            case (int)EnWeakPointPos.enRight: // 右
                transform.position = transform.parent.position + new Vector3(m_collider.size.x / 2, 0.0f, 0.0f);
                break;
            default: // 空
                break;
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
