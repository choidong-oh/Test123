using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class tessst : MonoBehaviourPunCallbacks
{
    public GameObject Black;
    public GameObject White;
    public GameObject[] Tr;
    int numA = 1;
    int numB = 1;
    public bool isPlayerA;
    public bool myTurn = true;//방장 true, 다른애 false
    public GameObject winUi;

    //포톤 간략설명 사이트
    //https://velog.io/@hhj3258/123

    //이론상 맞는데? 


    void Start()
    {
        isPlayerA = PhotonNetwork.IsMasterClient;
    }

    public void OnButton1(int number)
    {

        photonView.RPC("PhotonRPC", RpcTarget.All, number, isPlayerA);



        //PlayerB();
    }

    //마스터면 A, 아니면B
    [PunRPC]
    void PhotonRPC(int number, bool isA)
    {
        if (isA == true && myTurn == true)
        {
            PlayerA(number);
            myTurn = false;
        }
        else if (isA == false && myTurn == false)
        {
            PlayerB(number);
            myTurn = true;
        }
    }


    void PlayerA(int number)
    {
        var dd = PhotonNetwork.Instantiate(Black.name, Tr[number].transform.position, Quaternion.identity);
        if (numA != 5)
        {
            numA += DirectionA(Vector3.up, number);
            numA += DirectionA(Vector3.down, number);
            if (numA == 5)
            {
                winUi.SetActive(true);
                Debug.Log("게임끝");
            }
            else
            {
                numA = 1;
            }

        }
        if (numA != 5)
        {
            numA += DirectionA(Vector3.right, number);
            numA += DirectionA(Vector3.left, number);
            if (numA == 5)
            {
                winUi.SetActive(true);
                Debug.Log("게임끝");
            }
            else
            {
                numA = 1;
            }

        }

        if (numA != 5)
        {
            numA += DirectionA(Vector3.right + Vector3.up,number);
            numA += DirectionA(Vector3.left - Vector3.up, number);
            if (numA == 5)
            {
                winUi.SetActive(true);
                Debug.Log("게임끝");
            }
            else
            {
                numA = 1;
            }

        }
        if (numA != 5)
        {
            numA += DirectionA(Vector3.left + Vector3.up, number);
            numA += DirectionA(Vector3.right - Vector3.up, number);
            if (numA == 5)
            {
                winUi.SetActive(true);
                Debug.Log("게임끝");
            }
            else
            {
                numA = 1;
            }

        }
        Debug.Log(numA);
    }
    void PlayerB(int number)
    {
        var dd = PhotonNetwork.Instantiate(White.name, Tr[number].transform.position, Quaternion.identity);
        if (numB != 5)
        {
            numB += DirectionB(Vector3.up, number);
            numB += DirectionB(Vector3.down, number);
            if (numB == 5)
            {
                winUi.SetActive(true);
                Debug.Log("게임끝");
            }
            else
            {
                numB = 1;
            }

        }
        if (numB!= 5)
        {
            numB += DirectionB(Vector3.right, number);
            numB += DirectionB(Vector3.left, number);
            if (numB == 5)
            {
                winUi.SetActive(true);
                Debug.Log("게임끝");
            }
            else
            {
                numB = 1;
            }

        }

        if (numB != 5)
        {
            numB += DirectionB(Vector3.right + Vector3.up, number);
            numB += DirectionB(Vector3.left - Vector3.up, number);
            if (numB == 5)
            {
                winUi.SetActive(true);
                Debug.Log("게임끝");
            }
            else
            {
                numB = 1;
            }

        }
        if (numB != 5)
        {
            numB += DirectionB(Vector3.left + Vector3.up, number);
            numB += DirectionB(Vector3.right - Vector3.up, number);
            if (numB == 5)
            {
                winUi.SetActive(true);
                Debug.Log("게임끝");
            }
            else
            {
                numB = 1;
            }

        }
        Debug.Log(numB);
    }


    private int DirectionA(Vector3 direction, int number)
    {
        int count = 0;
        Vector3 startPosition = Tr[number].transform.position;

        for (int i = 0; i < 4; i++) 
        {
            Vector3 rayOrigin = startPosition + (direction * i * 0.5f); // 이동 방향 적용
            Ray ray = new Ray(rayOrigin, direction);
            //Ray ray = new Ray(new Vector3(startPosition.x, startPosition.y+i*0.5f, startPosition.z),direction); 
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 0.5f))
            {
                if (hit.collider.CompareTag("Black"))
                {
                    Debug.Log("블랙 닷음");
                    count++;
                }
                else
                {
                    break; 
                }
            }
            else
            {
                break; 
            }
        }

        return count;
    }
    private int DirectionB(Vector3 direction,int number)
    {
        int count = 0;
        Vector3 startPosition = Tr[number].transform.position;

        for (int i = 0; i < 4; i++)
        {
            Vector3 rayOrigin = startPosition + (direction * i * 0.5f); // 이동 방향 적용
            Ray ray = new Ray(rayOrigin, direction);
            //Ray ray = new Ray(new Vector3(startPosition.x, startPosition.y+i*0.5f, startPosition.z),direction); 
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 0.5f))
            {
                if (hit.collider.CompareTag("White"))
                {
                    count++;
                }
                else
                {
                    break;
                }
            }
            else
            {
                break;
            }
        }

        return count;
    }



}
