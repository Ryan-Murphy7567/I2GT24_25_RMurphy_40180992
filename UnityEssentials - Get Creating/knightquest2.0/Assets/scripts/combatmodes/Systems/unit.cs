using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class unit : MonoBehaviour
{
    //setting up values for CombatSystems to pull from
    public string unitName;
    public int dmg;
    public int maxHP = 100;
    public int currentHP;
    public int Healamount; 
    public int allhp;
    public int samuraiSummons;
   public Animator animator;
   public Healthbar healthbar;
   public int runecount;
   public int critchance;
   void Start()
   {
       healthbar.SetMaxHealth(maxHP);
   }
   
   public bool TakeDamage(int dmg)
   { 
        if (runecount > 0) //giving players more power after using runes
        {
            this.dmg = Random.Range(20, 30); //creating dynamic damage to make fights more unpredictable but not pure luck
            
        }
        else
        {
            this.dmg = Random.Range(10,20);

        }
        
        this.dmg = Random.Range(10,20);
        
        currentHP -= dmg;
      healthbar.SetHealth(currentHP); //adjusting sliders

      if (currentHP <= 0)
      {
          currentHP = 0;
          return true;
      }
      else //setting up a death condition for the player/enemy
          return false;
    }
 public bool criticalHit(){ //creating a crithit chance 
    critchance = Random.Range(1, 100);
    if (critchance > 80) return true;
    else return false;
}
    public bool NoDamage(int noDmg)
    {
        currentHP -= noDmg;
        return true;
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

    public bool RuneClear() //bool to destroy runes on use
    {
        
        if (runecount >= 3)
        {
            return true;
        }
        else
        {
            runecount += 1;
            return false;
        }
    }
    public bool SamuraiSummonscount()
    {
        samuraiSummons -= 1;
        if (samuraiSummons <= 0)
        {
            samuraiSummons = 0;
            return true;
        }
        return false;
    }
}
