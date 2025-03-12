using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Firebase;
using Firebase.Auth;
using Firebase.Extensions;

public class FirebaseAuthMgr : MonoBehaviour
{
    public FirebaseUser user;
    public FirebaseAuth auth;

    public InputField emailField;
    public InputField pwField;
    public InputField nickField;

    [SerializeField] GameObject RoomUi;
    [SerializeField] GameObject LoginUi;

    public Text warningText;
    public Text successText;



    private void Awake()
    {
        //�����ڵ�
        Firebase.FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
        {
            var dependencyStatus = task.Result;
            if (dependencyStatus == Firebase.DependencyStatus.Available)
            {
                auth = Firebase.Auth.FirebaseAuth.DefaultInstance;
            }
        });
    }
    private void Start()
    {
        warningText.text = "";
        successText.text = "";


    }
    IEnumerator LoginCor(string email, string password)
    {
        var loginTask = auth.SignInWithEmailAndPasswordAsync(email, password);  
        yield return new WaitUntil(predicate: ()=> loginTask.IsCompleted);
        if(loginTask.Exception != null)
        {
            Debug.LogWarning(message: "������ ���� ������ �α��� ����:" + loginTask.Exception);
            FirebaseException firebaseEx = loginTask.Exception.GetBaseException() as FirebaseException;
            AuthError errorCode = (AuthError)firebaseEx.ErrorCode;
            string message = "";
            switch (errorCode)
            {
                case AuthError.MissingEmail:
                    message = "�̸��� ����";
                    break;
                case AuthError.MissingPassword:
                    message = "�н����� ����";
                    break;
                case AuthError.WrongPassword:
                    message = "�н����� Ʋ��";
                    break;
                case AuthError.InvalidEmail:
                    message = "�̸��� ������ ���� ����";
                    break;
                case AuthError.UserNotFound:
                    message = "���̵� �������� ����";
                    break;
                default:
                    message = "�����ڿ��� ���� �ٶ��ϴ�";
                    break;
            }
            warningText.text = message;    
        }
        else
        {
            Debug.Log("�α��� �Ϸ�");
            user = loginTask.Result.User;
            warningText.text = "";
            nickField.text = user.DisplayName;
            successText.text = "�α��� �Ϸ�" + user.DisplayName;
            RoomUi.gameObject.SetActive(true);
            LoginUi.gameObject.SetActive(false);
        }
    }

    public void Login()
    {
        StartCoroutine(LoginCor(emailField.text, pwField.text));
    }

    public void Logout()
    {
        auth.SignOut();
        Debug.Log("�α��� �ƿ�");

    }

    public void CreateId()
    {
        auth.CreateUserWithEmailAndPasswordAsync(emailField.text, pwField.text).ContinueWithOnMainThread(task =>
        {
            if (task.IsFaulted)
            {
                Debug.Log("ȸ������ ����");
                return;
            }
            if (task.IsCanceled)
            {
                Debug.Log("ȸ������ ���");
                return;
            }
            Debug.Log("ȸ������ �Ϸ�");
            FirebaseUser registeredUser = task.Result.User;

        });

    }




}
