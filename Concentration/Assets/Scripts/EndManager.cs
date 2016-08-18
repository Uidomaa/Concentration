using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndManager : MonoBehaviour {

    //public Text bestScoreTitle;
    public Text bestScore;
    //public Text playerScoreTitle;
	public Text playerScore;
    public GameObject newBest;

	//private int	playerScore = -1;

	// Use this for initialization
	void Start () {
        newBest.gameObject.SetActive(false);
		ShowScores();
	}

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    public void PlayGame ()
    {
        SaveLoadScript.Save();
        SceneManager.LoadScene("PlayScene");
    }

    public void GoToMenu()
    {
        SaveLoadScript.Save();
        SceneManager.LoadScene("WelcomeScene");
    }

    //	public void Score () {
    //		playerScoreText.text = playerScore.ToString();
    ////		if (playerScore > SaveLoadScript.prefData["highScore"])
    ////			SaveLoadScript.prefData["highScore"] = playerScore;
    //	}

    void ShowScores () {
        //Show player score
        int curScore = Concentration.GetTilesClicked();
        playerScore.text = curScore.ToString();
        //Check if curScore is better than highscore and update, show highscore
        if (curScore < SaveLoadScript.playerPreferences.highScore || SaveLoadScript.playerPreferences.highScore < 1)
        {
            newBest.SetActive(true);
            SaveLoadScript.playerPreferences.highScore = curScore;
        }
        //Show high score
        bestScore.text = SaveLoadScript.playerPreferences.highScore.ToString();
    }

	void OnApplicationQuit () {
		SaveLoadScript.Save();
	}
}
