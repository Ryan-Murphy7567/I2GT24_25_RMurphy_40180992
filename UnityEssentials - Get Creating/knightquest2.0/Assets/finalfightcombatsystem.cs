using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

//this is a script i made for the final encounter to add more variety and utilise the inventory system in combat. if i am to make any more encounters they would use it as a base starting point 
public enum FinalPartyCombatState
{
    START,
    PLAYERTURN,
    PARTYTURN,
    ENEMYTURN,
    FIRSTSALVO,
    WON,
    LOST
}

public class finalfightcombatsystem : MonoBehaviour
{

    public FinalPartyCombatState Partystate; //letting the combat state to be altered in inspector

    public GameObject playerPrefab; // player model
    public GameObject partymemberPrefab;
    public GameObject enemyPrefab; // enemy model 
    public GameObject SummonPrefab;
    public GameObject Inventory;
    public GameObject Inventorybutton;
    public GameObject attackbutton;
    public GameObject healthbutton;
    public Text dialogueText;
    public float enemychoice = 0f;
    unit playerUnit;
    unit partyUnit;
    unit enemyUnit;
    public Animator playerAnimator;
    public Animator partyAnimator;
    public Animator enemyaAnimator;
    public Animator summonAnimator;
CanvasGroup inventoryButton; 
CanvasGroup FullInventory;
CanvasGroup SummonCanvas;
    public void Start()
    { SummonPrefab.SetActive(false);
        //on entering turnbased combat.
        Partystate = FinalPartyCombatState.START;
        StartCoroutine(SetupBattle());
Inventory.SetActive(true);
    }

    // Update is called once per frame
    IEnumerator SetupBattle()
    {
        //spawning player in alongside useful information


        dialogueText.text = "The  final battle begins";
//gettign components for prefabs 
        playerUnit = playerPrefab.GetComponent<unit>();
        enemyUnit = enemyPrefab.GetComponent<unit>();
        playerAnimator = playerPrefab.GetComponent<Animator>();
        enemyaAnimator = enemyPrefab.GetComponent<Animator>();
        partyUnit = partymemberPrefab.GetComponent<unit>();
        partyAnimator = partymemberPrefab.GetComponent<Animator>();
        SummonCanvas = SummonPrefab.GetComponent<CanvasGroup>();
        summonAnimator = SummonPrefab.GetComponent<Animator>();
        yield return new WaitForSeconds(2);
        dialogueText.text = "ULRICH DEMANDS BLOOD";
        Partystate = FinalPartyCombatState.FIRSTSALVO;
        StartCoroutine(Firstsalvoattack());

    }

    IEnumerator Firstsalvoattack() //scripted encounter to start the final boss fight and to encourage the rune use also breaks the pattern of the player attacking first
    { 
        enemyaAnimator.SetBool("attackplayer", true);
        Inventorybutton.SetActive(true);
        yield return new WaitForSeconds(2);
        dialogueText.text = "Ulrich attacks Lance for 250 damage";
        yield return new WaitForSeconds(2);
        enemyaAnimator.SetBool("attackplayer", false);
        partyAnimator.SetBool("dead", true);
        partyUnit.TakeDamage(99);
        yield return new WaitForSeconds(2);
        dialogueText.text = "Use the Runes to damage Ulrich";
        attackbutton.SetActive(false); //to make the player use atleast one rune to be familliar with the system
        healthbutton.SetActive(false);
        yield return new WaitForSeconds(2);
        UseRunes(); //this system returns to playerturn after use of runes etc to allow the player to whittle down the large health bar without having to have the RNG to attacks, this fight can also be beaten without using the runes for added difficulty
    }

    public void UseRunes()
    {  Partystate = FinalPartyCombatState.PLAYERTURN;
        bool Runeclear = playerUnit.RuneClear(); //check to make sure runes are used
        if (Runeclear)
        {
            dialogueText.text = "All runes have been used. Finish the fight and rescue the princess"; // if so 
            StartCoroutine(PlayerTurn());
        }
        dialogueText.text = "Use the runes to Damage Ulrich";
    }
    IEnumerator Enemyturn() //basic scripting for enemy attack

    {
        enemyaAnimator.SetBool("attackplayer", true);
        yield return new WaitForSeconds(2);

        bool isDead = playerUnit.TakeDamage(enemyUnit.dmg);

        if (isDead)
        {
            enemyaAnimator.SetBool("attackplayer", false);
            Partystate = FinalPartyCombatState.LOST;
            playerAnimator.SetBool("NOHP", true);
            yield return new WaitForSeconds(2);

            StartCoroutine(loadinglost());
        }
        else
        {
            dialogueText.text = enemyUnit.unitName + " hit " + playerUnit.unitName + " for " + enemyUnit.dmg +
                                " damage";
            yield return new WaitForSeconds(2);
            enemyaAnimator.SetBool("attackplayer", false);
            StartCoroutine(PlayerTurn());
            yield return new WaitForSeconds(2);
            Partystate = FinalPartyCombatState.PLAYERTURN;
        }
    }

