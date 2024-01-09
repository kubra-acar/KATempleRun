using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour {

    public int score;
    public static GameManager inst;

    [SerializeField] Text scoreText;

    [SerializeField] PlayerMovement playerMovement;

    public Text highscoreText, endScoreText;

    public GameObject panel;

    public void IncrementScore ()
    {
        score++;
        scoreText.text = "SCORE: " + score;
        // Increase the player's speed
        playerMovement.speed += playerMovement.speedIncreasePerPoint;
    }

    private void Awake ()
    {
        inst = this;
    }

    private void Start () {

	}

    public void GameFinished()
    {
        panel.SetActive(true);

        int scoreDatabase = SQLiteBasic.Instance.GetScore(PlayerData.Instance.playerName);

        if(score > scoreDatabase) //
        {
            SQLiteBasic.Instance.SendHighscore(PlayerData.Instance.playerName, score);

        }

        endScoreText.text = PlayerData.Instance.playerName + " Score : " + score.ToString();
        highscoreText.text = PlayerData.Instance.playerName + " Highscore : " + SQLiteBasic.Instance.GetScore(PlayerData.Instance.playerName);


    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}