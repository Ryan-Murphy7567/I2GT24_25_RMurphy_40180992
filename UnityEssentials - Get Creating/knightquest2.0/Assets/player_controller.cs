using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_controller : MonoBehaviour
{ Rigidbody2D body;
  

    public float speed=20.0f;
    // Start is called before the first frame update
    void Start()
    { 
       body= GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    { float horizontalInput= Input.GetAxis("Horizontal");
 
      float verticalInput = Input.GetAxis("Vertical");
        Vector3 direction= new Vector3(horizontalInput, verticalInput,0);
        transform.Translate(direction*speed*Time.deltaTime);
    }
   
    
        
}
