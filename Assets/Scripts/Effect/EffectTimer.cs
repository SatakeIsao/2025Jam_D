using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectTimer : MonoBehaviour
{
    float timer = 20.0f;
    float effectLength = 20.0f; // アニメーションの長さを取得するための変数
    // Start is called before the first frame update
    void Start()
    {
        effectLength=GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length;
    }

    // Update is called once per frame
    void Update()
    {
        if (timer > 0f) 
        {
            timer -= Time.deltaTime;
            if (timer <= 0f) {
                Destroy(gameObject);
            }
        }
    }
}
