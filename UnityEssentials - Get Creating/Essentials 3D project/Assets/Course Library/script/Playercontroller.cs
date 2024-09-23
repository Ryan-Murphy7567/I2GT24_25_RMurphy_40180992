using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playercontroller : MonoBehaviour
{
    // private variables
 private  float speed =20.0f;
 private float turnSpeed = 20.0f;
 private float horizontalInput;
 private float forwardInput;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // move the car forward
        transform.Translate(Vector3.forward * Time.deltaTime * speed * forwardInput);
        //turning the car
        transform.Rotate(Vector3.up, Time.deltaTime*turnSpeed*horizontalInput);

        //player input commands
       horizontalInput = Input.GetAxis("Horizontal");
        forwardInput = Input.GetAxis("Vertical");
    }
}
