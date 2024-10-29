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
    public int amount;
    public int allhp;
    public bool TakeDamage(int dmg) {
        currentHP -= dmg;

        if (currentHP <= 0) return true; //setting up a death condition for the player/enemy
        else
            return false;
    }
    public void Heal(int amount)
    {
        currentHP += amount;
        if (currentHP > maxHP)
        {
            currentHP = maxHP;
        }
    }
    public void death(int allhp)
    {
        currentHP -= allhp;
        if (currentHP <= 0)
        {
            SceneManager.LoadScene("Gameoverpanel");
        }
    }
}
