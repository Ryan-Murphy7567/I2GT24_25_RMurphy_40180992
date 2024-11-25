using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class SidescrollPlayerController : MonoBehaviour
{ //this script will allow the player to navigate the enviornment in the 2d levels. platforming/jumping to be added for final build.
    public float speed = 20.0f;
    public Animator animator;
    private Vector2 direction;

    public Rigidbody2D rb;
    

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    public void Update()
    {
        direction = new Vector2(Input.GetAxis("Horizontal"), 0).normalized; //movement HORIZONTAL
        animator.SetFloat("speed", MathF.Abs(direction.magnitude * speed));

        if (rb.velocity == Vector2.zero)
        {
            animator.SetFloat("speed", 0);
        }
        
        
animator.SetBool("running",true);

        bool flip = direction.x < 0;
        this.transform.rotation = Quaternion.Euler(0, flip ? 180f : 0f, 0f);


    }

    private void FixedUpdate()
    {
        if (direction != Vector2.zero)
        {
            var xdirection = direction.x * speed * Time.deltaTime;
            this.transform.Translate(new Vector3(xdirection, 0), Space.World);
        }
    }
    
}
