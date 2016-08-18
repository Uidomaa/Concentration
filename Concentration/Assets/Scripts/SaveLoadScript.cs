using UnityEngine;
using System.Collections;
using LitJson;
using System.IO;

public static class SaveLoadScript {

	private static string	jsonString;
	public static JsonData	prefData;
	public static Preferences playerPreferences;

	// Use this for initialization
	public static void Load () {
		if (File.Exists(Application.dataPath + "/Resources/Preferences.json")) {
			jsonString = File.ReadAllText(Application.dataPath + "/Resources/Preferences.json");	
		} else {
			Debug.Log("No preferences folder exists - creating one now");
			CreatePrefFile();
			return;
		}
		//Convert string to json data
		prefData = JsonMapper.ToObject(jsonString);
		//Save as preferences object
		int playerScoreInt;
		if (int.TryParse(SaveLoadScript.prefData["highScore"].ToString(), out playerScoreInt))
			playerPreferences = new Preferences(playerScoreInt, SaveLoadScript.prefData["playerName"].ToString());
		else
			Debug.Log("Error in json file");
//		Debug.Log(itemData["Weapons"][1]["name"]);
	}

	public static void Save () {
		if (File.Exists(Application.dataPath + "/Resources/Preferences.json")) {
            //if (prefData != null)
            //	File.WriteAllText(Application.dataPath + "/Resources/Preferences.json", JsonMapper.ToJson(prefData));
            if (playerPreferences != null)
                File.WriteAllText(Application.dataPath + "/Resources/Preferences.json", JsonMapper.ToJson(playerPreferences));
            else
				Debug.Log("No preferences data");
		} else {
			Debug.Log("No preferences folder exists - creating one now");
			CreatePrefFile();
		}
	}

	static void CreatePrefFile () {
		//Create file
		FileStream file = File.Create(Application.dataPath + "/Resources/Preferences.json");
		file.Close();
		//Create preferences instance
		playerPreferences = new Preferences(-1, "Player1");
		//Convert to json
		prefData = JsonMapper.ToJson(playerPreferences);
		//Convert to json object (for consistency)
		prefData = JsonMapper.ToObject(prefData.ToString());
		//Write default data to preferences file
		File.WriteAllText(Application.dataPath + "/Resources/Preferences.json", JsonMapper.ToJson(playerPreferences));
	}

	public class Preferences {
		public int highScore = 0;
		public string playerName;

		public Preferences (int highScore, string playerName) {
			this.highScore = highScore;
			this.playerName = playerName;
		}
	}
}
