﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SocketIO;

public class Network : MonoBehaviour {
    static SocketIOComponent socket;

	// Use this for initialization
	void Start () {
        socket = GetComponent<SocketIOComponent>();
        socket.On("open", OnConnected);
	}
	
	void OnConnected (SocketIOEvent e) {
        Debug.Log("We are connected");
	}
}
