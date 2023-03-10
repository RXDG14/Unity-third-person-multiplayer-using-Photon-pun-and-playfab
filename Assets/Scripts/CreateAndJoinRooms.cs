using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CreateAndJoinRooms : MonoBehaviourPunCallbacks
{
    public TMP_InputField joinInput;
    public TMP_InputField createInput;

    public void CreateRoom()
    {
        createInput.interactable = false;
        PhotonNetwork.CreateRoom(createInput.text);
    }

    public void JoinRoom()
    {
        joinInput.interactable = false;
        PhotonNetwork.JoinRoom(joinInput.text);
    }

    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel(4);
    }
}
