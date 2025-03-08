using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerNameInputField : MonoBehaviour
{
    InputField inputField;
    const string playerNamePrefKey = "PlayerName";

    void Start()
    {
        string defaultName = string.Empty;
        inputField = GetComponent<InputField>();

        if (inputField != null) //방어코드
        {
            if (PlayerPrefs.HasKey(playerNamePrefKey))
            {
                defaultName = PlayerPrefs.GetString(playerNamePrefKey);
                inputField.text = defaultName;
            }
        }

        PhotonNetwork.NickName = defaultName;
    }
    public void SetPlayerName(string value)
    {
        if (string.IsNullOrEmpty(value))
        {
            Debug.LogError("플레이어 이름이 비어있습니다");
            return;
        }
        PhotonNetwork.NickName = value;

        PlayerPrefs.SetString(playerNamePrefKey, value);
    }
}


