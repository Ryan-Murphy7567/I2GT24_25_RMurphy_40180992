using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_controller : MonoBehaviour
{ Rigidbody2D body;
    float horizontal;
    float vertical;
    float moveLimiter = 0.7f;

    public float speed=20.0f;
    // Start is called before the first frame update
    void Start()
    { 
       body= GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxisRaw("horizontal"); //-1 is left
        vertical = Input.GetAxisRaw("Vertical"); // -1 is down
    }
    private void FixedUpdate()
    {
        if (horizontal != 0 && vertical != 0)
        {
            horizontal *= moveLimiter;
            vertical *= moveLimiter;
        }//check for diagonal movement
        body.velocity = new Vector2(horizontal * speed, vertical * speed);
    }
        
}
