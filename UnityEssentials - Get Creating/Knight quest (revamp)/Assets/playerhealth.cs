using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build.Reporting;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.UIElements;

public class playerhealth : MonoBehaviour
{// simplified health
    public int health = 0;
    public int maxHealth = 100;
    public float speed = 10f;
    private Vector2 target;
    private Vector2 position;
    private bool movementcyle=false;
    void Start()
    {
        target = new Vector2(12f, 0f);
        position = gameObject.transform.position;
        health = maxHealth;
        movementcyle = false;
    }

    // Update is called once per frame
    void Update()
    { //HEALTH TEST FOR DAMAGE AND HEALTH BAR


        if (Input.GetKeyDown(KeyCode.Space)) { damageplayer(10); }
        if (health < 0) { health = 0; }
        if (Input.GetKeyDown(KeyCode.L)) { transform.Translate(Vector2.right * Time.deltaTime * speed); }
    }
    public void damageplayer(int damage)
    {
        health -= damage;


    }

   public void movementcycle() {
        if (movementcyle == true) { transform.Translate(Vector3.forward * Time.deltaTime * speed); }

    }

}

