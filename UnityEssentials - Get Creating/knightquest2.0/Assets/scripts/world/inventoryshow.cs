using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;
public class inventoryshow : MonoBehaviour
{ public CanvasGroup group; //script to show inventory on button press
    public void onInventoryButtonPress()
    { 
        show();
    }

    public void show()
    {
        group.alpha += 1;
    }
    
}
