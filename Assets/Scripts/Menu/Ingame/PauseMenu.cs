using Photon.Pun;
using Photon.Voice.PUN;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public FirstPersonController firstPersonController;
    public Sprite sprite;
    public GameObject backgroundPanel;
    private Image backgroundPanelImage;
    public GameObject pauseMenu;
    public GameObject optionMenu;

    private void Awake()
    {
        optionMenu.SetActive(false);
        backgroundPanelImage = backgroundPanel.GetComponent<Image>();
        backgroundPanelImage.sprite = sprite;
        backgroundPanelImage.color = new Color(0.235f, 0.235f, 0.235f, 0.588f);
    }

    private void Update()
    {
        Debug.Log(backgroundPanelImage.color);
    }

    private void OnEnable()
    {
        pauseMenu.SetActive(true);
        optionMenu.SetActive(false);
        backgroundPanelImage.sprite = sprite;
        backgroundPanelImage.color = new Color(0.235f, 0.235f, 0.235f, 0.588f);
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
