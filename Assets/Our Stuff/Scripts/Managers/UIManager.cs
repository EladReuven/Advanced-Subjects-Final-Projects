using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public TMP_InputField usernameInputField;
    public Button startButton;

    public void ForwardUsernameToDatabaseManager()
    {
        GameManager.Instance.dataBaseManager.SetUsername(usernameInputField.text);
    }

    public void CheckIfUserExists()
    {
        GameManager.Instance.dataBaseManager.CheckUserExists();
    }

}
