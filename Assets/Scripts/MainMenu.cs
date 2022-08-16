using System;
using System.Linq;

using Photon.Pun;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviourPunCallbacks
{
    public TMP_Text playButtonText;
    public TMP_Text versionText;
    public TMP_InputField inputField;
    public TMP_InputField roomCodeInputField;
    private System.Random random = new System.Random();

    void Awake()
    {
        versionText.text = "Version: " + Application.version;
        Application.targetFrameRate = 60;
    }

    private void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    public void startGame()
    {
        if (!string.IsNullOrWhiteSpace(inputField.text) && inputField.text.Length >= 4 && inputField.text.Length <= 16)
        {
            PhotonNetwork.LocalPlayer.NickName = inputField.text;
        }
        else
        {
            PhotonNetwork.LocalPlayer.NickName = "RandomUser-" + random.Next(100);
        }
        
        playButtonText.text = "Loading...";
        SceneManager.LoadScene("GameWorld");
    }
    
    public void createGame()
    {
        if (!string.IsNullOrWhiteSpace(inputField.text) && inputField.text.Length >= 4 && inputField.text.Length <= 16)
        {
            PhotonNetwork.LocalPlayer.NickName = inputField.text;
        }
        else
        {
            PhotonNetwork.LocalPlayer.NickName = "RandomUser-" + RandomString(5);
        }

        PhotonNetwork.CreateRoom(RandomString(5), new Photon.Realtime.RoomOptions(), Photon.Realtime.TypedLobby.Default, null);
    }

    public void joinGame()
    {
        if (!string.IsNullOrWhiteSpace(inputField.text) && inputField.text.Length >= 4 && inputField.text.Length <= 16)
        {
            PhotonNetwork.LocalPlayer.NickName = inputField.text;
        }
        else
        {
            PhotonNetwork.LocalPlayer.NickName = "RandomUser-" + RandomString(5);
        }

        if (string.IsNullOrWhiteSpace(roomCodeInputField.text)) return;

        PhotonNetwork.JoinOrCreateRoom(roomCodeInputField.text, new Photon.Realtime.RoomOptions(), Photon.Realtime.TypedLobby.Default, null);
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("Joined room " + PhotonNetwork.CurrentRoom.Name);
        playButtonText.text = "Loading...";
        PhotonNetwork.LoadLevel(SceneManagerHelper.ActiveSceneBuildIndex + 1);
    }

    public void leaveGame()
    {
        Application.Quit();
    }

    string RandomString(int length)
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        return new string(Enumerable.Repeat(chars, length)
            .Select(s => s[random.Next(s.Length)]).ToArray());
    }
}
