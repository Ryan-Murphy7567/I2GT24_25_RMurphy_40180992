
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.Search;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.SceneManagement;

using UnityEngine.UI; 
public enum CombatState { START,PLAYERTURN,ENEMYTURN,WON,LOST} // setting up gcombat to be turn based
public class CombatSystem : MonoBehaviour
{ //this script is used for instances of 1v1 combat such as the prologue and first fight before partry members are added. 
    public CombatState State; //letting the combat state to be altered in inspector
  
    public GameObject playerPrefab;// player model
    public GameObject enemyPrefab; // enemy model 
     //player spawn point (useful later when adding multiple  characters)
     //enemy spawn point 
    public Text dialogueText;
    unit playerUnit; // to read 
    unit enemyUnit;
    movement playerMovement;
    public Animator enemyaAnimator; // animator for enemy
    public Animator playerAnimator; //animator for player
    
    void Start()
    {
        //on entering turnbased combat.
        State = CombatState.START;
        StartCoroutine(SetupBattle());  
                        
    }

    // Update is called once per frame
  IEnumerator SetupBattle()
    { //spawning player in alongside useful information
  
      
     dialogueText.text = "The battle begins";


       
        
        playerUnit =playerPrefab.GetComponent<unit>();
        enemyUnit = enemyPrefab.GetComponent<unit>();
        playerMovement = playerPrefab.GetComponent<movement>();
        playerAnimator = playerPrefab.GetComponent<Animator>();
        enemyaAnimator = enemyPrefab.GetComponent<Animator>();
            
        //display text indicating player turn
        yield return new WaitForSeconds(2);
        dialogueText.text = "Galahad's turn";
        State = CombatState.PLAYERTURN;
        
        

    }
    // ReSharper disable Unity.PerformanceAnalysis
    IEnumerator PlayerAttack() //allow the player to do damage to the enemy
    {
        bool isDead= enemyUnit.TakeDamage(playerUnit.dmg);
      
        playerAnimator.SetBool("ATTACK", true);
        yield return new WaitForSeconds(2);
        playerMovement.ismoving = false;
        if (isDead) // setting up a win condition for the battle
        {
            enemyaAnimator.SetBool("dead", true);
            State=CombatState.WON;
            StartCoroutine(loadingwin());
        }
        else
        {

            State=CombatState.ENEMYTURN;
            playerAnimator.SetBool("ATTACK", false);
            dialogueText.text = playerUnit.unitName + " hit " + enemyUnit.unitName + " for " + playerUnit.dmg +
                                " damage ";
            yield return new WaitForSeconds(3);
            StartCoroutine(Enemyturn() );
            

        }
    }
    public IEnumerator Heal() { // allow the player to heal,

        playerUnit.Heal(10);
        dialogueText.text = playerUnit.unitName + " was healed for " + playerUnit.Healamount+ " HP";
        State = CombatState.ENEMYTURN;
        yield return new WaitForSeconds(2);
        StartCoroutine(Enemyturn());
    }
   public IEnumerator death() //allow player to die quick for an easy test of fail state
    {

        playerUnit.death(100);
        dialogueText.text = playerUnit.unitName + "has fallen in battle";
        yield return new WaitForSeconds(2);
         StartCoroutine(loadinglost());
    }
    // ReSharper disable Unity.PerformanceAnalysis
    IEnumerator Enemyturn() //basic scripting for enemy attack
    {
        
        dialogueText.text= enemyUnit.unitName+"'s Turn";
        yield return new WaitForSeconds(2);
        yield return new WaitForSeconds(2);
        enemyaAnimator.SetBool("attackplayer", true);
        yield return new WaitForSeconds(2);
        bool isDead = playerUnit.TakeDamage(enemyUnit.dmg);

        if (isDead)
        { enemyaAnimator.SetBool("attackplayer",false);
            State = CombatState.LOST;
            playerAnimator.SetBool("NOHP", true);
            yield return new WaitForSeconds(2);
            StartCoroutine(loadinglost());
        }
        else
        { dialogueText.text = enemyUnit.unitName + " hit " + playerUnit.unitName + " for " + enemyUnit.dmg + " damage";
            enemyaAnimator.SetBool("attackplayer", false);
            yield return new WaitForSeconds(2);
            yield return new WaitForSeconds(2);
            StartCoroutine(PlayerTurn());
            yield return new WaitForSeconds(2);
            State = CombatState.PLAYERTURN;
        }
        
    }

    IEnumerator PlayerTurn() // letting the player know its their turn
    { 
       
        dialogueText.text = playerUnit.unitName + "'s turn"; 
        yield return new WaitForSeconds(2);
        State = CombatState.PLAYERTURN;
    }
    public void Attackbuttonpress() //enabling the attack button to be used to take damage will also code animations etc in later build
    { 
        if (State != CombatState.PLAYERTURN) return;
      
        StartCoroutine(PlayerAttack()) ;
    }
    public void Healbuttonpress() //enabling the healbutton to be used to recover hp messages to be added once display.text function is fixed
    {
        if (State != CombatState.PLAYERTURN) return;
      
        StartCoroutine(Heal());
    }
    public void Diebuttonpress() //allow for button press to test fail state
    {
        if (State != CombatState.PLAYERTURN) return;
  
      
        StartCoroutine(death());
    }
    IEnumerator loadingwin() // load the next scene and will display a message
        { //code display victory message
            dialogueText.text = "Victory achieved";
            yield return new WaitForSeconds(2);
          if(enemyUnit.currentHP<=0) SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
            
    IEnumerator loadinglost() //load the game fail state
    {
        dialogueText.text = "You lost " + enemyUnit.unitName + " laughs at your attempt";
        playerAnimator.SetBool("NOHP", true);
        yield return new WaitForSeconds(2);
        if(playerUnit.currentHP <= 0) SceneManager.LoadScene("gameoverpanel");

    }

    
    
    
}
