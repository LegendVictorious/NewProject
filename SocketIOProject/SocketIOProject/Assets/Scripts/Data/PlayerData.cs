using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class PlayerData : MonoBehaviour {

    string gameDataFilePath = "/StreamingAssets/data.json";

	// Use this for initialization
	void Start () {
        GameData jsonObj = new GameData();
        jsonObj.playerName = "Evan";
        jsonObj.score = 1500;
        jsonObj.timePlayed = 10000.961f;
        jsonObj.lastLogin = System.DateTime.Now.ToString();

        string json = JsonUtility.ToJson(jsonObj);
        string filePath = Application.dataPath + gameDataFilePath;
        File.WriteAllText(filePath, json);

        Debug.Log(json);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
