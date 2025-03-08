using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
public class Launcher : MonoBehaviourPunCallbacks
{
    string gameVersion = "1";

    void Awake()
    {
        PhotonNetwork.AutomaticallySyncScene = true; 
    }


    public void Connect()
    {
        if (PhotonNetwork.IsConnected)
        {
            PhotonNetwork.CreateRoom(PhotonNetwork.NickName, new RoomOptions()); //방 만들어주는 메서드. 앞엔 방 이름, 뒤엔 옵션
        }
        else
        {

            //서버 연결
            PhotonNetwork.GameVersion = gameVersion;
            PhotonNetwork.ConnectUsingSettings();
        }



    }
    public  void isServer()
    {
        //서버 연결
        PhotonNetwork.GameVersion = gameVersion;
        PhotonNetwork.ConnectUsingSettings();
    }

    public void joinConnect()
    {
        PhotonNetwork.JoinRandomRoom();
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("펀 마스터 서버 연결 성공");
        //PhotonNetwork.JoinLobby();// 로비는 방에 들어가기 전에 대기하는 공간. 다양한 방 목록을 받아올 수 있음
        //PhotonNetwork.JoinRandomRoom();//로비에 있는 방 중 랜덤한 방에 들어가게 하는 함수. 실패할 수 있음
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.Log("연결 끊김 감지. 사유: " + cause);
    }
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("방 참여 실패. 보통 이러면 새로운 방 생성");
        PhotonNetwork.CreateRoom(PhotonNetwork.NickName, new RoomOptions()); //방 만들어주는 메서드. 앞엔 방 이름, 뒤엔 옵션
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("클라이언트가 방에 입장시에 알아서 호출");
        Debug.Log(PhotonNetwork.CurrentRoom.Name);
    }


}
