using System;

using OPS.AntiCheat.Prefs;

using Photon.Pun;

using TMPro;

using UnityEngine;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviourPunCallbacks
{

    public GameObject dropdownObject;
    public Slider volumeSlider;
    public Slider voiceVolumeSlider;
    private TMP_Dropdown dropdown;

    void Awake()
    {
        dropdown = dropdownObject.GetComponent<TMP_Dropdown>();
        volumeSlider.value = ProtectedPlayerPrefs.GetFloat("soundVolume", 1);
        voiceVolumeSlider.value = ProtectedPlayerPrefs.GetFloat("voiceVolume", 1);
        dropdown.SetValueWithoutNotify(dropdown.options.FindIndex(0, dropdown.options.Count, c => c.text == ProtectedPlayerPrefs.GetString("region", "AUTO")));
    }

    public void regionChange()
    {
        if (PhotonNetwork.IsConnected)
            PhotonNetwork.Disconnect();

        string textValue = dropdown.options[dropdown.value].text;

        if (textValue.Equals("AUTO"))
        {
            Debug.Log("Connecting to best Region!");


            PhotonNetwork.ConnectToBestCloudServer();
        }
        else if (textValue.Equals("DEV"))
        {
            Debug.Log("Connecting to Region " + textValue + "!");
            PhotonNetwork.ConnectToRegion("EU");
        }
        else
        {
            Debug.Log("Connecting to Region " + textValue + "!");
            PhotonNetwork.ConnectToRegion(textValue);
        }

        ProtectedPlayerPrefs.SetString("region", textValue);
        ProtectedPlayerPrefs.Save();
    }

    public void saveSoundVolume()
    {
        ProtectedPlayerPrefs.SetFloat("soundVolume", volumeSlider.value);
        ProtectedPlayerPrefs.Save();
    }

    public void saveVoiceVolume()
    {
        ProtectedPlayerPrefs.SetFloat("voiceVolume", voiceVolumeSlider.value);
        ProtectedPlayerPrefs.Save();
    }
}
