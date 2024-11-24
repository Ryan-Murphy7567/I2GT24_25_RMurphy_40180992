using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using TMPro;

public class lance_collision : MonoBehaviour
{ //this script is used as a dialogue trigger and will be repurposed for future cutscenes. 
    public dialoguewindow dialogue;
    public SidescrollPlayerController player;
    public string DialogueText;
public Animator animator;
    public Behaviour Script;
    // Start is called before the first frame update
    public void   OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {player.animator.SetBool("running", false);
            dialogue.show(DialogueText);
            Script.enabled = false;

        }
       
    }

    // Update is called once per frame
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            dialogue.close();
            Script.enabled = true;
        }
       
    }
}
