using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Launcher : MonoBehaviourPunCallbacks
{
    public PhotonView playerPrefab;

    public Vector3 spawnpoint;
    
    // Start is called before the first frame update
    private void Start()
    {
        if (PhotonNetwork.IsConnected && PhotonNetwork.InRoom)
        {
            PhotonNetwork.Instantiate(playerPrefab.name, spawnpoint, Quaternion.identity);
        }
        else
        {
            if (!PhotonNetwork.IsConnected) 
                PhotonNetwork.ConnectUsingSettings();
            
            if (!PhotonNetwork.InRoom)
                PhotonNetwork.JoinRandomOrCreateRoom();
        }
    }

    public override void OnConnectedToMaster() {
        if (!PhotonNetwork.InRoom)
            PhotonNetwork.JoinRandomOrCreateRoom();
    }
    
    public override void OnJoinedRoom() {
        Debug.Log("Joined room");
        PhotonNetwork.Instantiate(playerPrefab.name, spawnpoint, Quaternion.identity);
    }
}