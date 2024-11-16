using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHp : MonoBehaviour
{

public Slider hpSlider;

public void SetHpBar(unit unit)
{
 
    hpSlider.maxValue = unit.maxHP;
    hpSlider.value=unit.currentHP;
        ; }
   };
