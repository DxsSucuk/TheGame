using System;
using System.Collections.Generic;

using OPS.AntiCheat.Prefs;

using Photon.Pun;

using TMPro;

using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class PauseOptionsMenu : MonoBehaviourPunCallbacks
{

    public Slider masterVolumeSlider;
    public Slider soundVolumeSlider;
    public Slider voiceVolumeSlider;
    public AudioMixer voiceMixer;
    public AudioMixer backgroundMixer;
    public TMP_Dropdown dropdownGraphic;
    public TMP_Dropdown dropdownResolution;
    public TMP_Dropdown dropdownScreen;
    private Resolution[] resolutions;

    void Awake()
    {
        masterVolumeSlider.SetValueWithoutNotify(ProtectedPlayerPrefs.GetFloat("masterVolume", 1));
        soundVolumeSlider.SetValueWithoutNotify(ProtectedPlayerPrefs.GetFloat("soundVolume", 1));
        voiceVolumeSlider.SetValueWithoutNotify(ProtectedPlayerPrefs.GetFloat("voiceVolume", 1));
        backgroundMixer.SetFloat("soundVolume", Mathf.Log10(soundVolumeSlider.value) * 20);
        voiceMixer.SetFloat("voiceVolume", Mathf.Log10(voiceVolumeSlider.value) * 20);
        dropdownResolution.ClearOptions();

        resolutions = Screen.resolutions;

        List<string> options = new List<string>();

        int currentResolution = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
            Resolution res = resolutions[i];
            if (res.width == Screen.currentResolution.width && res.height == Screen.currentResolution.height)
            {
                currentResolution = i;
            }

            options.Add(res.height + "x" + res.width);
        }

        dropdownResolution.AddOptions(options);
        dropdownResolution.SetValueWithoutNotify(currentResolution);

        dropdownGraphic.ClearOptions();

        options = new List<string>();

        foreach (string name in QualitySettings.names)
        {
            options.Add(name);
        }

        dropdownGraphic.AddOptions(options);

        dropdownGraphic.SetValueWithoutNotify(QualitySettings.GetQualityLevel());

        dropdownScreen.SetValueWithoutNotify((int)Screen.fullScreenMode);

        gameObject.SetActive(false);
    }

    public void resolutionChange()
    {
        Resolution current = Screen.currentResolution;
        Resolution resolution = resolutions[dropdownResolution.value];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreenMode, current.refreshRate);
    }

    public void graphicChange()
    {
        QualitySettings.SetQualityLevel(dropdownGraphic.value);
    }

    public void screenChange()
    {
        FullScreenMode fullScreenMode = FullScreenMode.FullScreenWindow;

        switch (dropdownScreen.value)
        {
            case 0:
                fullScreenMode = FullScreenMode.ExclusiveFullScreen;
                break;
            case 1:
                fullScreenMode = FullScreenMode.FullScreenWindow;
                break;
            case 2:
                fullScreenMode = FullScreenMode.MaximizedWindow;
                break;
            case 3:
                fullScreenMode = FullScreenMode.Windowed;
                break;
            default:
                fullScreenMode = FullScreenMode.ExclusiveFullScreen;
                break;
        }

        Resolution current = Screen.currentResolution;
        Screen.SetResolution(current.width, current.height, fullScreenMode, current.refreshRate);
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