    IEnumerator Enemyattackparty() //enemy chooses the non player character
    {

        enemyaAnimator.SetBool("attackparty", true);
        bool isDead = partyUnit.TakeDamage(enemyUnit.dmg);
        yield return new WaitForSeconds(2);

        if (isDead)
        {
            enemyaAnimator.SetBool("attackparty", false);
            partyAnimator.SetBool("lancedead", true);
            Partystate = FinalPartyCombatState.LOST;
            StartCoroutine(loadinglost());
        }
        else
        {
            dialogueText.text = enemyUnit.unitName + " hit " + partyUnit.unitName + " for " + enemyUnit.dmg +
                                " damage";
            enemyaAnimator.SetBool("attackparty", false);
            yield return new WaitForSeconds(2);
            StartCoroutine(PlayerTurn());
            yield return new WaitForSeconds(2);
            Partystate = FinalPartyCombatState.PLAYERTURN;
        }
    }

    IEnumerator Enemychoice() //allowing enemy to choose who to attack to add variety to combat
    {
        dialogueText.text = enemyUnit.unitName + "'s Turn";
        enemychoice = Random.Range(1, 10);

        if (enemychoice >= 5)
        {
            dialogueText.text = enemyUnit.unitName + " attacks " + playerUnit.unitName;
            yield return new WaitForSeconds(4);
            StartCoroutine(Enemyturn());
        }
        else
        {
            dialogueText.text = enemyUnit.unitName + " attacks " + partyUnit.unitName;
            yield return new WaitForSeconds(4);
            StartCoroutine(Enemyattackparty());
        }
    }

    IEnumerator PlayerAttack() //allow the player to do damage to the enemy
    {
        bool isDead = enemyUnit.TakeDamage(playerUnit.dmg);
        playerAnimator.SetBool("ATTACK", true);

        yield return new WaitForSeconds(2);
        if (isDead) // setting up a win condition for the battle
        {
            enemyaAnimator.SetBool("dead", true);
            Partystate = FinalPartyCombatState.WON;
            StartCoroutine(loadingwin());
        }
        else
        {

            Partystate = FinalPartyCombatState.PARTYTURN;
            playerAnimator.SetBool("ATTACK", false);
            yield return new WaitForSeconds(2);
            if (enemyUnit.currentHP == enemyUnit.maxHP)
            {
                dialogueText.text = playerUnit.unitName + " drew first blood"; //adding unique text to vary battle 
            }
            else
            {
                dialogueText.text = playerUnit.unitName + " hit " + enemyUnit.unitName + " for " + playerUnit.dmg +
                                    " damage"; 
            }

            ;
            yield return new WaitForSeconds(2);
            StartCoroutine(PartyTurn());


        }
    }

    IEnumerator Heal()
    {
        // allow the player to heal,

        playerUnit.Heal(10);
        dialogueText.text = playerUnit.unitName + " was healed for " + playerUnit.Healamount + " HP";
        Partystate = FinalPartyCombatState.PARTYTURN;
        yield return new WaitForSeconds(2);
        StartCoroutine(PartyTurn());
    }

    IEnumerator death() //allow player to die quick for an easy test of fail state
    {

        playerUnit.death(100);
        dialogueText.text = playerUnit.unitName + "has fallen in battle";
        yield return new WaitForSeconds(2);
        playerAnimator.SetBool("NOHP", true);
        yield return new WaitForSeconds(2);
        StartCoroutine(loadinglost());
    }

    public IEnumerator PartyTurn() //party members turn
    {
        dialogueText.text = partyUnit.unitName + "'s Turn";
        yield return new WaitForSeconds(2);
        partyAnimator.SetBool("attackplayer", true);
        bool isDead = enemyUnit.TakeDamage(partyUnit.dmg);

        if (isDead) //enemy death check
        {
            dialogueText.text = partyUnit.unitName + " hit " + enemyUnit.unitName + " for " + partyUnit.dmg +
                                " damage";
            partyAnimator.SetBool("attackplayer", false);
            yield return new WaitForSeconds(2);
            enemyaAnimator.SetBool("dead", true);
            yield return new WaitForSeconds(2);
            Partystate = FinalPartyCombatState.WON;
            StartCoroutine(loadingwin());
        }
        else
        {
            Partystate = FinalPartyCombatState.ENEMYTURN;
            dialogueText.text = partyUnit.unitName + " hit " + enemyUnit.unitName + " for " + partyUnit.dmg + //inidcating damage done and to who
                                " damage";
            yield return new WaitForSeconds(2);
            partyAnimator.SetBool("attackplayer", false);
            StartCoroutine(Enemychoice());


        }
    }






    IEnumerator PlayerTurn() // letting the player know its their turn
    { attackbutton.SetActive(true);
        healthbutton.SetActive(true);
        dialogueText.text = playerUnit.unitName + "'s turn";
        yield return new WaitForSeconds(1);
        Partystate = FinalPartyCombatState.PLAYERTURN;
    }

