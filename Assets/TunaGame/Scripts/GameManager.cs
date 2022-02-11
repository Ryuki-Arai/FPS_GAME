using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance 
    {
        get 
        {
            if (_instance == null)
            {
                var obj = new GameObject(typeof(GameManager).Name);
                _instance = obj.AddComponent<GameManager>();
            }
            return _instance; 
        }
    }

    private void Awake()
    {
        _instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

    int score;
    int highscore;
    string user;

    public int Score
    {
        get => score;
        set
        {
            score += value;
        }
    }

    public string UserName
    {
        get
        {
            if (PlayerPrefs.HasKey("USERNAME")) user = PlayerPrefs.GetString("USERNAME");
            else user = "Guest";
            return user;
        }
        set
        {
            PlayerPrefs.SetString("USERNAME",value);
            PlayerPrefs.Save();
        }
    }

    public int HighScore
    {
        get
        {
            if (PlayerPrefs.HasKey("HIGHSCORE")) highscore = PlayerPrefs.GetInt("HIGHSCORE");
            else highscore = 0;
            return highscore;
        }
        set
        {
            var hs = HighScore;
            if (hs < score) PlayerPrefs.SetInt("HIGHSCORE", score);
            PlayerPrefs.Save();
        }
    }
    
    public void SetZero()
    {
        score = 0;
    }

    public void Reset()
    {
        PlayerPrefs.DeleteAll();
    }
}
