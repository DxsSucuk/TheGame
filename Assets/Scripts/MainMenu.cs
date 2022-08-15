using System;
using Photon.Pun;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = System.Random;

public class MainMenu : MonoBehaviourPunCallbacks
{
    public Text playButtonText;
    public TMP_InputField inputField;
    private void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    public void startGame()
    {
        if (!string.IsNullOrWhiteSpace(inputField.text) && inputField.text.Length >= 4)
        {
            PhotonNetwork.NickName = inputField.text;
        }
        else
        {
            PhotonNetwork.LocalPlayer.NickName = "RandomUser-" + new Random().Next(100);
        }
        
        playButtonText.text = "Loading...";
        SceneManager.LoadScene("GameWorld");
    }
    
    public void leaveGame()
    {
        Application.Quit();
    }
}
