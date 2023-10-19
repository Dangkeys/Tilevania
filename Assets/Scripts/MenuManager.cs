using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public void LoadGame()
    {
       SceneManager.LoadScene("Level1"); 
    }
    public void LoadGameOver()
    {
        SceneManager.LoadScene("Lose");
    }
    public void Quit()
    {
        Application.Quit();
    }
    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
