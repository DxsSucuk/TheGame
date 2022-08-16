using Photon.Pun;
using Photon.Voice.PUN;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public FirstPersonController firstPersonController;

    public void resume()
    {
        firstPersonController.isPaused = false;
        Cursor.lockState = CursorLockMode.Locked;
        firstPersonController.pauseMenuUI.SetActive(false);
    }

    public void backToMenu()
    {
        if (PhotonNetwork.IsConnected)
            PhotonNetwork.Disconnect();

        PhotonVoiceNetwork.Instance.Disconnect();

        SceneManager.LoadScene(0);
    }
}
