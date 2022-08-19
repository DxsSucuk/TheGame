using Photon.Pun;
using Photon.Realtime;
using Photon.Voice.PUN;

using System.Collections;
using System.Collections.Generic;

using TMPro;

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public FirstPersonController firstPersonController;
    public TMP_Text roomCodeText;
    public TMP_Text playerListText;
    public Sprite sprite;
    private Image backgroundPanelImage;
    public GameObject pauseMenu;
    public GameObject optionMenu;

    private void Awake()
    {
        optionMenu.SetActive(false);
        backgroundPanelImage = GetComponent<Image>();
        backgroundPanelImage.sprite = sprite;
        backgroundPanelImage.color = new Color(0.235f, 0.235f, 0.235f, 0.588f);

        if (PhotonNetwork.CurrentRoom != null)
            roomCodeText.text = PhotonNetwork.CurrentRoom.Name;
    }

    private void OnEnable()
    {
        pauseMenu.SetActive(true);
        optionMenu.SetActive(false);
        backgroundPanelImage.sprite = sprite;
        backgroundPanelImage.color = new Color(0.235f, 0.235f, 0.235f, 0.588f);
    }

    private void Update()
    {
        string text = string.Empty;
        foreach (Player player in PhotonNetwork.PlayerList)
        {
            text += player.NickName + (player.IsMasterClient ? " [H]" : string.Empty) + "\n";
        }

        playerListText.text = text.Trim();
    }

    public void resume()
    {
        firstPersonController.isPaused = false;
        Cursor.lockState = CursorLockMode.Locked;
        firstPersonController.pauseMenuUI.SetActive(false);
    }

    public void backToMenu()
    {
        if (PhotonNetwork.InRoom)
        {
            PhotonNetwork.DestroyPlayerObjects(PhotonNetwork.LocalPlayer);
            PhotonNetwork.LeaveRoom();
        }

        SceneManager.LoadScene(0);
    }
}
