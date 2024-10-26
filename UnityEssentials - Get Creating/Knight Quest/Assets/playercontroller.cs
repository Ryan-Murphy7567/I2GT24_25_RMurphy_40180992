using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class playercontroller : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {public float horizontalInput;
    public float speed;
    
    }

    // Update is called once per frame
    void Update()
    {
    horizontalInput = Input.GetAxis("Horizontal");
    transform.Translate(Vector3.forward*Time.deltaTime*speed);
    }
}
