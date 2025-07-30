using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLaser : MonoBehaviour
{
    EnemyStatus m_enemyStatus;

    //�_���[�W��
    [SerializeField] private int m_damage = 0;
    //�U���܂ł̃^�[����
    [SerializeField] private int m_attackTurn = 0;
    //���݂̎c��^�[��
    private int m_currentTurn = 0;
    //�v���C���[�I�u�W�F�N�g���擾
    private GameObject[] m_players = new GameObject[4];
    //���݂̍Ŋ��̃v���C���[�܂ł̋���
    private Vector2 m_toNearestPlayerDis = Vector2.zero;
    //�Ŋ��̃v���C���[
    private GameObject m_nearestPlayer = null;
    //���[�U�[�̎ˏo�����x�N�g��
    private Vector2 m_toNearPlayerDir = Vector2.zero;

    private float laserLength = 20.0f;     // ���[�U�[�̒���
    private float duration = 0.7f;       // ���[�U�[�\������
    private LayerMask hitLayer;          // �����蔻�肷�郌�C���[

    private LineRenderer line;

    void Start()
    {
        m_enemyStatus = GetComponent<EnemyStatus>();

        m_players = GameObject.FindGameObjectsWithTag("Player");

        // LineRenderer��p��
        line = gameObject.AddComponent<LineRenderer>();
        line.startWidth = 1.0f;
        line.endWidth = 1.0f;
        line.material = new Material(Shader.Find("Sprites/Default"));
        line.startColor = Color.red;
        line.endColor = Color.red;
        line.enabled = false;
    }

    

    // Update is called once per frame
    void Update()
    {
    }

    /// <summary>
    /// �^�[���̃J�E���g�_�E���i�^�[������0�ɂȂ����烌�[�U�[�𔭎ˁj
    /// </summary>
    public void TurnCount()
    {
        m_currentTurn--;

        if (m_currentTurn <= 0)
        {
            CalcLaserVec();
            Fire(); // ���[�U�[�𔭎�
            m_currentTurn = m_attackTurn; // �^�[���������Z�b�g
        }
    }

    public void Fire()
    {
        // ���[�U�[�̎n�_
        Vector2 startPos = transform.position;
        Vector2 endPos = startPos + m_toNearPlayerDir * laserLength;

        // Raycast �Ńq�b�g�`�F�b�N
        RaycastHit2D hit = Physics2D.Raycast(startPos, m_toNearPlayerDir, laserLength, hitLayer);
        if (hit.collider != null)
        {
            GameObject hitObject = hit.collider.gameObject;
            if (hitObject.tag == "Player")
            {
                //TODO �v���C���[�Ƀ_���[�W

            }
        }

        // ���[�U�[��\��
        StartCoroutine(ShowLaser(startPos, endPos));
    }

    private System.Collections.IEnumerator ShowLaser(Vector2 start, Vector2 end)
    {
        line.enabled = true;
        line.SetPosition(0, start);
        line.SetPosition(1, end);
        yield return new WaitForSeconds(duration);
        line.enabled = false;
    }

    /// <summary>
    /// ���[�U�[�̎ˏo�������v�Z
    /// </summary>
    private void CalcLaserVec()
    {
        for (int i = 0; i < m_players.Length; i++)
        {
            if (m_players[i] == null) continue; // �v���C���[�����݂��Ȃ��ꍇ�̓X�L�b�v
            Vector2 playerDis = m_players[i].transform.position - transform.position;
            if (playerDis.magnitude > m_toNearestPlayerDis.magnitude)
            {
                m_toNearestPlayerDis = playerDis;
            }
        }

        m_toNearPlayerDir = m_toNearestPlayerDis.normalized;
    }
}
