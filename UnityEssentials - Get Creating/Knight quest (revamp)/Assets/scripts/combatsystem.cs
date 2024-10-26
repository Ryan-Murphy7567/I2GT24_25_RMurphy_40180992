using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEditorInternal;
using UnityEngine;
public enum Combatmode {START, PLAYERTURN,ENEMYTURN, WON, LOST}
public class combatsystem : MonoBehaviour
{//spawning in player and enemy 
    public GameObject playerPrefab;
    public GameObject enemyPrefab;
    //SPAWN points for player and enemy
    public Transform playerBattleStation;
    public Transform enemyBattleStation;
        
    
    private Combatmode combatmode;
    private bool hasClicked = true;
    
    // Start is called before the first frame update
    void Start()
        //begin combat
    {  combatmode= Combatmode.START;
        SetupBattle();







    }

    void SetupBattle()
    {

    //spawn in player
      GameObject playerGo= Instantiate(playerPrefab,playerBattleStation);

     //spawn in enemy
        Instantiate(enemyPrefab,enemyBattleStation);
    }

}
