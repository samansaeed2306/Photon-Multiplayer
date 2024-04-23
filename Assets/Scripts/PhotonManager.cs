using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using TMPro;

public class PhotonManager : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
    public TMP_InputField userNameText;

    public GameObject PlayerNamePanel;
    public GameObject LobbyPanel;
    public GameObject RoomCreatePanel;
    public GameObject LoadingPanel;
    #region UnityMethods

    void Start()
    {
        ActivateMyPanel(PlayerNamePanel.name);
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Network State:" + PhotonNetwork.NetworkClientState);
    }
    #endregion
    #region UiMethods
    public void onLoginClick()
    {
        string name = userNameText.text;
        if (!string.IsNullOrEmpty(name))
        {
            PhotonNetwork.LocalPlayer.NickName = name;
            PhotonNetwork.ConnectUsingSettings();
            ActivateMyPanel(LoadingPanel.name);
        }
        else
        {
            Debug.Log("Empty name ");
        }
    }
    #endregion

    #region PHOTON_CALLBACKS
    public override void OnConnected()
    {
        Debug.Log("Connected to Internet!");
    }
    public override void OnConnectedToMaster()
    {
        Debug.Log(PhotonNetwork.LocalPlayer.NickName + " is connected to photon...");
        ActivateMyPanel(LobbyPanel.name);
    }
    #endregion


    #region Public_Methods
    public void ActivateMyPanel(string panelName)
    {
        LobbyPanel.SetActive(panelName.Equals(LobbyPanel.name));
        PlayerNamePanel.SetActive(panelName.Equals(PlayerNamePanel.name));
        RoomCreatePanel.SetActive(panelName.Equals(RoomCreatePanel.name));
        LoadingPanel.SetActive(panelName.Equals(LoadingPanel.name));
    }
    #endregion
}
