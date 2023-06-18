using Firebase.Database;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DataBaseManager : MonoBehaviour
{
    public const string USERID_NAME_CONST = "name";
    public const string USERID_GOLD_CONST = "gold";
    public const string USERID_WEAPONID_CONST = "currentWeaponID";
    public const string USERID_X_CONST = "x";
    public const string USERID_Y_CONST = "y";
    public const string USERID_Z_CONST = "z";

    string userID;
    DatabaseReference dbReference;


    private void Start()
    {
        userID = SystemInfo.deviceUniqueIdentifier;
        dbReference = FirebaseDatabase.DefaultInstance.RootReference;
    }

    [ContextMenu("Create/Update user")]
    void CreateOrUpdateUser()
    {
        User newUser = new User(GameManager.Instance.player.playerData);
        string json = JsonUtility.ToJson(newUser);

        dbReference.Child("users").Child(userID).SetRawJsonValueAsync(json);
        print(json);
        
    }

    public IEnumerator GetUser(Action<DataSnapshot> onCallback)
    {
        var userData = dbReference.Child("users").Child(userID).GetValueAsync();

        yield return new WaitUntil(predicate: () => userData.IsCompleted);

        if(userData != null)
        {
            DataSnapshot snapshot = userData.Result;

            onCallback.Invoke(snapshot);
        }
        else
        {
            CreateOrUpdateUser();
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

        print($"{pname} {gold} {weaponID} {x} {y} {z}");

        GameManager.Instance.player.SetPlayerData(
                pname,
                gold,
                weaponID,
                x,
                y,
                z);
    }
}
