using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AnimDialog : MonoBehaviour
{
    //アニメーター
    [SerializeField] private Animator m_animator;
    //アニメーターコントローラーのレイヤー
    [SerializeField] private int m_layer;
    //IsOpenフラグ
    //raadoly->読み込み専用（定数）
    private static readonly int m_paramIsOpen = Animator.StringToHash("isOpen");
    //ダイアログは開いているかどうか
    //getプロパティで、ゲームオブジェクトがアクティブかどうかを返す
    public bool m_isOpen => gameObject.activeSelf;
    //アニメーション中かどうか
    //set→自クラスだけ設定可能、get→外部から読み取り可能
    public bool m_isTransition { get; private set; }

    //ダイアログを開く
    public void Open()
    {
        //不正操作の防止
        if (m_isOpen || m_isTransition) return;
        //パネル自体をアクティブにする
        gameObject.SetActive(true);
        //m_isOpenフラグをセット
        m_animator.SetBool(m_paramIsOpen, true);
        //アニメーション待機
        StartCoroutine(WaitAnimation("Shown"));
    }

    //ダイアログを閉じる
    public void Close()
    {
        //不正操作の防止
        if (!m_isOpen || m_isTransition) return;
        //m_isOpenフラグをクリア
        m_animator.SetBool(m_paramIsOpen, false);
        //アニメーション待機し、終わったらパネル自体を非アクティブにする
        StartCoroutine(WaitAnimation("Hidden", () => gameObject.SetActive(false)));
    }

    //開閉アニメーションの待機コルーチン
    private IEnumerator WaitAnimation(string stateName,UnityAction onCompled=null)
    {
        m_isTransition = true;
        yield return new WaitUntil(() =>
        {
            //ステートが変化し、アニメーションが終了するまでループ
            var state = m_animator.GetCurrentAnimatorStateInfo(m_layer);
            return state.IsName(stateName) && state.normalizedTime >= 1;
        });
        m_isTransition = false;
        onCompled?.Invoke();
    }
}