    public void
        Attackbuttonpress() //enabling the attack button to be used to take damage will also code animations etc in later build
    {
        if (Partystate != FinalPartyCombatState.PLAYERTURN) return;

        dialogueText.text = playerUnit.unitName + " Attacks";
        StartCoroutine(PlayerAttack());
    }

    public void
        Healbuttonpress() //enabling the healbutton to be used to recover hp messages to be added once display.text function is fixed
    {
        if (Partystate != FinalPartyCombatState.PLAYERTURN) return;

        StartCoroutine(Heal());
    }

    public void Diebuttonpress() //allow for button press to test fail state
    {
        if (Partystate != FinalPartyCombatState.PLAYERTURN) return;


        StartCoroutine(death());
    }

    public void Slot1buttonpress() //inventory slot button for mechanic in final fight
    {
        if (Partystate != FinalPartyCombatState.PLAYERTURN) return;
        
            StartCoroutine(UlrichWeak());
    }

    public IEnumerator UlrichWeak() //player power up to make last fight more even by reducing health
    {  
            dialogueText.text = enemyUnit.unitName + " is staggered"; //stunning the boss and inflicting a lot of damage to make the healthbar more even. and giving player a bit of a power fantasy
        enemyaAnimator.SetBool("dead", true);
        yield return new WaitForSeconds(3);
        enemyUnit.TakeDamage(250);
        yield return new WaitForSeconds(2);
        enemyaAnimator.SetBool("dead", false);
        yield return new WaitForSeconds(2);
        dialogueText.text = "The Rune's Power has faded";
        yield return new WaitForSeconds(2);
        UseRunes();
    }

    public void Slot2buttonpress() //inventory Slot button for mechanic in final fight
    {
        if (Partystate != FinalPartyCombatState.PLAYERTURN) return;
        StartCoroutine(SummonSamurai());
    }

    public IEnumerator SummonSamurai() // inventory item to summon another ally temporarily
    {
        {
            bool issamurai = playerUnit.SamuraiSummonscount(); //bool to limit uses of the summon incase i want to use more in future.
            if (issamurai)
            {
                dialogueText.text = "The Spirit rests as the runes power has faded"; //letting player know there are no more uses
                yield return new WaitForSeconds(2);
                UseRunes();
            }

            else //actual summon loop and way to hide it on other turns
            {
                dialogueText.text = "An Old enemy becomes an ally";
                yield return new WaitForSeconds(2);
                SummonPrefab.SetActive(true); //enabling summon
                yield return new WaitForSeconds(2);
                summonAnimator.SetBool("attackplayer", true); //summon attack using same activation as when it was an enemy but sprite is flipped
                enemyUnit.TakeDamage(50);
                yield return new WaitForSeconds(2);
                summonAnimator.SetBool("attackplayer", false); //endeing animation
                yield return new WaitForSeconds(2);
                dialogueText.text= "The Spirit rests for now"; //indicating the summon is disappearing 
                summonAnimator.SetBool("dead", true); //summon disappearing 
                yield return new WaitForSeconds(2);
                SummonPrefab.SetActive(false);
                summonAnimator.SetBool("dead", false); //summon returning to idle for next summon even if invisible
                
                UseRunes();

            }
        }
       
    }
    public void Slot3buttonpress() //inventory slot button
    {
        if (Partystate != FinalPartyCombatState.PLAYERTURN) return;
        
        StartCoroutine(revivelance());
    }

    public IEnumerator revivelance() //one of the rune abilities.
    {
        
            partyUnit.Heal(90); //restoring health also
            partyAnimator.SetBool("dead", false);
            dialogueText.text = partyUnit.unitName + " is restored by the Rune's Power which then fades"; //indicating rune is gone
            yield return new WaitForSeconds(2);
            dialogueText.text = partyUnit.unitName + " seeks revenge"; //indicating party member interrupts the flow and give character to fight
            partyAnimator.SetBool("attackplayer", true); // party member animation start 
            enemyUnit.TakeDamage(100); // another hit to boss hp to make it feel like a big fight
            yield return new WaitForSeconds(2);
            partyAnimator.SetBool("attackplayer", false); //end party member animation
            dialogueText.text = partyUnit.unitName + " hit "+enemyUnit.unitName +" for 100 damage";
            yield return new WaitForSeconds(2);
            UseRunes();
        
      
    }

    IEnumerator loadingwin() // load the next scene and will display a message
    {
        //code display victory message
        dialogueText.text = "Victory achieved";
        yield return new WaitForSeconds(2);
        if (enemyUnit.currentHP <= 0) SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);// loading next scene in order 
    }

    public IEnumerator loadinglost() //load the game fail state
    {
        dialogueText.text = "You lost " + enemyUnit.unitName + " laughs at your attempt";
        playerAnimator.SetBool("NOHP", true);
        partyAnimator.SetBool("dead", true);
        yield return new WaitForSeconds(4);
        SceneManager.LoadScene("gameoverpanel");

    }
}
