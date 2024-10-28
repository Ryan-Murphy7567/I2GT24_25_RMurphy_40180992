using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
public enum CombatState { START,PLAYERTURN,ENEMYTURN,WON,LOST} // setting up gcombat to be turn based
public class CombatSystem : MonoBehaviour
{
    public CombatState State; //letting the combat state to be altered in inspector

    public GameObject playerPrefab;// player model
    public GameObject enemyPrefab; // enemy model 
    public GameObject playerHPBar; //healthbar currently not dynamic due to Using Unity.UI component bug
    public GameObject PlayerTurnSign;// Unity.UI Workaround asVisual studios bug is preventing me from using it
    public GameObject EnemyTurnSign;// Unity.UI Workaround asVisual studios bug is preventing me from using it

    public Transform playerBattleStation; //player spawn point (useful later when adding multiple  characters)
    public Transform enemyBattleStation; //enemy spawn point 
    public Transform playerHealthUI; // healthbar spawn point Visual studios UI bug is preventing implementation of it scaling 
    public Transform PlayerTurnSignStation;// Unity.UI Workaround asVisual studios bug is preventing me from using it
    public Transform EnemyTurnSignStation;// Unity.UI Workaround asVisual studios bug is preventing me from using it

    unit playerUnit;
    unit enemyUnit;
    // Start is called before the first frame update
    void Start()
    { //on entering turnbased combat.
        State = CombatState.START;
        SetupBattle();
    }

    // Update is called once per frame
  IEnumerator SetupBattle()
    { //spawning player in alongside useful information
      GameObject playerGO=  Instantiate(playerPrefab,playerBattleStation);
       GameObject EnemyGO= Instantiate(enemyPrefab,enemyBattleStation);
        GameObject playerHP = Instantiate(playerHPBar, playerHealthUI);
        GameObject enemysign = Instantiate(EnemyTurnSign, EnemyTurnSignStation);// Unity.UI Workaround asVisual studios bug is preventing me from using it
        playerUnit =playerGO.GetComponent<unit>();

        enemyUnit = EnemyGO.GetComponent<unit>();
        yield return new WaitForSeconds(3f);
        PlayerTurn();
    }
    IEnumerator PlayerAttack() //allow the player to do damage to the enemy
    { bool isDead= enemyUnit.TakeDamage(playerUnit.damage);                                                                                           
        yield return new WaitForSeconds(3f);
        if (isDead) // setting up a win condition for the battle
        {
            State=CombatState.WON;
            EndBattle();
        }
        else
        {
            State= CombatState.ENEMYTURN;
            EnemyTurnSign();
        }
    }

    void PlayerTurn() // letting the player know its their turn
    {
        GameObject playersign= Instantiate(PlayerTurnSign,PlayerTurnSignStation); // Unity.UI Workaround asVisual studios bug is preventing me from using it
       
    }
    public void Attackbuttonpress() //enabling the attack button to be used to take damage will also code animations etc in later build
    {
        if (State != CombatState.PLAYERTURN) return;
        StartCoroutine(PlayerAttack() );
    }
}
