using Photon.Pun;
using Photon.Voice.PUN;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playrrr : MonoBehaviour
{
    public PhotonView view;
    public PhotonVoiceView voiceView;

    private void Start()
    {
        if (voiceView.IsRecording)
        {
            Debug.Log(view.name);
        }
    }
}
