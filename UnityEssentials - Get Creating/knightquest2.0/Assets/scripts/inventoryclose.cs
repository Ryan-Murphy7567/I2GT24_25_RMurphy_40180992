using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class inventoryclose : MonoBehaviour
{
    // Start is called before the first frame update
    public CanvasGroup group; //script to hide inventory on button press made on a second script as i was scared of messing my other stuff up
    public void closeButtonPress()
    { 
        hide();
    }

    public void hide()
    {
        group.alpha -= 1;
    }
}


