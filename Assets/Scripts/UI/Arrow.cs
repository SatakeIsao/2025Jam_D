using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Arrow : MonoBehaviour
{
    private GameObject arrow;
    private MauseInput m_mauseInput;
    private RectTransform m_arrowRectTransform;

    void Awake()
    {
        if (transform.parent != null && transform.parent.parent != null)
        {
            Debug.Log("マウスインプットのコンポーネントを取得");
            m_mauseInput = transform.parent.parent.GetComponent<MauseInput>();
        }
        if (m_mauseInput == null)
        {
            Debug.LogError("MouseInput が見つかりませんでした");
        }
        arrow = this.gameObject;
        m_arrowRectTransform = arrow.GetComponent<RectTransform>();
    }

    // Start is called before the first frame update
    void Start()
    {
        toggleArrow(false);
        m_mauseInput.OnDragStarted += () => toggleArrow(true); //ドラッグ開始時に矢印を表示する。
        m_mauseInput.OnDragEnded += () => toggleArrow(false); //ドラッグ終了時に矢印を非表示にする。
        m_mauseInput.OnArrowLengthUpdated += (scale) => SetLengthUpdated(scale*0.1f); //ドラッグ中の矢印のスケールを更新する。
        m_mauseInput.OnArrowRotationUpdated += (angle) => RotateArrow(angle); //ドラッグ中の矢印の角度を更新する。
        RotateArrow(11.0f);
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void toggleArrow(bool IsActive)
    {
        //矢印の表示・非表示を切り替える。
        arrow.SetActive(IsActive);
    }

    public void SetLengthUpdated(float scale)
    {
        //矢印のスケールを設定する。
        //入力と逆の方向に矢印を向けるため、X軸のスケールをマイナスにする。
        arrow.transform.localScale = new Vector3(-scale,1.0f , 1.0f);
    }

    public void RotateArrow(float angle)
    {
        //矢印の角度を設定する。
        m_arrowRectTransform.rotation = Quaternion.Euler(0, 0,angle);
        if (m_arrowRectTransform == null)
        {
            Debug.LogError("RectTransform が見つかりませんでした: " + gameObject.name);
        }
    }

}
