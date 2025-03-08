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
        // 씬 동기화 설정 (마스터 클라이언트가 씬을 변경하면 자동으로 동기화됨)
        PhotonNetwork.AutomaticallySyncScene = true;
        Screen.SetResolution(1366, 768, false);
    }
    public override void OnLeftRoom() //방 나가면 알아서 호출
    {
        SceneManager.LoadScene(0); //타이틀로 이동
    }

    public void LeaveRoom() //나중에 나가기 버튼 만드려고 메서드로 빼둠
    {
        PhotonNetwork.LeaveRoom(); //포톤에게 방 나간다고 명령
    }

    public void changeScene()
    {

        PhotonNetwork.LoadLevel("InGame");

    }
}
