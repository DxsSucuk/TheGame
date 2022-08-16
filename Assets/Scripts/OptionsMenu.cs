using System;

using OPS.AntiCheat.Prefs;

using Photon.Pun;

using TMPro;

using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviourPunCallbacks
{

    public GameObject dropdownObject;
    public Slider masterVolumeSlider;
    public Slider soundVolumeSlider;
    public Slider voiceVolumeSlider;
    public AudioMixer voiceMixer;
    public AudioMixer backgroundMixer;
    private TMP_Dropdown dropdown;

    void Awake()
    {
        dropdown = dropdownObject.GetComponent<TMP_Dropdown>();
        masterVolumeSlider.value = ProtectedPlayerPrefs.GetFloat("masterVolume", 1);
        soundVolumeSlider.value = ProtectedPlayerPrefs.GetFloat("soundVolume", 1);
        voiceVolumeSlider.value = ProtectedPlayerPrefs.GetFloat("voiceVolume", 1);
        backgroundMixer.SetFloat("soundVolume", Mathf.Log10(soundVolumeSlider.value) * 20);
        voiceMixer.SetFloat("voiceVolume", Mathf.Log10(voiceVolumeSlider.value) * 20);
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

    public void saveMasterVolume()
    {
        ProtectedPlayerPrefs.SetFloat("masterVolume", masterVolumeSlider.value);
        ProtectedPlayerPrefs.Save();
    }

    public void saveSoundVolume()
    {
        backgroundMixer.SetFloat("soundVolume", Mathf.Log10(soundVolumeSlider.value) * 20);
        ProtectedPlayerPrefs.SetFloat("soundVolume", soundVolumeSlider.value);
        ProtectedPlayerPrefs.Save();
    }

    public void saveVoiceVolume()
    {
        voiceMixer.SetFloat("voiceVolume", Mathf.Log10(voiceVolumeSlider.value) * 20);
        ProtectedPlayerPrefs.SetFloat("voiceVolume", voiceVolumeSlider.value);
        ProtectedPlayerPrefs.Save();
    }
}
