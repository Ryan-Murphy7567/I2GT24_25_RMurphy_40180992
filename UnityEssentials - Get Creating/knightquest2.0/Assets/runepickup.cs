using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class runepickup : MonoBehaviour
{
    public CanvasGroup InventorySlotCanvasGroup;

    GameObject inventorySlotcanvas;
public Text displayText;
    // Start is called before the first frame update
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

