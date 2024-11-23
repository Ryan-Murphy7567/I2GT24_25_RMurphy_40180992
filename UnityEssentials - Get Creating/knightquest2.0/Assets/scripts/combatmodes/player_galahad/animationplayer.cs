using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animationplayer : MonoBehaviour
{ public Animator animator;
    public GameObject player;
    
    public void onAttackButtonPress() // PLAYY AN ANIMATION WHEN THE ATTTACK BUTTON IS PRESSED
    {
        animator.Play("player_attack");
    }
    // Update is called once per frame
}
