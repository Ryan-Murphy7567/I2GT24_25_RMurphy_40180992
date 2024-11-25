using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class unit : MonoBehaviour
{
    //setting up values for CombatSystem to pull from
    public string unitName;
    public int dmg;
    public int maxHP = 100;
    public int currentHP;
    public int Healamount; 
    public int allhp;
   public Animator animator;
   public Healthbar healthbar;

   void Start()
   {
       healthbar.SetMaxHealth(maxHP);
   }
   
   public bool TakeDamage(int dmg)
    { this.dmg = Random.Range(10,20);
        
        currentHP -= dmg;
      healthbar.SetHealth(currentHP);

      if (currentHP <= 0)
      {
          currentHP = 0;
          return true;
      }
      else //setting up a death condition for the player/enemy
          return false;
    }
    public void Heal(int Healamount) //setting up heal condition for the player or enemies later
    {
        currentHP += Healamount;
        if (currentHP > maxHP)
        {
            currentHP = maxHP;
        }
        healthbar.SetHealth(currentHP);
    }
    public void death(int allhp)
    { 
        currentHP -= allhp;
        healthbar.SetHealth(currentHP);
        
    }
}
