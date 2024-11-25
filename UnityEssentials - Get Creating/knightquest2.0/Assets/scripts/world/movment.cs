using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement : MonoBehaviour
{
    public GameObject player;
    public GameObject target;
    public GameObject home;
    private float moveSpeed= 20.0f;
    public bool ismoving = false;
    Vector3 offset;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public void movetoenemy ()
    {
        
        if (ismoving)
        {
            player.transform.position = Vector3.MoveTowards(transform.position, target.transform.position + offset, moveSpeed * Time.deltaTime);
        }
        else
        {
            player.transform.position = Vector3.MoveTowards(transform.position, home.transform.position, moveSpeed * Time.deltaTime);
        }
    }
}
