using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
public class unit : MonoBehaviour
{
    //setting up values for CombatSystem to pull from
    public string unitName;
    public int dmg;
    public int maxHP;
    public int currentHP;
    public int Healamount; 
    public int allhp;
   public Animator animator;
    public bool TakeDamage(int dmg) {
        currentHP -= dmg;
      

        if (currentHP <= 0) return true;
        else//setting up a death condition for the player/enemy
            return false;
    }
    public void Heal(int Healamount) //setting up heal condition for the player or enemies later
    {
        currentHP += Healamount;
        if (currentHP > maxHP)
        {
            currentHP = maxHP;
        }
    }
    public void death(int allhp)
    { 
        currentHP -= allhp;
        
        
    }
}
