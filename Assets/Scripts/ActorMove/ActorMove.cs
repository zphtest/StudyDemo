using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorMove : MonoBehaviour
{
    public Transform m_ActorTransform;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            Debug.Log("Press: W");
            
            //m_ActorTransform.position += forward;
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            Debug.Log("Press: A");
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            Debug.Log("Press: S");
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            Debug.Log("Press: D");
        }
    }
}
