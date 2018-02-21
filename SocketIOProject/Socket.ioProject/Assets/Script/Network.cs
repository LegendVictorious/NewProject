using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SocketIO;

public class Network : MonoBehaviour {
    static SocketIOComponent socket;
    public GameObject playerPrefab;

    Dictionary<string, GameObject> players;

	// Use this for initialization
	void Start ()
    {
        socket = GetComponent<SocketIOComponent>();
        socket.On("open", OnConnected);
        socket.On("spawn player", OnSpawn);
        socket.On("disconnected", OnDisconnected);
        players = new Dictionary<string, GameObject>();
	}
	
	void OnConnected (SocketIOEvent e)
    {
        Debug.Log("We are connected");
        socket.Emit("playerhere");
	}

    void OnSpawn(SocketIOEvent e)
    {
        string id = e.data["id"].ToString();
        Debug.Log("Player spawned: " + id);
        GameObject player = Instantiate(playerPrefab);
        players.Add(id, player);
        Debug.Log("Count: " + players.Count);
    }

    void OnDisconnected(SocketIOEvent e)
    {
        string id = e.data["id"].ToString();
        Debug.Log("Player disconnected: " + id);
        GameObject player = players[id];
        Destroy(player);
        players.Remove(id);
    }
}
