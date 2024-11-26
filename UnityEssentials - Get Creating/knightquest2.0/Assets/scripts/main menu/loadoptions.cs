using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Begingame : MonoBehaviour
{
    // Start is called before the first frame update
   public void BeginGame()
   {
       SceneManager.LoadScene("levelone");
   }

    // Update is called once per frame
   public void Options()
    {
        SceneManager.LoadScene("options");
    }

    public void knownissues()
    {
        SceneManager.LoadScene("knownissues");
    }
    public void mainmenu()
    {
        SceneManager.LoadScene("mainmenu");
    }

    public void bossfight()
    {
        SceneManager.LoadScene("bossfight");
        ;
    }

    public void Startgame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
