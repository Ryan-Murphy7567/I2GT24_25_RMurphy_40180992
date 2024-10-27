using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Healthbar : MonoBehaviour
{
    public Slider slider;
    public Health health;
    public GameObject healthbar;
    public void Start()
    {
     
        GameObject go = Instantiate<GameObject>(healthbar);
        Slider slider = go.GetComponent<Slider>();
    }
    public void SetMaxHealth(int health)
    {
        slider.value = health;
    }
    public void SetHealth(int health) { slider.value = health;

      
     
    }


}
