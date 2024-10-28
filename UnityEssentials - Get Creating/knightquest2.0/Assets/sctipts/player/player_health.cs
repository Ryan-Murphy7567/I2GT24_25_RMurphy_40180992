using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class Health : MonoBehaviour
{
    public int currentHealth = 0;
    public int maxHealth = 100;
    public Healthbar healthbar;

    void Start()
    { //health refreshes on combat start
        currentHealth = maxHealth;
    }
    void Update()
    { //creating a  test damage instance and Game over
        if (Input.GetKeyDown(KeyCode.Space))
        {
            DamagePlayer(10);
        }
        if (currentHealth <= 0) { currentHealth = 0; }
        if (currentHealth >= maxHealth) { currentHealth = 100; }
        if (currentHealth <= 0) { death(); }
        
       
    }
    public void DamagePlayer(int damage)
    {
        currentHealth -= damage;
        healthbar.SetHealth(currentHealth);

    }
    public void death() {
    if (currentHealth <= 0) {SceneManager.LoadScene("Gameoverpanel"); }
    
    
    
    
    }
    
  
   
}
