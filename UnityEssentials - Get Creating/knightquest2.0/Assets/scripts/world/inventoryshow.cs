using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;
public class inventoryshow : MonoBehaviour
{ private CanvasGroup group;
    // Start is called before the first frame update
    void Start()
    {
        group = GetComponent<CanvasGroup>();
        group.alpha = 0;
    }

    // Update is called once per frame
    public void onInventoryButtonPress()
    { 
        show();
    }

    public void show()
    {
        group.alpha = 1;
    }
}
