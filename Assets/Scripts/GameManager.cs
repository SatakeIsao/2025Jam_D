using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    void Awake()
    {
        SceneManager.LoadScene("UI", LoadSceneMode.Additive);
    }

    public void OnclickStart()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void OnClickReStart()
    {
        SceneManager.LoadScene("Title");
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
