using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLaser : MonoBehaviour
{
    EnemyStatus m_enemyStatus;

    //���[�U�[�̃I�u�W�F�N�g
    [SerializeField] private GameObject m_laserObject = null;
    //�_���[�W��
    [SerializeField] private int m_damageAmount = 0;
    //�U���܂ł̃^�[����
    [SerializeField] private int m_attackTurn = 0;
    //���݂̎c��^�[��
    private int m_currentTurn = 0;
    //���[�U�[�̒���
    [SerializeField] private float m_laserLength = 0.0f;
    //���[�U�[�̑���
    [SerializeField] private float m_laserWidth = 0.0f;
    //���[�U�[�̎�������
    [SerializeField] private float m_laserDuration = 0.0f;
    //���[�U�[�̑���
    private float m_laserSpeed = 1.0f;
    //�v���C���[�I�u�W�F�N�g���擾
    private GameObject[] m_players = new GameObject[4];
    //���݂̍Ŋ��̃v���C���[�܂ł̋���
    private float m_toNearestPlayerDis = 0.0f;
    //�Ŋ��̃v���C���[
    private GameObject m_nearestPlayer = null;
    //���[�U�[�̎ˏo�����x�N�g��
    private Vector2 m_toNearPlayerDir = Vector2.zero;

    public float laserLength = 10f;     // ���[�U�[�̒���
    public float damage = 20f;          // �_���[�W��
    public float duration = 0.2f;       // ���[�U�[�\������
    public LayerMask hitLayer;          // �����蔻�肷�郌�C���[

    private LineRenderer line;

    void Start()
    {
        m_enemyStatus = GetComponent<EnemyStatus>();

        m_players = GameObject.FindGameObjectsWithTag("Player");

        // LineRenderer��p��
        line = gameObject.AddComponent<LineRenderer>();
        line.startWidth = 0.1f;
        line.endWidth = 0.1f;
        line.material = new Material(Shader.Find("Sprites/Default"));
        line.startColor = Color.red;
        line.endColor = Color.red;
        line.enabled = false;
    }

    public void Fire(Vector2 direction)
    {
        // ���[�U�[�̎n�_
        Vector2 startPos = transform.position;
        Vector2 endPos = startPos + direction.normalized * laserLength;

        // Raycast �Ńq�b�g�`�F�b�N
        RaycastHit2D hit = Physics2D.Raycast(startPos, direction, laserLength, hitLayer);
        if (hit.collider != null)
        {
            endPos = hit.point;

            // �G�Ƀ_���[�W��^���鏈��
            GameObject hitObject = hit.collider.gameObject;
            if (hitObject.tag =="Player")
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
    /// �^�[���̃J�E���g�_�E���i�^�[������0�ɂȂ����烌�[�U�[�𔭎ˁj
    /// </summary>
    public void TurnCount()
    {
        m_currentTurn--;

        if (m_currentTurn <= 0)
        {
            CalcLaserVec();
            m_laserObject = Instantiate(m_laserObject, transform.position, Quaternion.LookRotation(m_toNearPlayerDir));
            m_currentTurn = m_attackTurn; // �^�[���������Z�b�g
        }
    }

    // Update is called once per frame
    void Update()
    {
        m_laserObject.transform.localScale += new Vector3(0.0f, m_laserSpeed, 0.0f); //���[�U�[��������ɐL�΂�
    }



    /// <summary>
    /// ���[�U�[�̎ˏo�������v�Z
    /// </summary>
    private void CalcLaserVec()
    {
        for (int i = 0; i < m_players.Length; i++)
        {
            if (m_players[i] == null) continue; // �v���C���[�����݂��Ȃ��ꍇ�̓X�L�b�v
            float playerDis = Vector2.Distance(m_enemyStatus.GetNewPos(), m_players[i].transform.position);
            if (playerDis < m_toNearestPlayerDis)
            {
                m_toNearestPlayerDis = playerDis;
            }
        }

        //m_toNearPlayerDir = m_toNearestPlayerDis.normalized;
    }

    /// <summary>
    /// �x�N�g�����Čv�Z
    /// </summary>
    private void VecRecalculation()
    {

    }
}
