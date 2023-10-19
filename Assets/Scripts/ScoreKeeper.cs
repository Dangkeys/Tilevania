using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class ScoreKeeper : MonoBehaviour
{
    [Range(0, int.MaxValue)] int score;
    static ScoreKeeper instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }else
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
    }
    public int GetScore() {return score;}
    public void ResetScore() {score = 0;}
    public void SetScore(int setScore) {score = setScore;}
 }
