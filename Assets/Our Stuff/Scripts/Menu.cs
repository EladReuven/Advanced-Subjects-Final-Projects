using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    private static Menu instance;

    public static Menu Instance { get { return instance; } }

    [Header("Database")]
    [SerializeField] DataBaseManager dataBaseManager;

    [Header("UI")]
    [SerializeField] TMP_InputField usernameInputField;
    [SerializeField] TMP_InputField passwordInputField;
    [SerializeField] TextMeshProUGUI responseText;
    [SerializeField] Button startButton;
    [SerializeField] Button createUserButton;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            return;
        }
        else
        {
            instance = this;
        }
    }

    private void Update()
    {
        CheckInputFieldsEmpty();
    }

    public void CheckInputFieldsEmpty()
    {
        if (usernameInputField == null || passwordInputField == null || usernameInputField.text == "" || passwordInputField.text == "")
        {
            startButton.interactable = false;
            createUserButton.interactable = false;
            return;
        }

        startButton.interactable = true;
        createUserButton.interactable = true;
    }


    public string GetUsernameInputFieldText()
    {
        return usernameInputField == null ? null : usernameInputField.text;
    }

    public string GetPasswordInputFieldText()
    {
        return passwordInputField == null ? null : passwordInputField.text;
    }

    public void SetErrorText(string text)
    {
        responseText.text = text;
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Game");
    }
}
