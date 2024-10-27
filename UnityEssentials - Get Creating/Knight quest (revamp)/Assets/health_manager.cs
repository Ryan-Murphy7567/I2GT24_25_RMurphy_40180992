using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Health_manager : MonoBehaviour
{
    public int currenthealth = 100;
    public int maxhealth = 100;
    public int playerHealth;
    public HealthBar healthBar;
    void Start()
    { //making a health pool for our bar to recognise
        currenthealth = maxhealth;
           
    }

    // Update is called once per frame
    void Update()
    {
        playerHealth = currenthealth;
        if (Input.GetKeyDown(KeyCode.Space)) { damageplayer(10); }
        if (currenthealth<0) {currenthealth = 0;}
        if (currenthealth>100) {currenthealth=100;}
    }
    public void damageplayer(int damage)
    {
        currenthealth -= damage;
     healthBar.SetHealth(currenthealth);

    }
}
