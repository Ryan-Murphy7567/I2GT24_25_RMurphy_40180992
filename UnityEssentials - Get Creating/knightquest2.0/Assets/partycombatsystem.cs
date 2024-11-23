using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public enum PartyCombatState { START,PLAYERTURN,PARTYTURN,ENEMYTURN,WON,LOST}
public class partycombatsystem : MonoBehaviour
{
       public  PartyCombatState Partystate; //letting the combat state to be altered in inspector
  public Animator animator;
    public GameObject playerPrefab;// player model
    public GameObject partymemberPrefab;
    public GameObject enemyPrefab; // enemy model 
    public Transform playerBattleStation; //player spawn point (useful later when adding multiple  characters)
    public Transform enemyBattleStation; //enemy spawn point 
    public Transform partyBattleStation;
    public Text dialogueText;
    public float enemychoice = 0f; 
    unit playerUnit;
    unit partyUnit;
    unit enemyUnit;
    
   public void Start()
    {;
        //on entering turnbased combat.
        Partystate = PartyCombatState.START;
        StartCoroutine(SetupBattle());  
                        
    }

    // Update is called once per frame
  IEnumerator SetupBattle()
    { //spawning player in alongside useful information
      GameObject playerGO=  Instantiate(playerPrefab,playerBattleStation);
       GameObject EnemyGO= Instantiate(enemyPrefab,enemyBattleStation);
       GameObject PartyGO= Instantiate(partymemberPrefab,partyBattleStation);
     dialogueText.text = "The battle begins";


        //signs to indicate turn
        //signs to indicate turn
        // Unity.UI Workaround asVisual studios bug is preventing me from using it.
        playerUnit =playerGO.GetComponent<unit>();
partyUnit = PartyGO.GetComponent<unit>();
        enemyUnit = EnemyGO.GetComponent<unit>();
        //display text indicating player turn
        yield return new WaitForSeconds(2);
        dialogueText.text = "Galahad's turn";
        Partystate = PartyCombatState.PLAYERTURN;

    }
    // ReSharper disable Unity.PerformanceAnalysis
    
    // ReSharper disable Unity.PerformanceAnalysis


     IEnumerator Enemyturn() //basic scripting for enemy attack

    {
        dialogueText.text = enemyUnit.unitName + "'s Turn";
        bool isDead = playerUnit.TakeDamage(enemyUnit.dmg);

        if (isDead)
        {
            Partystate = PartyCombatState.LOST;
            
            StartCoroutine(loadinglost());
        }
        else
        {
            StartCoroutine(PlayerTurn());
            yield return new WaitForSeconds(2);
            Partystate = PartyCombatState.PLAYERTURN;
        }
    }

    IEnumerator Enemyattackparty()
        {
            dialogueText.text = enemyUnit.unitName+"'s Turn";
            bool isDead = partyUnit.TakeDamage(enemyUnit.dmg);

            if (isDead)
            {
                Partystate = PartyCombatState.LOST;
                StartCoroutine(loadinglost());
            }
            else
            {
                StartCoroutine(PlayerTurn());
                yield return new WaitForSeconds(2);
                Partystate= PartyCombatState.PLAYERTURN;
            }
        }
    IEnumerator Enemychoice()
    {
        enemychoice = Random.Range(1, 10);
        if (enemychoice >= 5)
        {
            dialogueText.text = enemyUnit.unitName + " attacks " + playerUnit.unitName;
            yield return new WaitForSeconds(2);
            StartCoroutine(Enemyturn());
        }
        else
        {
            dialogueText.text = enemyUnit.unitName + " attacks " + partyUnit.unitName;
            yield return new WaitForSeconds(2);
            StartCoroutine(Enemyattackparty());
        }
    }

    IEnumerator PlayerAttack() //allow the player to do damage to the enemy
            {
                bool isDead= enemyUnit.TakeDamage(playerUnit.dmg);
               
                yield return new WaitForSeconds(2);
                if (isDead) // setting up a win condition for the battle
                { 
                    Partystate=PartyCombatState.WON;
                    StartCoroutine(loadingwin());
                }
                else
                { animator.SetBool("ATTACKBUTTONPRESS",true);

                    Partystate=PartyCombatState.PARTYTURN;
                    StartCoroutine(PartyTurn() );
                    

                }
            }

             IEnumerator Heal() { // allow the player to heal,

                playerUnit.Heal(10);
                dialogueText.text = playerUnit.unitName + " was healed";
                Partystate = PartyCombatState.PARTYTURN;
                yield return new WaitForSeconds(2);
                StartCoroutine(PartyTurn());
            }
             IEnumerator death() //allow player to die quick for an easy test of fail state
            {

                playerUnit.death(100);
                dialogueText.text = playerUnit.unitName + "has fallen in battle";
                animator.SetBool("NOHP",true);
                yield return new WaitForSeconds(2);
                StartCoroutine(loadinglost());
            }
        
        IEnumerator PartyTurn()
        {
            dialogueText.text=partyUnit.unitName + "'s Turn";
            bool isDead = enemyUnit.TakeDamage(partyUnit.dmg);
            if (isDead)
            {   
                Partystate = PartyCombatState.WON;
                    StartCoroutine(loadingwin());
            }
            else
            {   Partystate = PartyCombatState.ENEMYTURN;
                yield return new WaitForSeconds(2);
                StartCoroutine(Enemychoice());
                
              
            }
        }
         
       
     
        
    

    IEnumerator PlayerTurn() // letting the player know its their turn
    { 
       
        dialogueText.text = playerUnit.unitName + "'s turn"; 
        yield return new WaitForSeconds(2);
        Partystate = PartyCombatState.PLAYERTURN;
    }
    public void Attackbuttonpress() //enabling the attack button to be used to take damage will also code animations etc in later build
    { 
        if (Partystate != PartyCombatState.PLAYERTURN) return;
       dialogueText.text = partyUnit.unitName + "'s Turn"; 
       
        StartCoroutine(PlayerAttack()) ;
    }
    public void Healbuttonpress() //enabling the healbutton to be used to recover hp messages to be added once display.text function is fixed
    {
        if (Partystate != PartyCombatState.PLAYERTURN) return;
      
        StartCoroutine(Heal());
    }
    public void Diebuttonpress() //allow for button press to test fail state
    {
        if (Partystate != PartyCombatState.PLAYERTURN) return;
  
      
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

   

    
    



