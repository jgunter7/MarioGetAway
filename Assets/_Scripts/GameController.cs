/*
 *      File:                   GameController.cs
 *      Authors Name:           Jason Gunter
 *      Last Modified By:       Jason Gunter
 *      Date Last Modified:     October 18th, 2016
 *      Description:            A unity platform game featuring mario escaping from jail :) - jgunter
 *      Revision History:       https://github.com/jgunter7/MarioGetAway/commits/master 
 */
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour {
    //PRIVATE VARIABLES
    private int _lives;
    private int _score;

    //PUBLIC VARIABLES
    [Header("UI Objects")]
    public Text LivesLabel;
    public Text ScoreLabel;
    public Text GameOverLabel;
    public Text FinalScoreLabel;
    public Button RestartButton;

    public int LivesValue {
        get {
            return this._lives;
        }

        set {
            this._lives = value;
            if (this._lives <= 0) {
                this._endGame();
            } else {
                this.LivesLabel.text = "Lives: " + this._lives;
            }
        }
    }

    public int ScoreValue {
        get {
            return this._score;
        }

        set {
            this._score = value;
            this.ScoreLabel.text = "Score: " + this._score;
        }
    }


    // Use this for initialization
    void Start () {
        this.ScoreValue = 0;
        this.LivesValue = 10; // very generous! - jgunter
        // hide items at start:
        this.GameOverLabel.gameObject.SetActive(false);
        this.FinalScoreLabel.gameObject.SetActive(false);
        this.RestartButton.gameObject.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void _endGame() {
        this.LivesLabel.gameObject.SetActive(false);
        this.ScoreLabel.gameObject.SetActive(false);
        // SET GAME OVER SCREEN ! - jgunter
        var player = GameObject.FindWithTag("Player");
        player.gameObject.SetActive(false); // get rid of the player component
        this.GameOverLabel.gameObject.SetActive(true);
        this.FinalScoreLabel.gameObject.SetActive(true);
        this.RestartButton.gameObject.SetActive(true);
        this.FinalScoreLabel.text = "Final Score: " + this._score;
    }

    public void RestartButton_Click() {
        SceneManager.LoadScene("Play");
    }
}
