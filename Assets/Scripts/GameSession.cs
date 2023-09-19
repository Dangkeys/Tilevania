using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSession : MonoBehaviour
{
    [SerializeField] int playerLives = 3;
    // Start is called before the first frame update
    void Awake()
    {
        int numberOfGameSession = FindObjectsOfType<GameSession>().Length;
        if (numberOfGameSession > 1)
            Destroy(gameObject);
        else
            DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    public void ProcessPlayerDeath()
    {
        if(playerLives > 1)
            TakeLive();
        else
            ResetGameSession();
    }

    private void ResetGameSession()
    {
        //hard code might change later
        SceneManager.LoadScene(0);
        Destroy(gameObject);
    }

    private void TakeLive()
    {
        playerLives--;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
