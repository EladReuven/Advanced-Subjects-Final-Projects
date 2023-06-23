using Firebase.Database;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.Application;

public class DataBaseManager : MonoBehaviour
{
    public const string USERID_NAME_CONST = "name";
    public const string USERID_USERNAME_CONST = "username";
    public const string USERID_PASSWORD_CONST = "password";
    public const string USERID_GOLD_CONST = "gold";
    public const string USERID_WEAPONID_CONST = "currentWeaponID";
    public const string USERID_X_CONST = "x";
    public const string USERID_Y_CONST = "y";
    public const string USERID_Z_CONST = "z";

    string userID;
    Menu menuInstance;
    DatabaseReference dbReference;


    private void Start()
    {
        menuInstance = Menu.Instance;
        //userID = SystemInfo.deviceUniqueIdentifier;
        dbReference = FirebaseDatabase.DefaultInstance.RootReference;
    }

    /// <summary>
    /// if a user with the same username and password already exists, it just updates the user data, if not then it creates a new one
    /// </summary>
    [ContextMenu("Try Create User")]
    public void TryCreateUser()
    {
        StartCoroutine(CreateUser());
    }

    IEnumerator CreateUser()
    {
        string username = menuInstance.GetUsernameInputFieldText();
        string password = menuInstance.GetPasswordInputFieldText();

        var userData = dbReference.Child("users").Child(username).GetValueAsync();

        yield return new WaitUntil(predicate: () => userData.IsCompleted);

        if (userData.Result.Child(username).Value.ToString() != null)
        {
            menuInstance.SetErrorText("User Already Exists");
        }
        else
        {
            menuInstance.SetErrorText("User Created");

            User newUser = new User(password, GameManager.Instance.player.playerData);
            string json = JsonUtility.ToJson(newUser);

            dbReference.Child("users").Child(username).SetRawJsonValueAsync(json);
        }
    }

    public IEnumerator GetUser(Action<DataSnapshot> onCallback)
    {
        string username = menuInstance.GetUsernameInputFieldText();
        string password = menuInstance.GetPasswordInputFieldText();

        var userData = dbReference.Child("users").Child(username).GetValueAsync();

        yield return new WaitUntil(predicate: () => userData.IsCompleted);

        print(userData.Result.Child(username).Value);

        if(userData != null)
        {
            DataSnapshot snapshot = userData.Result;

            //check if userdata password == menu password
            //string userDataPassword = snapshot.Child(USERID_PASSWORD_CONST).Value.ToString();
            //print(userDataPassword);
            //if(userDataPassword.Equals(password))
            //{
            //    onCallback.Invoke(snapshot);
            //}
            //else
            //{
            //    //if not then error message password is wrong
            //    menuInstance.SetErrorText("Wrong Password");
            //}
        }
        else
        {
            //update error message that user doesnt exist
            menuInstance.SetErrorText("User doesn't exist, Create a user first");
        }
    }

    [ContextMenu("Get User")]
    public void GetUserInfo()
    {
        StartCoroutine(GetUser(SetUser));
    }

    void SetUser(DataSnapshot userData)
    {
        string pname = userData.Child(USERID_NAME_CONST).Value.ToString();
        int gold = int.Parse(userData.Child(USERID_GOLD_CONST).Value.ToString());
        int weaponID = int.Parse(userData.Child(USERID_WEAPONID_CONST).Value.ToString());
        float x = float.Parse(userData.Child(USERID_X_CONST).Value.ToString());
        float y = float.Parse(userData.Child(USERID_Y_CONST).Value.ToString());
        float z = float.Parse(userData.Child(USERID_Z_CONST).Value.ToString());

        //print($"{pname} {gold} {weaponID} {x} {y} {z}");

        GameManager.Instance.player.SetPlayerData(
                pname,
                gold,
                weaponID,
                x,
                y,
                z);

        menuInstance.StartGame();
    }
}
