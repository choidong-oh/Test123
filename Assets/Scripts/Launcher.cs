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
            PhotonNetwork.CreateRoom(PhotonNetwork.NickName, new RoomOptions()); //�� ������ִ� �޼���. �տ� �� �̸�, �ڿ� �ɼ�
        }
        else
        {

            //���� ����
            PhotonNetwork.GameVersion = gameVersion;
            PhotonNetwork.ConnectUsingSettings();
        }



    }
    public  void isServer()
    {
        //���� ����
        PhotonNetwork.GameVersion = gameVersion;
        PhotonNetwork.ConnectUsingSettings();
    }

    public void joinConnect()
    {
        PhotonNetwork.JoinRandomRoom();
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("�� ������ ���� ���� ����");
        //PhotonNetwork.JoinLobby();// �κ�� �濡 ���� ���� ����ϴ� ����. �پ��� �� ����� �޾ƿ� �� ����
        //PhotonNetwork.JoinRandomRoom();//�κ� �ִ� �� �� ������ �濡 ���� �ϴ� �Լ�. ������ �� ����
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.Log("���� ���� ����. ����: " + cause);
    }
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("�� ���� ����. ���� �̷��� ���ο� �� ����");
        PhotonNetwork.CreateRoom(PhotonNetwork.NickName, new RoomOptions()); //�� ������ִ� �޼���. �տ� �� �̸�, �ڿ� �ɼ�
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("Ŭ���̾�Ʈ�� �濡 ����ÿ� �˾Ƽ� ȣ��");
        Debug.Log(PhotonNetwork.CurrentRoom.Name);
    }


}
