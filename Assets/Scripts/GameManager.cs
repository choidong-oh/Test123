using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;


public class GameManager : MonoBehaviourPunCallbacks
{
    public Text debugText;
    public ScrollRect scrollRect;

    private void Start()
    {
        // 씬 동기화 설정 (마스터 클라이언트가 씬을 변경하면 자동으로 동기화됨)
        PhotonNetwork.AutomaticallySyncScene = true;
        Screen.SetResolution(1366, 768, false);

        Application.logMessageReceived += HandleLog;
    }

    //삭제도 생각해야댐, 메모리 누수
    private void OnDestroy()
    {
        Application.logMessageReceived -= HandleLog; 
    }

    private void HandleLog(string logString, string stackTrace, LogType type)
    {
        string logMessage = $"[{type}] {logString}\n"; // 로그 타입과 메시지 조합

        if (debugText != null)
        {
            debugText.text += logMessage; // UI Text에 추가
        }
        Canvas.ForceUpdateCanvases();
        scrollRect.verticalNormalizedPosition = 0;

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
