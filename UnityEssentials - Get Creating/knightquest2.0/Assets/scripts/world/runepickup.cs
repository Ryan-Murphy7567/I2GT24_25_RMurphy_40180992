using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class runepickup : MonoBehaviour
{
    public CanvasGroup InventorySlotCanvasGroup;

    GameObject inventorySlotcanvas;
public Text displayText;
    // this script is for picking up each of the runes that i will be using in my inventory to enable them to be seen 
    void Start()
    {
        InventorySlotCanvasGroup = inventorySlotcanvas.GetComponent<CanvasGroup>();
        InventorySlotCanvasGroup.alpha = 0;
    }

    // Update is called once per frame
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            showcanvas();
            displayText.text = "Rune of Power added to Inventory";
            DestroySelf();
        }

        void showcanvas()
        {
            InventorySlotCanvasGroup.alpha = 1;
        }
    }

  
    void DestroySelf()
    {
        Destroy(gameObject);
    }
    

}

