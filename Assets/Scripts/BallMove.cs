using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMove : MonoBehaviour
{
    Vector2 m_vel = new Vector2(10.0f, 10.0f);
    float m_time=1.0f;
    Rigidbody2D rigidBody;

    // Start is called before the first frame update
    void Start()
    {
        
        rigidBody = this.GetComponent<Rigidbody2D>();
        rigidBody.velocity = m_vel;
        //if(rigidBody.velocity >= Vector2(0.0f,0.0f))

    }

    // Update is called once per frame
    void Update()
    {
        m_time += Time.deltaTime * 0.00005f;
        //ë¨ìxÇÃå∏êäÇ≥ÇπÇƒÇ¢ÇÈ
        rigidBody.velocity *=  1/m_time;
    }
}
