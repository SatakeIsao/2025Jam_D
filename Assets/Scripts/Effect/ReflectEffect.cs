using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReflectEffect : MonoBehaviour
{

   [SerializeField] private GameObject reflectEffectPrefab; // エフェクトのプレハブを指定するための変数
   [SerializeField] private PlayerBase playerBase; // プレイヤーの基本情報を管理するスクリプトを参照するための変数
   [SerializeField] private PlayerMoveBase playerMoveBase; // プレイヤーの移動を管理するスクリプトを参照するための変数


    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("プレイヤーと衝突しました: " + collision.gameObject.name);                                            
            Instantiate(reflectEffectPrefab, transform.position, Quaternion.identity); // プレイヤーの現在位置にエフェクトを出す
        }
        Destroy(gameObject); // エフェクトを出した後、オブジェクトを削除
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
