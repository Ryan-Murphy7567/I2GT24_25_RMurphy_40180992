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
    }

    // Update is called once per frame
    void Update()
    {
        StatText.text = playerUnit.unitName + " : " + "current HP : " + playerUnit.currentHP + " / " + playerUnit.maxHP;
    }
}
