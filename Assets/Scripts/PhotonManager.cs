using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using TMPro;
using Photon.Realtime;
public class PhotonManager : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
    public TMP_InputField userNameText;
    public TMP_InputField roomNameText;

    public TMP_InputField maxPlayer;
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
    public void OnClickCreateRoom()
    {
        string roomName = roomNameText.text.Trim();
        if (string.IsNullOrEmpty(roomName))
        {
            roomName = "Room" + Random.Range(0, 1000);
        }

        int maxPlayers;
        if (!int.TryParse(maxPlayer.text, out maxPlayers))
        {
            Debug.Log("Invalid Max Players value");
            return;
        }

        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = (byte)maxPlayers;

        PhotonNetwork.CreateRoom(roomName, roomOptions);

        Debug.Log("Room Name: " + roomName);
        Debug.Log("Max Players: " + maxPlayers);
    }

    // public void OnClickCreateRoom()
    // {
    //     string roomName = roomNameText.text;
    //     if (string.IsNullOrEmpty(roomName))
    //     {
    //         roomName = roomName + Random.Range(0, 1000);
    //         RoomOptions roomOptions = new RoomOptions();
    //         roomOptions.MaxPlayers = (byte)int.Parse(maxPlayer.text);
    //         PhotonNetwork.CreateRoom(roomName, roomOptions);
    //         Debug.Log("Room Name: " + roomNameText.text);
    //         Debug.Log("Max Players: " + maxPlayer.text);

    //     }
    // }
    public void OnCancelClick()
    {
        Debug.Log("You have exited the room");
        ActivateMyPanel(LobbyPanel.name);
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

    public override void OnCreatedRoom()
    {
        Debug.Log(PhotonNetwork.CurrentRoom.Name + " is created !");
    }
    public override void OnJoinedRoom()
    {
        Debug.Log(PhotonNetwork.LocalPlayer.NickName + "Room Joined");
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
