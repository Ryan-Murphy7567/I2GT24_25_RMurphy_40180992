using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class final_healthbar : MonoBehaviour
{  //basic healthbar script this is the one used
    public Slider slider;



    public void SetMaxHealth(int health)
    {
        slider.maxValue =health;
        slider.value = health;
    }
    public void SetHealth(int health)
    {
        slider.value = health; ;
    }  
}
