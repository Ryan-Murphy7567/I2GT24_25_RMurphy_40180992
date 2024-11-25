using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class displaystats : MonoBehaviour
{
    public GameObject player;

    public Text StatText;
    unit playerUnit;
    // Start is called before the first frame update
    void Start()
    {
        playerUnit = player.GetComponent<unit>();
        int value = playerUnit.currentHP;
        StatText.text = playerUnit.unitName + " : " + "current HP : " + value + " / " + playerUnit.maxHP;
    }

    public void Update()
    {
        
    }
    // Update is called once per frame
     public void Statsupdate()
    {
        StatText.text = playerUnit.unitName + " : " + "current HP : " + playerUnit.currentHP + " / " + playerUnit.maxHP;
    }
}
