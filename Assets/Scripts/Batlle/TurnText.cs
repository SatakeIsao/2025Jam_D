///
/// ターンテキスト
///
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TurnText : MonoBehaviour
{
    [Header("テキスト"), SerializeField]
    private TextMeshProUGUI text_;

    /// <summary>
    /// ターンテキスト表示
    /// </summary>
    public IEnumerator DisplayText(string text)
    {
        text_.text = text;

        // 2秒待つ
        yield return new WaitForSeconds(2f);

        text_.text = string.Empty;
    }
}
