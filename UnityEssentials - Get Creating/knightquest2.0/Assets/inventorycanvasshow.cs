using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class inventorycanvasshow : MonoBehaviour
{
    public CanvasGroup InventoryCanvasGroup;
// another inventory script.
    GameObject inventorycanvas;

    // Start is called before the first frame update
    void Start()
    {
        InventoryCanvasGroup = inventorycanvas.GetComponent<CanvasGroup>();
        InventoryCanvasGroup.alpha = 0;
    }

    // Update is called once per frame
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            showcanvas();
DestroySelf();
        }

        void showcanvas()
        {
            InventoryCanvasGroup.alpha = 1;
        }
    }

  
    void DestroySelf() //incase object needs destroyed
    {
        Destroy(gameObject);
    }
    

}
    

