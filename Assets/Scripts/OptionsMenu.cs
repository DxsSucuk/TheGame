using System;

using Photon.Pun;

using TMPro;

using UnityEngine;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviourPunCallbacks
{

    public GameObject dropdownObject;
    private TMP_Dropdown dropdown;

    void Awake()
    {
        dropdown = dropdownObject.GetComponent<TMP_Dropdown>();
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
    }
}
