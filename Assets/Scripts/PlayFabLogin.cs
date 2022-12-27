using Photon.Pun;
using PlayFab;
using PlayFab.ClientModels;
using System;
using TMPro;
//using UnityEditor.PackageManager.Requests;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayFabLogin : MonoBehaviourPunCallbacks
{
    public GameObject loginScreen;
    public GameObject registerScreen;
    //public TMP_Text usernametext;

    //Login............................................................
    public TMP_Text loginErrorMsg;
    public TMP_InputField loginEmailInput;
    public TMP_InputField loginPasswordInput;
    //Register.........................................................
    public TMP_Text registerErrorMsg;
    public TMP_InputField usernameInput;
    public TMP_InputField registerEmailInput;
    public TMP_InputField registerPasswordInput;

    public void Start()
    {
        Debug.Log(SystemInfo.deviceUniqueIdentifier);
    }

    public void GoToRegistration()
    {
        loginScreen.SetActive(false);
        registerScreen.SetActive(true);
        ResetAllText();
    }

    public void GoToLogin()
    {
        loginScreen.SetActive(true);
        registerScreen.SetActive(false);
        ResetAllText();
    }

    public void RegisterClicked()
    {
        var request = new RegisterPlayFabUserRequest
        {
            Email = registerEmailInput.text,
            Password = registerPasswordInput.text,
            Username = usernameInput.text,
            DisplayName = usernameInput.text,
            RequireBothUsernameAndEmail = true
        };
        PlayFabClientAPI.RegisterPlayFabUser(request, OnRegisterSuccess, OnRegisterError);
    }

    public void LoginClicked()
    {
        var request = new LoginWithEmailAddressRequest {
            Email = loginEmailInput.text,
            Password = loginPasswordInput.text,
            InfoRequestParameters = new GetPlayerCombinedInfoRequestParams()
            {
                GetPlayerProfile = true
            }
        };
        PlayFabClientAPI.LoginWithEmailAddress(request, OnLoginSuccess, OnLoginError);
    }

    void OnRegisterSuccess(RegisterPlayFabUserResult result)
    {
        Debug.Log("registered!");
        loginScreen.SetActive(true);
        registerScreen.SetActive(false);
        
    }

    void OnLoginSuccess(LoginResult result)
    {
        if (result.InfoResultPayload.PlayerProfile != null)
        {
            PhotonNetwork.NickName = result.InfoResultPayload.PlayerProfile.DisplayName;
            PhotonNetwork.ConnectUsingSettings();
        }
    }

    void OnRegisterError(PlayFabError error)
    {
        Debug.Log("error");
        registerErrorMsg.text = error.ErrorMessage;
    }
    void OnLoginError(PlayFabError error)
    {
        Debug.Log("error");
        loginErrorMsg.text = error.ErrorMessage;
    }

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        SceneManager.LoadScene(2);
    }

    void ResetAllText()
    {
        loginErrorMsg.text = "";
        loginEmailInput.text = "";
        loginPasswordInput.text = "";
        registerErrorMsg.text = "";
        usernameInput.text = "";
        registerEmailInput.text = "";
        registerPasswordInput.text = "";
    }
}
