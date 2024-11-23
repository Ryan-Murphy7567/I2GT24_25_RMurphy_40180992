using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class final_healthbar : MonoBehaviour
{   public GameObject unitprefab;
    public Slider slider;
unit unit;


    public void SetMaxHealth(int health)
    { unit = unitprefab.GetComponent<unit>();
        slider.maxValue =unit.maxHP;
        slider.value = unit.currentHP;
    }
    public void SetHealth(int health)
    { 
        slider.value = unit.currentHP ;
    }  
}
