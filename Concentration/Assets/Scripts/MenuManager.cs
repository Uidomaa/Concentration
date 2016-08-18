using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour {

	public Text	playerName;
	public Text playerScoreTitle;
	public Text playerScore;

	//private int	playerScore = -1;

	// Use this for initialization
	void Start () {
        playerScoreTitle.gameObject.SetActive(false);
        playerScore.gameObject.SetActive(false);
        SaveLoadScript.Load();
		ShowHighScores();
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
        //yield return new WaitForSeconds(0.1f);
        SceneManager.LoadScene("PlayScene");
    }

//	public void Score () {
//		playerScoreText.text = playerScore.ToString();
////		if (playerScore > SaveLoadScript.prefData["highScore"])
////			SaveLoadScript.prefData["highScore"] = playerScore;
//	}

	void ShowHighScores () {
        if (SaveLoadScript.playerPreferences.highScore > 0)
        {
            //playerName.text = SaveLoadScript.playerPreferences.playerName;
            playerScore.text = SaveLoadScript.playerPreferences.highScore.ToString();
            playerScoreTitle.gameObject.SetActive(true);
            playerScore.gameObject.SetActive(true);
        }
	}

	void OnApplicationQuit () {
		SaveLoadScript.Save();
	}
}
