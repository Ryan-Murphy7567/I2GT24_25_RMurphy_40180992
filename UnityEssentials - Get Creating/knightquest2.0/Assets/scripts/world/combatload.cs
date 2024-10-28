using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.SceneManagement;

public class combatload : MonoBehaviour
{
    // Start is called before the first frame update
    void OnTriggerEnter(Collider other) 
    {
        SceneManager.LoadScene("Goblinfight");
        Debug.Log("leda");
    }

    // Update is called once per frame
    private void Update()
    {
        
    }

}
