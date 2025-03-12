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
        //안전코드
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
            Debug.LogWarning(message: "다음과 같은 이유로 로그인 실패:" + loginTask.Exception);
            FirebaseException firebaseEx = loginTask.Exception.GetBaseException() as FirebaseException;
            AuthError errorCode = (AuthError)firebaseEx.ErrorCode;
            string message = "";
            switch (errorCode)
            {
                case AuthError.MissingEmail:
                    message = "이메일 누락";
                    break;
                case AuthError.MissingPassword:
                    message = "패스워드 누락";
                    break;
                case AuthError.WrongPassword:
                    message = "패스워드 틀림";
                    break;
                case AuthError.InvalidEmail:
                    message = "이메일 형식이 옳지 않음";
                    break;
                case AuthError.UserNotFound:
                    message = "아이디가 존재하지 않음";
                    break;
                default:
                    message = "관리자에게 문의 바랍니다";
                    break;
            }
            warningText.text = message;    
        }
        else
        {
            Debug.Log("로그인 완료");
            user = loginTask.Result.User;
            warningText.text = "";
            nickField.text = user.DisplayName;
            successText.text = "로그인 완료" + user.DisplayName;
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
        Debug.Log("로그인 아웃");

    }

    public void CreateId()
    {
        auth.CreateUserWithEmailAndPasswordAsync(emailField.text, pwField.text).ContinueWithOnMainThread(task =>
        {
            if (task.IsFaulted)
            {
                Debug.Log("회원가입 오류");
                return;
            }
            if (task.IsCanceled)
            {
                Debug.Log("회원가입 취소");
                return;
            }
            Debug.Log("회원가입 완료");
            FirebaseUser registeredUser = task.Result.User;

        });

    }




}
