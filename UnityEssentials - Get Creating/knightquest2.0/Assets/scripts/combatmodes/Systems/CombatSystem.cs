using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
public enum CombatState { START,PLAYERTURN,ENEMYTURN,WON,LOST} // setting up gcombat to be turn based
public class CombatSystem : MonoBehaviour
{
    public CombatState State; //letting the combat state to be altered in inspector
  
    public GameObject playerPrefab;// player model
    public GameObject enemyPrefab; // enemy model 
    public GameObject PlayerTurnSign;// Unity.UI Workaround asVisual studios bug is preventing me from using it
    public GameObject EnemyTurnSign;// Unity.UI Workaround asVisual studios bug is preventing me from using it
    public GameObject PlayerDamageSign;//Unity.UI Workaround asVisual studios bug is preventing me from using it
    public GameObject EnemyDamageSign;// Unity.UI Workaround asVisual studios bug is preventing me from using it
    public GameObject PlayerHealSign;//Unity.UI Workaround asVisual studios bug is preventing me from using it
    public Transform playerBattleStation; //player spawn point (useful later when adding multiple  characters)
    public Transform enemyBattleStation; //enemy spawn point 
    public Transform playerHealthUI; // healthbar spawn point Visual studios UI bug is preventing implementation of it scaling 
    public Transform PlayerTurnSignStation;// Unity.UI Workaround asVisual studios bug is preventing me from using it
    public Transform EnemyTurnSignStation;// Unity.UI Workaround asVisual studios bug is preventing me from using it
    public Transform PlayerDamageSignStation;// Unity.UI Workaround asVisual studios bug is preventing me from using it
    public Transform EnemyDamageSignStation;// Unity.UI Workaround asVisual studios bug is preventing me from using it

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
     


        //signs to indicate turn
        //signs to indicate turn
        // Unity.UI Workaround asVisual studios bug is preventing me from using it.
        playerUnit =playerGO.GetComponent<unit>();

        enemyUnit = EnemyGO.GetComponent<unit>();
        //display text indicating player turn
        yield return new WaitForSeconds(2);
        State = CombatState.PLAYERTURN;
        GameObject clone = Instantiate(PlayerTurnSign, PlayerTurnSignStation);
        Destroy(clone, 2.0f);

    }
   IEnumerator PlayerAttack() //allow the player to do damage to the enemy
    {

        bool isDead= enemyUnit.TakeDamage(playerUnit.dmg);                                                                                           
        
        if (isDead) // setting up a win condition for the battle
        {
            State=CombatState.WON;
            StartCoroutine(loadingwin());
        }
        else
        {
            State= CombatState.ENEMYTURN;
            GameObject clone2 = Instantiate(EnemyTurnSign, EnemyTurnSignStation);
            GameObject clone3 = Instantiate(PlayerDamageSign, PlayerDamageSignStation);
            yield return new WaitForSeconds(2);
            Destroy(clone2);
            Destroy(clone3);
            StartCoroutine(Enemyturn() );
            ;

        }
    }
    public IEnumerator Heal() { // allow the player to heal,

        playerUnit.Heal(10);
        State = CombatState.ENEMYTURN;
        GameObject clone2 = Instantiate(EnemyTurnSign, EnemyTurnSignStation);
        GameObject clone5= Instantiate(PlayerHealSign,PlayerDamageSignStation);
        yield return new WaitForSeconds(2);
        Destroy(clone2);
        Destroy (clone5);
        StartCoroutine(Enemyturn());
    }
   public IEnumerator death() //allow player to die quick for an easy test of fail state
    {

        playerUnit.death(100);
        State = CombatState.ENEMYTURN;
        yield return new WaitForSeconds(2);
         StartCoroutine(loadinglost());
    }
    IEnumerator Enemyturn() //basic scripting for enemy attack
    { //code dialogue here
        
        bool isDead= playerUnit.TakeDamage(enemyUnit.dmg);

        if (isDead)
        {
            State = CombatState.LOST;
            StartCoroutine(loadinglost());
        }
        else
        yield return new WaitForSeconds(2);
        { State = CombatState.PLAYERTURN; }
        
        GameObject clone = Instantiate(PlayerTurnSign, PlayerTurnSignStation);
        GameObject clone4= Instantiate(EnemyDamageSign,PlayerDamageSignStation);
        Destroy(clone, 2.0f);
        Destroy(clone4,1.0f);
        
    }
    void PlayerTurn() // letting the player know its their turn
    {
       
        
    }
    public void Attackbuttonpress() //enabling the attack button to be used to take damage will also code animations etc in later build
    { 
        if (State != CombatState.PLAYERTURN) return;
        GameObject clone2 = Instantiate(EnemyTurnSign, EnemyTurnSignStation);
        Destroy(clone2, 1.0f);
        StartCoroutine(PlayerAttack()) ;
    }
    public void Healbuttonpress() //enabling the healbutton to be used to recover hp messages to be added once display.text function is fixed
    {
        if (State != CombatState.PLAYERTURN) return;
        GameObject clone2 = Instantiate(EnemyTurnSign, EnemyTurnSignStation);
        Destroy(clone2, 1.0f);
        StartCoroutine(Heal());
    }
    public void Diebuttonpress() //allow for button press to test fail state
    {
        if (State != CombatState.PLAYERTURN) return;
        GameObject clone2 = Instantiate(EnemyTurnSign, EnemyTurnSignStation);
        Destroy(clone2, 1.0f);
        StartCoroutine(death());
    }
    IEnumerator loadingwin() // load the next scene and will display a message
        { //code display victory message
            
            yield return new WaitForSeconds(2);
          if(enemyUnit.currentHP<=0) SceneManager.LoadScene("postfight");
        }
            
    IEnumerator loadinglost() //load the game fail state
    {// code in display text for losing 
        yield return new WaitForSeconds(2);
        if(playerUnit.currentHP <= 0) SceneManager.LoadScene("gameoverpanel");

    }
  
    
}
