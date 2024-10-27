using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
public enum CombatState { START,PLAYERTURN,WON,LOST}
public class CombatSystem : MonoBehaviour
{
    public CombatState State;

    public GameObject playerPrefab;
    public GameObject enemyPrefab;
    public GameObject playerHPBar;

    public Transform playerBattleStation;
    public Transform enemyBattleStation;
    public Transform playerHealthUI;

    unit playerUnit;
    unit enemyUnit;
    // Start is called before the first frame update
    void Start()
    { //on entering turnbased combat.
        State = CombatState.START;
        SetupBattle();
    }

    // Update is called once per frame
  void SetupBattle()
    { //spawning player in alongside useful information
      GameObject playerGO=  Instantiate(playerPrefab,playerBattleStation);
       GameObject EnemyGO= Instantiate(enemyPrefab,enemyBattleStation);
        GameObject playerHP = Instantiate(playerHPBar, playerHealthUI);
        playerUnit=playerGO.GetComponent<unit>();

        enemyUnit = EnemyGO.GetComponent<unit>();


    }
}
