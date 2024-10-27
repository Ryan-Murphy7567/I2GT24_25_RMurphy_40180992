using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
public class HealthBar : MonoBehaviour
{
    public Slider Healthbar;
    public Health_manager playerHealth;
    public HealthBar healthBar;
    private void Start()
    {
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<Health_manager>();
        Healthbar = GetComponent<Slider>();
       Healthbar.value = playerHealth.currenthealth;
       Healthbar.highValue = playerHealth.maxhealth;
    }
    public void SetHealth(int hp)
    {
        Healthbar.value = hp;

        
    }
}