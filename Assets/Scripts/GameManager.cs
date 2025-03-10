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
        // �� ����ȭ ���� (������ Ŭ���̾�Ʈ�� ���� �����ϸ� �ڵ����� ����ȭ��)
        PhotonNetwork.AutomaticallySyncScene = true;
        Screen.SetResolution(1366, 768, false);

        Application.logMessageReceived += HandleLog;
    }

    //������ �����ؾߴ�, �޸� ����
    private void OnDestroy()
    {
        Application.logMessageReceived -= HandleLog; 
    }

    private void HandleLog(string logString, string stackTrace, LogType type)
    {
        string logMessage = $"[{type}] {logString}\n"; // �α� Ÿ�԰� �޽��� ����

        if (debugText != null)
        {
            debugText.text += logMessage; // UI Text�� �߰�
        }
        Canvas.ForceUpdateCanvases();
        scrollRect.verticalNormalizedPosition = 0;

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
