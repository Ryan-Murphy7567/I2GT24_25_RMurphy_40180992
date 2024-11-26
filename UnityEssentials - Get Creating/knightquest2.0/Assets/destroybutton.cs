using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroybutton : MonoBehaviour
{ // script to destory gameobject if needed not used currently 
    void Start()
    {
        
    }

    // Update is called once per frame
    void destroyself()
    {
        Destroy(gameObject);
    }
}
