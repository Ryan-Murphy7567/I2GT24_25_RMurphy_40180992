using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera_followsplayer : MonoBehaviour
{ //script to have the camera follow the player in the sidescrolling levels 
    // Start is called before the first frame update
    public Transform player;
    public Vector3 offset;

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(player.position.x + offset.x, player.position.y + offset.y, player.position.z + offset.z); ;
    }
}
