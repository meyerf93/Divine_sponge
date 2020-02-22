using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu_manager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void playButton ()
    {
        SceneManager.LoadScene("Game");
    }

    public void creditButton()
    {
        SceneManager.LoadScene("Credit");
    }

    public void mainMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void controlButton()
    {
        SceneManager.LoadScene("Controls");
    }

    public void exitGame()
    {
        Debug.Log("game exit");
        Application.Quit();
    }
}
