///
/// �^�[���e�L�X�g
///
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TurnText : MonoBehaviour
{
    [Header("�e�L�X�g"), SerializeField]
    private TextMeshProUGUI text_;

    /// <summary>
    /// �^�[���e�L�X�g�\��
    /// </summary>
    public IEnumerator DisplayText(string text)
    {
        text_.text = text;

        // 2�b�҂�
        yield return new WaitForSeconds(2f);

        text_.text = string.Empty;
    }
}
