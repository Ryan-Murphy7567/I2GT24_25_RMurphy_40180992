using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class loadnext : MonoBehaviour
{ //loading
    // Start is called before the first frame update
    public void nextscene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void loadnextpress()
    {
        nextscene();
    }
}
