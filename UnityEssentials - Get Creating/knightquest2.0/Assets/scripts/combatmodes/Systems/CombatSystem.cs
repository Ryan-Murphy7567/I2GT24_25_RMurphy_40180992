
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.Search;
using UnityEngine;
using UnityEngine.SceneManagement;

using UnityEngine.UI; 
public enum CombatState { START,PLAYERTURN,ENEMYTURN,WON,LOST} // setting up gcombat to be turn based
public class CombatSystem : MonoBehaviour
{
    public CombatState State; //letting the combat state to be altered in inspector
  
    public GameObject playerPrefab;// player model
    public GameObject enemyPrefab; // enemy model 
    public Transform playerBattleStation; //player spawn point (useful later when adding multiple  characters)
    public Transform enemyBattleStation; //enemy spawn point 
  
    public Text dialogueText;

    unit playerUnit;
    unit enemyUnit;
    // Start is called before the first frame update
    void Start()
    {;
        //on entering turnbased combat.
        State = CombatState.START;
        StartCoroutine(SetupBattle());  
                        
    }

    // Update is called once per frame
  IEnumerator SetupBattle()
    { //spawning player in alongside useful information
      GameObject playerGO=  Instantiate(playerPrefab,playerBattleStation);
       GameObject EnemyGO= Instantiate(enemyPrefab,enemyBattleStation);
     dialogueText.text = "The battle begins";


        //signs to indicate turn
        //signs to indicate turn
        // Unity.UI Workaround asVisual studios bug is preventing me from using it.
        playerUnit =playerGO.GetComponent<unit>();

        enemyUnit = EnemyGO.GetComponent<unit>();
        //display text indicating player turn
        yield return new WaitForSeconds(2);
        dialogueText.text = "Galahad's turn";
        State = CombatState.PLAYERTURN;
        
        

    }
    // ReSharper disable Unity.PerformanceAnalysis
    IEnumerator PlayerAttack() //allow the player to do damage to the enemy
    {
        bool isDead= enemyUnit.TakeDamage(playerUnit.dmg)   ;                                                                                           
        yield return new WaitForSeconds(2);
        if (isDead) // setting up a win condition for the battle
        {
            State=CombatState.WON;
            StartCoroutine(loadingwin());
        }
        else
        {

            State=CombatState.ENEMYTURN;
            StartCoroutine(Enemyturn() );
            ;

        }
    }
    public IEnumerator Heal() { // allow the player to heal,

        playerUnit.Heal(10);
        dialogueText.text = playerUnit.unitName + " was healed";
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
        bool isDead = playerUnit.TakeDamage(enemyUnit.dmg);

        if (isDead)
        {
            State = CombatState.LOST;
            StartCoroutine(loadinglost());
        }
        else
        {
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
       dialogueText.text = enemyUnit.unitName + "'s Turn"; 
    
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
          if(enemyUnit.currentHP<=0) SceneManager.LoadScene("postfight");
        }
            
    IEnumerator loadinglost() //load the game fail state
    {// code in display text for losing 
        yield return new WaitForSeconds(2);
        if(playerUnit.currentHP <= 0) SceneManager.LoadScene("gameoverpanel");

    }

   

    
    
}
