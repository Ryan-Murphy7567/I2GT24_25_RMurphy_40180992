using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.UI;

public class SidescrollPlayerController : MonoBehaviour
{  
    public float speed=20.0f;
    public Animator animator;
    private Vector2 direction;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        
    }

    // Update is called once per frame
     public void Update()
    {
        direction = new Vector2(Input.GetAxis("Horizontal"),0).normalized; //movement HORIZONTAL
        animator.SetFloat("speed",MathF.Abs(direction.magnitude*speed));
        
        
        bool flip = direction.x < 0;
        this.transform.rotation = Quaternion.Euler(0,flip ? 180f : 0f,0f);
        
        
    }

    private void FixedUpdate()
    {
        if (direction != Vector2.zero)
        {
            var xdirection = direction.x*speed*Time.deltaTime;
            this.transform.Translate(new Vector3(xdirection,0),Space.World);
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("wall"))
        {
            speed = 0.0f;
        }
    }
}
