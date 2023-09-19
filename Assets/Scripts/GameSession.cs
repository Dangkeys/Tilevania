using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSession : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI livesText;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] int playerLives = 3;
    [SerializeField] int score;
    // Start is called before the first frame update
    void Awake()
    {
        int numberOfGameSession = FindObjectsOfType<GameSession>().Length;
        if (numberOfGameSession > 1)
            Destroy(gameObject);
        else
            DontDestroyOnLoad(gameObject);
    }
    private void Start() {
        livesText.text = playerLives.ToString();
        scoreText.text = score.ToString();
    }

    // Update is called once per frame
    public void ProcessPlayerDeath()
    {
        if(playerLives > 1)
            TakeLive();
        else
            ResetGameSession();
    }
    public void AddToScore(int pointsToAdd)
    {
        score += pointsToAdd;
        scoreText.text = score.ToString();
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
        livesText.text = playerLives.ToString();
    }
}
