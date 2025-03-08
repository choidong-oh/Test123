using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;


public class GameManager : MonoBehaviourPunCallbacks
{
    private void Awake()
    {
        
    }
    private void Start()
    {
        // �� ����ȭ ���� (������ Ŭ���̾�Ʈ�� ���� �����ϸ� �ڵ����� ����ȭ��)
        PhotonNetwork.AutomaticallySyncScene = true;
        Screen.SetResolution(1366, 768, false);
    }
    public override void OnLeftRoom() //�� ������ �˾Ƽ� ȣ��
    {
        SceneManager.LoadScene(0); //Ÿ��Ʋ�� �̵�
    }

    public void LeaveRoom() //���߿� ������ ��ư ������� �޼���� ����
    {
        PhotonNetwork.LeaveRoom(); //���濡�� �� �����ٰ� ���
    }

    public void changeScene()
    {

        PhotonNetwork.LoadLevel("InGame");

    }
}
