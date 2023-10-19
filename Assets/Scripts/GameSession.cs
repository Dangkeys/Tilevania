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
    ScoreKeeper scoreKeeper;
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
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
    }
    private void Start()
    {
        livesText.text = playerLives.ToString();
        scoreText.text = score.ToString();
    }

    // Update is called once per frame
    public void ProcessPlayerDeath()
    {
        if (playerLives > 1)
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
        FindObjectOfType<ScenePersist>().ResetScenePersist();
        //hard code might change later
        SceneManager.LoadScene("Lose");
        Destroy(gameObject);
    }

    private void TakeLive()
    {
        playerLives--;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        livesText.text = playerLives.ToString();
    }
}
