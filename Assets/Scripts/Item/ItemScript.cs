using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;

public class ItemScript : MonoBehaviour
{
    bool isGetItem = false; // �A�C�e�����擾�������ǂ���
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    void CheckItem()
    {
        if(gameObject.CompareTag("Item"))
        {
            GetItem(gameObject);
        }
    }

    void GetItem(GameObject item)
    {
        if (isGetItem == false)
        {
            isGetItem = true;
            Destroy(item); // �A�C�e�����V�[������폜
        }
    }
}
