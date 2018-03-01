using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using UnityEditor;
using SocketIO;

public class GameDataEditor : EditorWindow {

    string gameDataFilePath = "/StreamingAssets/data.json";
    public GameData editorData;
    private static GameObject server;
    private SocketIOComponent socket;

    [MenuItem("Window/Game Data Editor")]
    static void Init()
    {
        EditorWindow.GetWindow(typeof(GameDataEditor)).Show();
    }

    void OnGUI()
    {
        SerializedObject serializedObject = new SerializedObject(this);
        SerializedProperty serializedProperty = serializedObject.FindProperty("editorData");
        EditorGUILayout.PropertyField(serializedProperty, true);
        serializedObject.ApplyModifiedProperties();

        if(editorData != null)
        {
            if(GUILayout.Button("Save Game Data"))
            {
                SaveGameData();
            }
        }

        if (GUILayout.Button("Load Game Data"))
        {
            LoadGameData();
        }
        if (GUILayout.Button("Send Game Data"))
        {
            SendGameData();
        }
    }

    void LoadGameData()
    {
        string filePath = Application.dataPath + gameDataFilePath;

        if (File.Exists(filePath))
        {
            string gameData = File.ReadAllText(filePath);
            editorData = JsonUtility.FromJson<GameData>(gameData);
        }
        else
        {
            editorData = new GameData();
        }
    }

    void SaveGameData()
    {
        string jsonObj = JsonUtility.ToJson(editorData);
        string filePath = Application.dataPath + gameDataFilePath;
        File.WriteAllText(filePath, jsonObj);
    }

    void SendGameData()
    {
        string jsonObj = JsonUtility.ToJson(editorData);

        server = GameObject.Find("Server");
        if (server)
        {
            socket = server.GetComponent<SocketIOComponent>();
            socket.Emit("send data", new JSONObject(jsonObj));
        }
        else
        {
            Debug.Log("There is no server object in the active scene!");
        }
    }

    DateTime ConvertToDateTime(string dateTimeString)
    {
        return Convert.ToDateTime(dateTimeString);
    }
}
